namespace ToyRobot
{
    public class Table
    {
        private int[,] _dimensions;

        public int getHeight()
        {
            return _dimensions.GetLength(1);
        }

        public int getWidth()
        {
            return _dimensions.GetLength(0);
        }

        public Table(int width, int height)
        {
            _dimensions = new int[width, height];
        }
    }
}
