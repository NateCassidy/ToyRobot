namespace ToyRobot.Tests
{
    public class FileManagerTests
    {
        FileManager _fileManager;

        [Test]
        // Should check that the fileName is set correctly when creating an instance of the FileManager
        public void setFileName()
        {
            string testFileName = "Test File Name";
            _fileManager = new FileManager(testFileName);
            Assert.That(_fileManager.getFileName(), Is.EqualTo(testFileName));
        }

        [Test]
        // Should check that the fileContent is set correctly
        public void setFileContent()
        {
            string[] expectedFileContent = new string[] { "MOVE", "LEFT", "MOVE", "REPORT" };

            _fileManager = new FileManager(@"C:\Users\nazca\Documents\GitHub\ToyRobot\ToyRobot.Tests\TestCommands.txt");
            _fileManager.ReadFromFile();

            Assert.That(_fileManager.getFileContent(), Is.EqualTo(expectedFileContent));
        }
    }
}