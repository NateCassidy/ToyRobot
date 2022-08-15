namespace ToyRobot.Tests
{
    public class TableTests
    {
        private Table _table;

        [SetUp]
        public void SetUp()
        {
            _table = new Table(5, 3);
        }

        [Test]
        // Should check the table height is set correctly
        public void GetTableHeight()
        {
            int expectedHeight = 3;
            Assert.That(expectedHeight, Is.EqualTo(_table.getHeight()));
        }

        [Test]
        // Should check the table width is set correctly
        public void GetTableWidth()
        {
            int expectedWidth = 5;
            Assert.That(expectedWidth, Is.EqualTo(_table.getWidth()));
        }
    }
}
