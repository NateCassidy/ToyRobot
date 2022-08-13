namespace ToyRobot.Tests
{
    public class FileManagerTests
    {
        FileManager fileManager;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void setFileName()
        {
            string testFileName = "Test File Name";
            fileManager = new FileManager(testFileName);
            Assert.AreEqual(testFileName, fileManager.getFileName());
        }
    }
}