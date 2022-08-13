namespace ToyRobot
{
    internal class Robot
    {
        private string? _currentDirection;
        private string[]? _commandsToExecute;

        // TODO - Look into other data structures, maybe this isn't the most practical way to achieve this?
        private struct Coordinates
        {
            private int x;
            private int y;
        }

        // TODO - Remove the function call from here? The task doesn't specify that a user should run the command to execute movement. 
        public Robot(string[] commandsToExecute)
        {
            _commandsToExecute = commandsToExecute;
        }

        public void ExecuteCommands()
        {
            foreach(string command in _commandsToExecute)
            {
                switch (command)
                {
                    case Constants.PLACE:
                        Place();
                        break;

                    case Constants.MOVE:
                        Move();
                        break;

                    case Constants.LEFT:
                        TurnLeft();
                        break;

                    case Constants.RIGHT:
                        TurnRight();
                        break;

                    case Constants.REPORT:
                        Report();
                        break;

                    default:
                        Console.WriteLine($"Unknown command specified: {command} - No action has been carried out.");
                        break;
                }
            }
        }

        private void Move() 
        {
            Console.WriteLine("Moving...");
        }

        private void Report()
        {
            Console.WriteLine("Reporting...");
        }

        private void Place()
        {
            Console.WriteLine("Placing at...");
        }

        private void TurnRight()
        {
            Console.WriteLine("Turning right...");
        }

        private void TurnLeft()
        {
            Console.WriteLine("Turning left...");
        }
    }
}
