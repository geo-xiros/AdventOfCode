using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //        Console.WriteLine(new Day1());
            //Console.WriteLine(new Day2());
            //            Console.WriteLine(new Day3());
            //          Console.WriteLine(new Day4());
            var x = new Computer(LoadFile, 1);
            x.Run();

            Console.ReadKey();
        }
        private static int[] LoadFile()
        {
            return File
                .ReadAllText("..\\..\\input5.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();
        }
    }
}
