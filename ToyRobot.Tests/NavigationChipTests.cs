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

        [Test]
        [TestCase(0, 0, "Invalid Facing", false)]   // Should return false when an unknown facing is passed in.
        [TestCase(0, 4, Constants.NORTH, false)]    // Should return false when the calling class is unable to move NORTH
        [TestCase(0, 3, Constants.NORTH, true)]     // Should return true when the calling class is able to move NORTH
        [TestCase(4, 0, Constants.EAST, false)]     // Should return false when the calling class is unable to move EAST
        [TestCase(3, 0, Constants.EAST, true)]      // Should return true when the calling class is able to move EAST
        [TestCase(0, 0, Constants.SOUTH, false)]    // Should return false when the calling class is unable to move SOUTH
        [TestCase(0, 1, Constants.SOUTH, true)]     // Should return true when the calling class is able to move SOUTH
        [TestCase(0, 0, Constants.WEST, false)]     // Should return false when the calling class is unable to move WEST
        [TestCase(1, 0, Constants.WEST, true)]      // Should return true when the calling class is able to move WEST
        public void IsValidMove(int x, int y, string facing, bool expectedResult)
        {
            bool isValid = _chip.IsValidMove(x, y, facing);
            Assert.That(isValid, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(Constants.NORTH, false, Constants.WEST)]  // Should return WEST when we pass in a facing of NORTH and a false value for isTurningRight
        [TestCase(Constants.NORTH, true, Constants.EAST)]   // Should return EAST when we pass in a facing of NORTH and a true value for isTurningRight
        [TestCase(Constants.EAST, false, Constants.NORTH)]  // Should return NORTH when we pass in a facing of EAST and a false value for isTurningRight
        [TestCase(Constants.EAST, true, Constants.SOUTH)]   // Should return SOUTH when we pass in a facing of EAST and a true value for isTurningRight
        [TestCase(Constants.SOUTH, false, Constants.EAST)]  // Should return EAST when we pass in a facing of SOUTH and a false value for isTurningRight
        [TestCase(Constants.SOUTH, true, Constants.WEST)]   // Should return WEST when we pass in a facing of SOUTH and a true value for isTurningRight
        [TestCase(Constants.WEST, false, Constants.SOUTH)]  // Should return SOUTH when we pass in a facing of WEST and a false value for isTurningRight
        [TestCase(Constants.WEST, true, Constants.NORTH)]   // Should return NORTH when we pass in a facing of WEST and a true value for isTurningRight
        public void Rotate(string facing, bool isTurningRight, string expectedResult)
        {
            string result = _chip.rotate(facing, isTurningRight);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
