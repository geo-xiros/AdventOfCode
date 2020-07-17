using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Threading;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace AdventOfCode
{

    class Program
    {
        static void Main(string[] args)
        {
           // Console.WriteLine(new Day1());
            //Console.WriteLine(new Day2());
            //Console.WriteLine(new Day3());
            //Console.WriteLine(new Day4());
            //Console.WriteLine(new Day5());
            //var day7 = new Day7();
            //day7.Run().ContinueWith(t => Console.WriteLine(day7));

            Console.WriteLine(new Day6());
            //Console.WriteLine(new Day8());
            //Console.WriteLine(new Day9());

            //Console.WriteLine(new Day11());
            //Console.WriteLine(new Day13());

            //var s = new Scaffold();
            ////s.Print();
            //Console.WriteLine(s.SumOfAlignmentParamters);

            Console.ReadKey();
        }

    }

}