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
                // TODO - Change this to accept any file within this directory
                _fileManager = new FileManager(@"C:\Users\nazca\Documents\GitHub\ToyRobot\ToyRobot\CommandSetTwo.txt");
                _fileManager.ReadFromFile();
                SendCommandsToRobot();
            }
            else
            {
                GetInputFromInputManager();
            }
        }

        private void GetInputFromInputManager()
        {
            string userInput;

            do
            {
                userInput = _inputManager.ReadInput().ToUpper();

                if (_inputManager.IsUserInputValid(userInput))
                {
                    ExecuteCommand(userInput);
                }
                else
                {
                    Console.WriteLine($"'{userInput}' is not a valid command. Please try again.");
                }
            } while (userInput != "Q");
        }

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
                Console.WriteLine($"You must enter a valid {Constants.PLACE} command prior to entering any others.");
            }
        }

        private void ExecutePlaceCommand(string command)
        {
            bool isPlaceCommandFormatValid = IsPlaceCommandFormatValid(command);

            if (!isPlaceCommandFormatValid)
            {
                Console.WriteLine($"{Constants.PLACE} command was not in the correct format of: {Constants.PLACE} X,Y,F");
                return;
            }

            var (xPosition, yPosition, facing, isValidPlace) = ExtractPlacementCommandVariables(command);

            if (!isValidPlace)
            {
                Console.WriteLine($"The place command is in the correct format but is either outside the table constraints or has an incorrect facing.");
                return;
            }
            _hasInitialPlaceCommandBeenEntered = true;
            PlaceRobot(xPosition, yPosition, facing);
        }

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
            return (x > -1 && x < 5) && (y > -1 && y < 5);

        }

        private bool IsValidFacing(string facing)
        {
            return Array.IndexOf(_chip.getDirections(), facing) > -1;
        }

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
                Console.WriteLine($"No {Constants.PLACE} commands were found. Please restart and enter a valid command set with at least one {Constants.PLACE} command");
            }
        }

        // Places the Robot at X and Y position, facing the specified direction.
        private void PlaceRobot(int x, int y, string facing)
        { 
            _robot.setCurrentXPosition(x);
            _robot.setCurrentYPosition(y);
            _robot.setCurrentFacing(facing);
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
