using System;
using System.IO;
using System.Linq;

namespace Labs
{
    public static class Lab2
    {
        public static string Run(string pathInpFile = "input.txt")
        {
            var numberOfTrees = Convert.ToInt32(File.ReadLines(pathInpFile).First().Trim());
            return $"{3 * Math.Pow(2, numberOfTrees - 1)}";
        }
    }
}
