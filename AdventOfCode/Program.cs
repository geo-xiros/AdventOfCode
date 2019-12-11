using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(new Day1());
            Console.WriteLine(new Day2());
            //Console.WriteLine(new Day3());
            //Console.WriteLine(new Day4());
            //Console.WriteLine(new Day5());
            Console.WriteLine(new Day6());
            //Console.WriteLine(new Day8());

            var x = new Computer()
                .LoadProgram(() => new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 })
                .Run();
            Console.ReadKey();
        }
    }
}