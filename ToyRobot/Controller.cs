namespace ToyRobot
{
    internal class Controller
    {
        private bool _isUsingFileManager;

        private Robot _robot;
        private Table _table;
        private FileManager _fileManager;
        private InputManager _inputManager;

        private void setIsUsingFileManager(bool isUsingFileManager)
        {
            _isUsingFileManager = isUsingFileManager;
        }

        private bool getIsUsingFileManager()
        {
            return _isUsingFileManager;
        }

        public Controller()
        {
            // TODO - Change this to accept any file within this directory
            _fileManager = new FileManager(@"C:\Users\nazca\Documents\GitHub\ToyRobot\ToyRobot\CommandSetTwo.txt");
            _fileManager.ReadFromFile();

            _inputManager = new InputManager();
            _table = new Table(5, 5);
        }

        // Determines whether the application is taking input via the command line or a file and sets up the robot.
        public void SetupApplication()
        {
           setIsUsingFileManager(_inputManager.PromptUserToUseFileManager());

            NavigationChip chip = new NavigationChip(_table.getWidth() - 1, _table.getHeight() - 1);
            _robot = new Robot(chip);

            if (_isUsingFileManager)
            {
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
                userInput = _inputManager.ReadInput();

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
                var (xPosition, yPosition, facing, isValidPlace) = ExtractPlacementCommandVariables(command);

                if (!isValidPlace)
                {
                    Console.WriteLine($"The place command was not valid.");
                    return;
                }
                PlaceRobot(xPosition, yPosition, facing);
            }
            else
            {
                _robot.ExecuteCommand(command);
            }
        }

        private bool IsValidPlaceCommand(int x, int y)
        {
           return (x > -1 && x < 5) && (y > -1 && y < 5);
        }

        private (int, int, string, bool) ExtractPlacementCommandVariables(string command)
        {
            string placeCommandParams = command.Substring(command.IndexOf(' '));
            string[] placementParams = placeCommandParams.Split(',');

            int xPosition = Int32.Parse(placementParams[0]);
            int yPosition = Int32.Parse(placementParams[1]);
            string facing = placementParams[2];

            return (xPosition, yPosition, facing, IsValidPlaceCommand(xPosition, yPosition));
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
