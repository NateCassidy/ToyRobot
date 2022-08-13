namespace ToyRobot
{
    public class FileManager
    {
        private string _fileName;
        private string[] _fileContent;

        // TODO - Change this to be set multiple times by adding a setter, or have it set once on application load and expect a re-load to go again?
        //      - Change the ReadFromFile function to return the array to the calling controller (if the app takes this approach), or is it better to store it here, then use a getter
        public string getFileName()
        {
            return _fileName;
        }

        // TODO - Remove this, if the above functionality is implemented for ReadFromFile. Currently for testing purposes
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

        public void PrintStoredFileContent()
        {
            if(_fileContent.Length == 0)
            {
                Console.WriteLine("There was no content stored in the FileManager. Have you tried running the FileManager.ReadFromFile command?");
                return;
            }

            foreach(string command in _fileContent)
            {
                Console.WriteLine(command);
            }
        }
    }
}
