using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day5
    {
        private long _answer1;
        private long _answer2;
        public Day5()
        {
            var computer = new Computer();

            _answer1 = computer.LoadProgram(LoadFile).Set(1).Run().DiagnosticCode;
            _answer2 = computer.LoadProgram(LoadFile).Set(5).Run().DiagnosticCode;
        }
        
        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
        
        private IEnumerable<long> LoadFile()
        {
            return File
                .ReadAllText("..\\..\\input5.txt")
                .Split(',')
                .Select(long.Parse);
        }
    }
}
