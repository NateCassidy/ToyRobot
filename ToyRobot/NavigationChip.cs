namespace ToyRobot
{
    internal class NavigationChip
    {
        private readonly int _northConstraint;
        private readonly int _eastConstraint;
        private readonly int _westAndSouthConstraint;

        // Due to the task, we can assume the westAndSouthConstraint will always be 0. The other constraints are assigned via the height (North), and width (East)
        // We could extend this by adding a west and south constraint but for the purpose of this task, it's redundant.
        public NavigationChip(int eastConstraint, int northConstraint)
        {
            _eastConstraint = eastConstraint;
            _northConstraint = northConstraint;
            _westAndSouthConstraint = 0;
        }

        // Takes in the X, Y, and F of the calling class and checks if it can perform the action.
        public bool IsValidMove(int x, int y, string facing)
        {
            switch (facing)
            {
                case Constants.NORTH:
                    return y < _northConstraint;

                case Constants.EAST:
                    return x < _eastConstraint;

                case Constants.WEST:
                    return x > _westAndSouthConstraint;

                case Constants.SOUTH:
                    return y > _westAndSouthConstraint;

                default:
                    return false;
            }
        }

        // TODO - Function to turn the calling class in a specified direction.
    }
}
