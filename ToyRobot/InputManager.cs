﻿using System.Text.RegularExpressions;

namespace ToyRobot
{
    internal class InputManager
    {
        public InputManager()
        {
            
        }

        public bool IsUserInputValid(string userInput)
        {
            // Strip the PLACE coordinates and facing so we can easily check the base commands
            int spaceIndex = userInput.IndexOf(' ');
            string input = userInput.Substring(0, spaceIndex > -1 ? spaceIndex : userInput.Length);

            switch (input)
            {
                case Constants.MOVE:
                case Constants.LEFT:
                case Constants.RIGHT:
                case Constants.REPORT:
                case Constants.PLACE:
                    return true;
                default:
                    return false;
            }
        }

        public bool PromptUserToUseFileManager()
        {
            Console.WriteLine("Welcome, this is a small application in which you will help the Robot navigate a table that is 5x5.\n");
            Console.WriteLine("The Robots starting point is 0,0 - the furthest South and West point on the table.\n");
            Console.WriteLine("The default input method for this application is via the command line. You can enter specific commands individually to control the Robot.");
            Console.WriteLine("If you prefer, you may switch to a command file instead. Would you like to switch? (Commands will be shown if you stay with the default input)\n");
            Console.Write("Press Y to switch. Any other key will result in the default input method: ");

            string userInput = Console.ReadLine().ToUpper();
            return userInput == "Y";
        }

        public void PrintInstructions()
        {
            Console.WriteLine("The Robot will not take any actions until it receives a PLACE command in the below format:");
            Console.WriteLine("PLACE X,Y,F; where X and Y are coordinates and F is the facing\n");
            Console.WriteLine("In order to move the Robot you can use the following commands: MOVE - LEFT - RIGHT\n");
            Console.WriteLine("The available facings for the Robot are NORTH - EAST - SOUTH - WEST\n");
            Console.WriteLine("You can ask the Robot to report its current location and facing by using the REPORT command\n");
            Console.WriteLine("Any commands/values other than the ones listed above will be ignored by the Robot!\n");
        }

        public string ReadInput()
        {
            Console.Write("Enter a command followed by the ENTER key: ");
            return Console.ReadLine().ToUpper();
        }
    }
}
