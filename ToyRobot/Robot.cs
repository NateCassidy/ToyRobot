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
            if(_commandsToExecute.Length == 0)
            {
                Console.WriteLine("You have given me no commands.");
                return;
            }
 
            bool hasRobotReceivedPlaceCommand = false;

            foreach(string command in _commandsToExecute)
            {
                if(!hasRobotReceivedPlaceCommand && command != Constants.PLACE)
                {
                    Console.WriteLine($"A valid {Constants.PLACE} command has not been received. Discarding the following command: {command}");
                    continue;
                }

                // TODO - Move the PLACE command into a controller? That should be responsible for placing the robot, and checking if it's able to be placed?
                if(command == Constants.PLACE)
                {
                    hasRobotReceivedPlaceCommand = true;
                    Place();
                }
                else if(command == Constants.MOVE)
                {
                    Move();
                }
                else if(command == Constants.REPORT)
                {
                    Report();
                }
                else if(command == Constants.RIGHT)
                {
                    TurnRight();
                }
                else if(command == Constants.LEFT)
                {
                    TurnLeft();
                }
                else
                {
                    Console.WriteLine($"Unknown command specified: {command} - No action has been carried out.");
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
