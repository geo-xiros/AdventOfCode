using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.IO;

namespace AdventOfCode
{

    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(new Day1());
            //Console.WriteLine(new Day2());
            //Console.WriteLine(new Day3());
            //Console.WriteLine(new Day4());
            //Console.WriteLine(new Day5());
            //Console.WriteLine(new Day6());
            //Console.WriteLine(new Day7());
            //Console.WriteLine(new Day8());
            Console.WriteLine(new Day9());


            Console.ReadKey();
        }

    }

    public class Day9
    {
        private long _answer1;
        private long _answer2;

        public Day9()
        {
            var computer = new IntComputer()
                .LoadProgram(LoadFile)// testing () => new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 })
                .Run();
            _answer1 = computer.Output2.Last();
        }
        private IEnumerable<long> LoadFile()
        {
            return File
                .ReadAllText("..\\..\\input9.txt")
                .Split(',')
                .Select(long.Parse);
        }
        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}