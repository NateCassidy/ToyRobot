using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public void PlaceRobot()
        {
            if (!ValidateCommandListForRobot())
            {
                _robot = new Robot(GetCommandListForRobot());
                _robot.ExecuteCommands();
            }
            else
            {
                Console.WriteLine($"No {Constants.PLACE} commands were found. Please restart and enter a valid command set with at least one {Constants.PLACE} command");
            }
        }

        // Returns an array of strings containing the initial PLACE command and all subsequent commands. Discards all commands prior to the initial PLACE command
        private string[] GetCommandListForRobot()
        {
            int indexOfInitialPlaceCommand = Array.FindIndex(_fileManager.getFileContent(), (command) => command.Equals(Constants.PLACE));
            Console.WriteLine($"Discarding the following commands: {string.Join(", ", _fileManager.getFileContent().Take(indexOfInitialPlaceCommand))}");

            string[] robotCommands = _fileManager.getFileContent().Skip(indexOfInitialPlaceCommand).ToArray();
            return robotCommands;
        }

        // Validates the commands received from the FileManager by checking for an initial PLACE command.
        private bool ValidateCommandListForRobot()
        {
            int indexOfInitialPlaceCommand = Array.FindIndex(_fileManager.getFileContent(), (command) => command.Equals(Constants.PLACE));
            return indexOfInitialPlaceCommand == -1;
        }
    }
}
