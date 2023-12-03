namespace Lab2_47
{
    public class Program
    {
        public static string InputFilePath = @"..\..\input.txt";
        public static string OutputFilePath = @"..\..\output.txt";

        static void Main(string[] args)
        {
            FileInfo outputFileInfo = new FileInfo(OutputFilePath);
            var numberOfTrees = Convert.ToInt32(File.ReadLines(InputFilePath).First().Trim());
            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                if (numberOfTrees < 1 || numberOfTrees > 50)
                {
                    streamWriter.WriteLine("Number is out of range");
                }
                else
                {
                    streamWriter.WriteLine(3 * Math.Pow(2, numberOfTrees - 1));
                }
            }
        }
    }
}