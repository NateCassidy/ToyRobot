using System.Reflection;
using System.Text.RegularExpressions;

namespace ToyRobot
{
    internal class Controller
    {
        private bool _isUsingFileManager;
        private bool _hasInitialPlaceCommandBeenEntered;

        private Robot _robot;
        private Table _table;
        private FileManager _fileManager;
        private InputManager _inputManager;
        private NavigationChip _chip;

        private void setIsUsingFileManager(bool isUsingFileManager)
        {
            _isUsingFileManager = isUsingFileManager;
        }

        public Controller()
        {
            _inputManager = new InputManager();
            _fileManager = new FileManager();
            _table = new Table(5, 5);

            _chip = new NavigationChip(_table.getWidth() - 1, _table.getHeight() - 1);
            _robot = new Robot(_chip);

            _hasInitialPlaceCommandBeenEntered = false;
        }

        // Determines whether the application is taking input via the command line or a file.
        public void SetupApplication()
        {
           setIsUsingFileManager(_inputManager.PromptUserToUseFileManager());

            if (_isUsingFileManager)
            {
                _fileManager.PrintInstructions();
                StartGetFileNameLoop();
            }
            else
            {
                _inputManager.PrintInstructions();
                StartInputLoop();
            }
        }

        // Starts the Input Loop when using the InputManager. Reads input and passes it to validation, and checks for the user quitting the application.
        private void StartInputLoop()
        {
            string userInput;

            while (true)
            {
                userInput = _inputManager.ReadInput().ToUpper().Trim();

                if(userInput == Constants.QUIT_COMMAND)
                {
                    break;
                }
                else if (_inputManager.IsUserInputValid(userInput))
                {
                    ExecuteCommand(userInput);
                }
                else
                {
                    Console.WriteLine($"'{userInput}' is not a valid command. Please try again.\n");
                }
            }
            Console.WriteLine("Closing the application. Thanks for having a look!");
        }

        private void StartGetFileNameLoop()
        {

            while (true)
            {
                Console.Write("Please enter the file name, including the extension: ");

                _fileManager.setFileName(Console.ReadLine());

                if (_fileManager.DoesFileExist())
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"{_fileManager.getFileName()} is not a valid file. Please check and enter the correct fileName to proceed.\n");
                }
            }
            _fileManager.ReadFromFile();
            SendCommandsToRobot();
        }

        // Executes the commands. This is used for both the Input and File managers as the Robot operates on the same command list either way.
        private void ExecuteCommand(string command)
        {
            if (command.StartsWith(Constants.PLACE))
            {
                ExecutePlaceCommand(command);
            }
            else if (_hasInitialPlaceCommandBeenEntered)
            {
                _robot.ExecuteCommand(command);
            }
            else
            {
                Console.WriteLine($"{command} is a valid command. However, you must enter a valid {Constants.PLACE} command prior to entering any others.\n");
            }
        }

        // Passes the PLACE command to the below validation functions, and executes it if applicable.
        private void ExecutePlaceCommand(string command)
        {
            bool isPlaceCommandFormatValid = IsPlaceCommandFormatValid(command);

            if (!isPlaceCommandFormatValid)
            {
                Console.WriteLine($"{Constants.PLACE} command was not in the correct format of: {Constants.PLACE} X,Y,F");
                Console.WriteLine($"Example: PLACE 2,2,NORTH - Valid facings are {Constants.NORTH}, {Constants.EAST}, {Constants.SOUTH}, {Constants.WEST}\n");
                return;
            }

            var (xPosition, yPosition, facing, isValidPlace) = ExtractPlacementCommandVariables(command);

            if (!isValidPlace)
            {
                Console.WriteLine($"The place command is in the correct format but is either outside the table constraints or has an incorrect facing.\n");
                return;
            }

            _hasInitialPlaceCommandBeenEntered = true;
            PlaceRobot(xPosition, yPosition, facing);
        }

        // Places the Robot at X and Y position, facing the specified direction.
        private void PlaceRobot(int x, int y, string facing)
        {
            _robot.setCurrentXPosition(x);
            _robot.setCurrentYPosition(y);
            _robot.setCurrentFacing(facing);
        }


        // CONTROLLER FUNCTIONS TO VALIDATE INPUT COMMANDS


        private (int, int, string, bool) ExtractPlacementCommandVariables(string command)
        {
            string placeCommandParams = command.Substring(command.IndexOf(' '));
            string[] placementParams = placeCommandParams.Split(',');

            int xPosition = Int32.Parse(placementParams[0]);
            int yPosition = Int32.Parse(placementParams[1]);
            string facing = placementParams[2];

            return (xPosition, yPosition, facing, IsValidCoordinates(xPosition, yPosition) && IsValidFacing(facing));
        }

        private bool IsPlaceCommandFormatValid(string command)
        {
            var isPlaceCommandValid = Regex.Match(command, "^[a-zA-Z]+\\s[0-9]+,[0-9]+,[a-zA-Z]+$");
            return isPlaceCommandValid.Success;
        }

        private bool IsValidCoordinates(int x, int y)
        {
            int westAndSouthConstraint = _chip.getWestAndSouthConstraint();

            return (x >= westAndSouthConstraint && x < _chip.getEastConstraint()) && (y >= westAndSouthConstraint && y < _chip.getNorthConstraint());

        }

        private bool IsValidFacing(string facing)
        {
            return Array.IndexOf(_chip.getDirections(), facing) > -1;
        }


        // CONTROLLER FUNCTIONS TO MANAGE FILE COMMANDS


        private void SendCommandsToRobot()
        {
            if (IsCommandListForRobotValid())
            {
                foreach(string command in GetCommandListForRobot())
                {
                    ExecuteCommand(command);
                }
            }
            else
            {
                Console.WriteLine($"No {Constants.PLACE} commands were found. Please restart and enter a valid command set with at least one {Constants.PLACE} command in the correct format");
            }
        }

        // Returns an array of strings containing the initial PLACE command and all subsequent commands. Discards all commands prior to the initial PLACE command
        private string[] GetCommandListForRobot()
        {
            int indexOfInitialPlaceCommand = GetIndexOfInitialPlaceCommand();

            // If the index is greater than 0, it means we had some invalid commands prior to the initial PLACE. Print them out to the user.
            if(indexOfInitialPlaceCommand > 0)
            {
                Console.WriteLine($"Discarding the following commands: {string.Join(", ", _fileManager.getFileContent().Take(indexOfInitialPlaceCommand))}");
            }

            string[] robotCommands = _fileManager.getFileContent().Skip(indexOfInitialPlaceCommand).ToArray();
            return robotCommands;
        }

        // Validates the commands received from the FileManager by checking for an initial PLACE command.
        private bool IsCommandListForRobotValid()
        {
            return GetIndexOfInitialPlaceCommand() > -1;
        }

        private int GetIndexOfInitialPlaceCommand()
        {
            return Array.FindIndex(_fileManager.getFileContent(), (command) => command.StartsWith(Constants.PLACE));
        }
    }
}
