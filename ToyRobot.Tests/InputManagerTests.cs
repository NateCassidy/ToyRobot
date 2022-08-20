namespace ToyRobot.Tests
{
    public class InputManagerTests
    {
        private InputManager _inputManager;

        [SetUp]
        public void SetUp()
        {
            _inputManager = new InputManager();
        }

        [Test]
        [TestCase(Constants.MOVE, true)]
        [TestCase(Constants.LEFT, true)]
        [TestCase(Constants.RIGHT, true)]
        [TestCase(Constants.REPORT, true)]
        [TestCase(Constants.PLACE, true)]
        [TestCase("PLACE 2,2,NORTH", true)]     // Test that it works by stripping the values following the space
        [TestCase("PLACE2,2,NORTH", false)]     // Test that it works when no space is provided, making it an invalid PLACE command
        [TestCase("Unknown Command", false)]    // Test that it works with an unknown command
        public void IsUserInputValid(string command, bool expectedResult)
        {
            Assert.That(_inputManager.IsUserInputValid(command), Is.EqualTo(expectedResult));
        }
    }
}
