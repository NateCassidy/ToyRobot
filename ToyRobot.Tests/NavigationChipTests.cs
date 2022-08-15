namespace ToyRobot.Tests
{
    public class NavigationChipTests
    {
        private NavigationChip _chip;

        [SetUp]
        public void SetUp()
        {
            _chip = new NavigationChip(4, 4);
        }

        // TODO - Look into a loop for the IsValidMove tests, something similar to Jest's It.Each

        [Test]
        // Should return false when an unknown facing is passed in.
        public void IsValidMoveUnknownFacing()
        {
            bool isValid = _chip.IsValidMove(0, 0, "Invalid Facing");
            Assert.That(isValid, Is.False);
        }

        [Test]
        // Should return false when the calling class is unable to move North
        public void IsValidMoveNorthFail()
        {
            bool isValid = _chip.IsValidMove(0, 4, Constants.NORTH);
            Assert.That(isValid, Is.False);
        }

        [Test]
        // Should return true when the calling class is able to move NORTH
        public void IsValidMoveNorthPass()
        {
            bool isValid = _chip.IsValidMove(0, 3, Constants.NORTH);
            Assert.That(isValid, Is.True);
        }

        [Test]
        // Should return false when the calling class is unable to move East
        public void IsValidMoveEastFail()
        {
            bool isValid = _chip.IsValidMove(4,0, Constants.EAST);
            Assert.That(isValid, Is.False);
        }

        [Test]
        // Should return true when the calling class is able to move EAST
        public void IsValidMoveEastPass()
        {
            bool isValid = _chip.IsValidMove(3, 0, Constants.EAST);
            Assert.That(isValid, Is.True);
        }

        [Test]
        // Should return WEST when we pass in a facing of NORTH and a false value for isTurningRight
        public void RotateNorthToWest()
        {
            string facing = _chip.rotate(Constants.NORTH, false);
            Assert.That(facing, Is.EqualTo(Constants.WEST));
        }

        [Test]
        // Should return EAST when we pass in a facing of NORTH and a true value for isTurningRight
        public void RotateNorthToEast()
        {
            string facing = _chip.rotate(Constants.NORTH, true);
            Assert.That(facing, Is.EqualTo(Constants.EAST));
        }

        [Test]
        // Should return NORTH when we pass in a facing of WEST and a true value for isTurningRight
        public void RotateWestToNorth()
        {
            string facing = _chip.rotate(Constants.WEST, true);
            Assert.That(facing, Is.EqualTo(Constants.NORTH));
        }

        [Test]
        // Should return SOUTH when we pass in a facing of WEST and a false value for isTurningRight
        public void RotateWestToSouth()
        {
            string facing = _chip.rotate(Constants.WEST, false);
            Assert.That(facing, Is.EqualTo(Constants.SOUTH));
        }
    }
}
