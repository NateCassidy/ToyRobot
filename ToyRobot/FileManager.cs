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

        public void setFileName(string fileName)
        {
            _fileName = fileName;
        }

        public string[] getFileContent()
        {
            return _fileContent;
        }


        public FileManager()
        {

        }


        public void ReadFromFile()
        {
            _fileContent = File.ReadAllLines(_fileName);
        }

        public bool DoesFileExist()
        {
            return File.Exists(_fileName);
        }

        public void PrintInstructions()
        {
            Console.WriteLine("\nThere are some example command files below, alternatively you can place your own file within the solution to run it.");
            Console.WriteLine("Example Files: CommandSetOne.txt - CommandSetTwo.txt - CommandSetThree.txt - CommandSetFour.txt\n");
        }
    }
}
