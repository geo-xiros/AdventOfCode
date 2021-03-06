﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day2
    {
        private long _answer1;
        private long _answer2;
        public Day2()
        {
            var computer = new IntComputer();

            _answer1 = computer.LoadProgram(FileUtils.LoadDataFor(2)).Using(12, 2).Run().MemoryZeroAddress;
            _answer2 = FindVerbAndNounForOutput(computer, 19690720);

        }

        private int FindVerbAndNounForOutput(IntComputer computer, int output)
        {
            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    if (computer
                        .LoadProgram(FileUtils.LoadDataFor(2))
                        .Using(noun, verb)
                        .Run()
                        .MemoryZeroAddress == output)
                    {
                        return (100 * noun) + verb;
                    }
                }
            }

            return 0;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}
