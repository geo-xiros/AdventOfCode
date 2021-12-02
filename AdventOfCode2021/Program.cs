using System;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    class Program
    {

        static void Main(string[] args)
        {
            var day1 = new Day1();
            Console.WriteLine(day1.GetAnswear1());
            Console.WriteLine(day1.GetAnswear2());

            var day2 = new Day2();
            Console.WriteLine(day2.GetAnswear1());
            Console.WriteLine(day2.GetAnswear2());
        }

    }

}
