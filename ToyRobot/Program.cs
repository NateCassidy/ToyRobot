namespace ToyRobot
{
    /** TODO 
     * 
     * 
     * */
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager(@"C:\Users\nazca\Documents\GitHub\ToyRobot\ToyRobot\CommandSetOne.txt");
            fileManager.ReadFromFile();
            fileManager.PrintStoredFileContent();

            Robot robo = new Robot(fileManager.getFileContent());
            robo.ExecuteCommands();

        }
    }
}
