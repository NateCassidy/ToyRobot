namespace ToyRobot
{
    public class FileManager
    {
        private string _fileName;
        private string[] _fileContent;

        public string getFileName()
        {
            return _fileName;
        }

        public string[] getFileContent()
        {
            return _fileContent;
        }


        public FileManager(string fileName)
        {
            _fileName = fileName;
        }


        public void ReadFromFile()
        {
            _fileContent = File.ReadAllLines(_fileName);
        }
    }
}
