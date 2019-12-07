using System.Linq;
using System.IO;

namespace AdventOfCode
{
    public class Day5
    {
        private long _answer1;
        private long _answer2;
        private Computer computer;
        public Day5()
        {
            computer = new Computer(LoadFile);
        
            _answer1 = computer.GetDiagnosticCodeFor(1);
            _answer2 = computer.GetDiagnosticCodeFor(5);
        }
        
        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
        
        private int[] LoadFile()
        {
            return File
                .ReadAllText("..\\..\\input5.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();
        }
    }
}
