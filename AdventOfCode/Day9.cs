using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    public class Day9
    {
        private long _answer1;
        private long _answer2;

        public Day9()
        {
            var computer = new IntComputer();

            computer.LoadProgram(FileUtils.LoadDataFor(9)).SetInput(() => 1).SetOutput((o) => _answer1 = o).Run();
            computer.LoadProgram(FileUtils.LoadDataFor(9)).SetInput(() => 2).SetOutput((o) => _answer2 = o).Run();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}