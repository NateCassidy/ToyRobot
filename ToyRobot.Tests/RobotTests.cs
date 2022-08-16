namespace ToyRobot.Tests
{
    public class RobotTests
    {
        Robot _robot;
        NavigationChip _chip;

        [SetUp]
        public void SetUp()
        {
            _chip = new NavigationChip(5,5);
            _robot = new Robot(_chip);
        }

        [Test]
        // Should set the Robot's current facing to the value passed in
        public void SetCurrentFacing()
        {
            string expectedFacing = Constants.NORTH;
            _robot.setCurrentFacing(Constants.NORTH);

            Assert.That(expectedFacing, Is.EqualTo(_robot.getCurrentFacing()));
        }

        [Test]
        // Should set the Robot's current X position to the value passed in
        public void SetCurrentXPosition()
        {
            int expectedXPosition = 2;
            _robot.setCurrentXPosition(expectedXPosition);

            Assert.That(expectedXPosition, Is.EqualTo(_robot.getCurrentXPosition()));
        }

        [Test]
        // Should set the Robot's current Y position to the value passed in
        public void SetCurrentYPosition()
        {
            int expectedYPosition = 4;
            _robot.setCurrentYPosition(expectedYPosition);

            Assert.That(expectedYPosition, Is.EqualTo(_robot.getCurrentYPosition()));
        }

        [Test]
        [TestCase(Constants.NORTH, 2, 3)]   // Should move the Robot NORTH and result in placement at coordinates X:2, Y:3
        [TestCase(Constants.EAST, 3, 2)]    // Should move the Robot EAST and result in placement at coordinates X:3, Y:2
        [TestCase(Constants.SOUTH, 2, 1)]   // Should move the Robot SOUTH and result in placement at coordinates X:1, Y:2
        [TestCase(Constants.WEST, 1, 2)]    // Should move the Robot WEST and result in placement at coordinates X:1, Y:2
        public void MoveWhenValid(string facing, int x, int y)
        {
            // Set the Robot to 2,2 each iteration with a new facing and execute the MOVE command
            _robot.setCurrentXPosition(2);
            _robot.setCurrentYPosition(2);
            _robot.setCurrentFacing(facing);
            _robot.ExecuteCommand(Constants.MOVE);

            Assert.That(x, Is.EqualTo(_robot.getCurrentXPosition()));
            Assert.That(y, Is.EqualTo(_robot.getCurrentYPosition()));           
        }
    }
}
