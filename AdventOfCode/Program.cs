using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Day2());

            Console.ReadKey();
        }
    }
    class Day2
    {
        private long _Answer1;
        private long _Answer2;

        public Day2()
        {
            var program = File
                .ReadAllText(@"C:\Users\George\source\repos\AdventOfCode\AdventOfCode\Input\input2.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            program[1] = 12;
            program[2] = 2;

            RunProgram(program);
            _Answer1 = program[0];

        }
        private void RunProgram(int[] program)
        {
            int position1 = 0;
            int position2 = 0;
            int position3 = 0;

            for (var i = 0; i < program.Length; i += 4)
            {
                switch (program[i])
                {
                    case 1:
                        position1 = program[i + 1];
                        position2 = program[i + 2];
                        position3 = program[i + 3];
                        program[position3] = program[position1] + program[position2];
                        break;
                    case 2:
                        position1 = program[i + 1];
                        position2 = program[i + 2];
                        position3 = program[i + 3];
                        program[position3] = program[position1] * program[position2];
                        break;
                    case 99:
                        return;
                    default:
                        break;
                }
            }
        }
        public override string ToString()
        {
            return $"Day 2 => Answer A:{_Answer1}, Answer B:{_Answer2}";
        }
    }
}
