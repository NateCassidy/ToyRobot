namespace ToyRobot
{
    public class InputManager
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
            Console.Write($"Press {Constants.CONFIRM_COMMAND} to switch. Any other input will result in the default input method: ");

            return Console.ReadLine().ToUpper() == Constants.CONFIRM_COMMAND;
        }

        public void PrintInstructions()
        {
            Console.WriteLine($"\nThe Robot will not take any actions until it receives a {Constants.PLACE} command in the following format - 'PLACE X,Y,F' where X and Y are coordinates and F is the facing\n");
            Console.WriteLine($"In order to move the Robot you can use the following commands: {Constants.MOVE} - {Constants.LEFT} - {Constants.RIGHT}\n");
            Console.WriteLine($"The available facings for the Robot are {Constants.NORTH} - {Constants.EAST} - {Constants.SOUTH} - {Constants.WEST}\n");
            Console.WriteLine($"You can ask the Robot to report its current location and facing by using the {Constants.REPORT} command\n");
            Console.WriteLine($"Any commands/values other than the ones listed above will be ignored by the Robot! You can exit the application at any time by pressing {Constants.QUIT_COMMAND}.\n");
        }

        public string ReadInput()
        {
            Console.Write("Enter a command followed by the ENTER key: ");
            return Console.ReadLine();
        }
    }
}
