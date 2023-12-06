using System;

namespace Labs
{
    public class Lab2Manager
    {
        private readonly int _numberOfTrees;

        public Lab2Manager(int numberOfTrees)
        {
            _numberOfTrees = numberOfTrees;
        }

        public string Run()
        {
            if (_numberOfTrees < 1 || _numberOfTrees > 50)
            {
                throw new Exception("Number is out of range");
            }

            return (3 * Math.Pow(2, _numberOfTrees - 1)).ToString();
        }
    }
}
