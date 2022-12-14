namespace ToyRobot
{
    public class Robot
    {
        private string _currentFacing;

        private int _currentXPosition;
        private int _currentYPosition;

        private NavigationChip _navigationChip;


        public Robot(NavigationChip navigationChip)
        {
            _navigationChip = navigationChip;
        }

        public void setCurrentFacing(string facing)
        {
            _currentFacing = facing;
        }

        public string getCurrentFacing()
        {
            return _currentFacing;
        }

        public void setCurrentXPosition(int x)
        {
            _currentXPosition = x;
        }

        public int getCurrentXPosition()
        {
            return _currentXPosition;
        }

        public void setCurrentYPosition(int y)
        {
            _currentYPosition = y;
        }

        public int getCurrentYPosition()
        {
            return _currentYPosition;
        }

        public void ExecuteCommand(string command)
        {
            switch (command)
            {
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

        private void Move() 
        {
            bool isValidMove = _navigationChip.IsValidMove(_currentXPosition, _currentYPosition, _currentFacing);

            if (isValidMove)
            {
                ApplyMove(_currentFacing);
            }
            else
            {
                Console.WriteLine($"An invalid move was performed. No action has been taken.");
            }
        }

        private void ApplyMove(string facing)
        {
            switch (facing)
            {
                case Constants.NORTH:
                    _currentYPosition += 1;
                    break;

                case Constants.SOUTH:
                    _currentYPosition -= 1;
                    break;

                case Constants.EAST:
                    _currentXPosition += 1;
                    break;

                case Constants.WEST:
                    _currentXPosition -= 1;
                    break;

                default:
                    Console.WriteLine($"An incorrect facing of '{facing}' was provided. No action was taken.\n");
                    break;
            }
        }

        private void Report()
        {
            Console.WriteLine($"Reporting.....Currently facing {_currentFacing} at coordinates - X:{_currentXPosition}, Y:{_currentYPosition}.\n");
        }

        private void TurnRight()
        {
            _currentFacing = _navigationChip.rotate(_currentFacing, true);
        }

        private void TurnLeft()
        {
            _currentFacing = _navigationChip.rotate(_currentFacing, false);
        }
    }
}
