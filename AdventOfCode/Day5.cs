﻿using System.Linq;
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
            var inputs = InputEnumerator(1, 5);

            computer.LoadProgram(LoadFile).SetInput(inputs).SetOutput((o) => _answer1 = o).Run();
            computer.LoadProgram(LoadFile).SetInput(inputs).SetOutput((o) => _answer2 = o).Run();
        }

        private IEnumerator<long> InputEnumerator(params long[] values)
        {
            foreach (var value in values)
            {
                yield return value;
            }
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }

        private IEnumerable<long> LoadFile()
        {
            return File
                .ReadAllText("..\\..\\inputs\\input5.txt")
                .Split(',')
                .Select(long.Parse);
        }
    }
}
