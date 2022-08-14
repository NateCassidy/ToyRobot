namespace ToyRobot
{
    internal class Controller
    {
        private Robot _robot;
        private Table _table;
        private FileManager _fileManager;

        public Controller()
        {
            // TODO - Tidy up file manager, remove printStoredFileContent
            _fileManager = new FileManager(@"C:\Users\nazca\Documents\GitHub\ToyRobot\ToyRobot\CommandSetOne.txt");
            _fileManager.ReadFromFile();

            _table = new Table(5, 5);
        }

        public void SendCommandsToRobot()
        {
            if (IsCommandListForRobotValid())
            {
                NavigationChip chip = new NavigationChip(_table.getWidth() - 1, _table.getHeight() - 1);

                _robot = new Robot(chip);

                foreach(string command in GetCommandListForRobot())
                {
                    if (command.StartsWith(Constants.PLACE))
                    {
                        PlaceRobot(command);
                        continue;
                    }
                    _robot.ExecuteCommand(command);
                }
            }
            else
            {
                Console.WriteLine($"No {Constants.PLACE} commands were found. Please restart and enter a valid command set with at least one {Constants.PLACE} command");
            }
        }

        // Places the Robot at X and Y position, facing the specified direction.
        private void PlaceRobot(string command)
        {
            string placeCommandParams = command.Substring(command.IndexOf(' '));
            string[] placementParams = placeCommandParams.Split(',');

            _robot.setCurrentXPosition(Int32.Parse(placementParams[0]));
            _robot.setCurrentYPosition(Int32.Parse(placementParams[1]));
            _robot.setCurrentFacing(placementParams[2]);
        }

        // Returns an array of strings containing the initial PLACE command and all subsequent commands. Discards all commands prior to the initial PLACE command
        private string[] GetCommandListForRobot()
        {
            int indexOfInitialPlaceCommand = Array.FindIndex(_fileManager.getFileContent(), (command) => command.StartsWith(Constants.PLACE));
            Console.WriteLine($"Discarding the following commands: {string.Join(", ", _fileManager.getFileContent().Take(indexOfInitialPlaceCommand))}");

            string[] robotCommands = _fileManager.getFileContent().Skip(indexOfInitialPlaceCommand).ToArray();
            return robotCommands;
        }

        // Validates the commands received from the FileManager by checking for an initial PLACE command.
        private bool IsCommandListForRobotValid()
        {
            int indexOfInitialPlaceCommand = Array.FindIndex(_fileManager.getFileContent(), (command) => command.StartsWith(Constants.PLACE));
            return indexOfInitialPlaceCommand > -1;
        }
    }
}
