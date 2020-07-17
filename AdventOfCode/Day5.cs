using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;

namespace AdventOfCode
{
    public class Day5
    {
        private long _answer1;
        private long _answer2;
        public Day5()
        {
            var computer = new IntComputer();

            computer.LoadProgram(FileUtils.LoadDataFor(5))
                .SetInput(() => 1)
                .SetOutput((o) => _answer1 = o)
                .Run();

            computer.LoadProgram(FileUtils.LoadDataFor(5))
                .SetInput(() => 5)
                .SetOutput((o) => _answer2 = o)
                .Run();
        }


        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}
