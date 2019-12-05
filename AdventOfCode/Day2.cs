using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day2
    {
        private int _answer1;
        private int _answer2;
        public Day2()
        {
            var computer = new Computer(LoadFile);
            
            _answer1 = computer.Run(12, 2);
            _answer2 = computer.FindVerbAndNounForOutput(19690720);

        }

        private int[] LoadFile()
        {
            return File
                .ReadAllText("..\\..\\input2.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}
