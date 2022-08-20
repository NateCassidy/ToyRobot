namespace ToyRobot.Tests
{
    public class FileManagerTests
    {
        FileManager _fileManager;

        private string _testFileName;

        [SetUp]
        public void SetUp()
        {
            _testFileName = "CommandSetThree.txt";
            _fileManager = new FileManager();
            _fileManager.setFileName(_testFileName);
        }

        [Test]
        // Should check that the fileName is set correctly when creating an instance of the FileManager
        public void setFileName()
        {
            Assert.That(_fileManager.getFileName(), Is.EqualTo(_testFileName));
        }

        [Test]
        // Should check that the file exists
        public void DoesFileExist()
        {
            Assert.That(_fileManager.DoesFileExist(), Is.True);
        }

        [Test]
        // Should check that the file does not exist
        public void FileDoesNotExist()
        {
            FileManager fileManager = new FileManager();
            fileManager.setFileName("Unknown File");

            Assert.That(fileManager.DoesFileExist(), Is.False);
        }

        [Test]
        // Should check that the fileContent is set correctly
        public void setFileContent()
        {
            string[] expectedFileContent = new string[] { "PLACE 2,2,NORTH", Constants.MOVE, Constants.MOVE, Constants.MOVE, Constants.REPORT, Constants.MOVE };

            _fileManager.ReadFromFile();
            Assert.That(_fileManager.getFileContent(), Is.EqualTo(expectedFileContent));
        }
    }
}