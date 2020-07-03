using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Threading;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

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
            //Console.WriteLine(new Day2());
            //Console.WriteLine(new Day5());
            //var day7 = new Day7();
            //day7.Run().ContinueWith(t => Console.WriteLine(day7));

            //Console.WriteLine(new Day6());
            //Console.WriteLine(new Day7());
            //Console.WriteLine(new Day8());
            //Console.WriteLine(new Day9());

            //Console.WriteLine(new Day11());
            var day13Part1 = new ArcadeCabinet();
            Console.WriteLine(day13Part1.Blocks);

            var day13Part2 = new ArcadeCabinet(2);
            Console.WriteLine(day13Part2.Score);

            Console.ReadKey();
        }

    }
    public class ArcadeCabinet
    {
        private IntComputer computer = new IntComputer();
        private Dictionary<Point, long> screen = new Dictionary<Point, long>();
        private long paddleX;
        private long joystickInput;
        private long x;
        private long y;

        public long Blocks => screen.Where(s => s.Value == 2).Count();
        public long Score { get; set; }

        public ArcadeCabinet()
        {
            computer.LoadProgram(LoadFile)
                .SetInput(JoystickInput())
                .SetOutput(GetX)
                .Run();
        }

        public ArcadeCabinet(long quarters)
        {
            computer.LoadProgram(LoadFile)
                .SetInput(JoystickInput())
                .SetOutput(GetX)
                .SetQuarters(quarters)
                .Run();
        }
        private IEnumerable<long> LoadFile()
        {
            return File
                .ReadAllText("..\\..\\inputs\\input13.txt")
                .Split(',')
                .Select(long.Parse);
        }

        private void GetX(long output)
        {
            x = output;
            computer.SetOutput(GetY);
        }

        private void GetY(long output)
        {
            y = output;
            computer.SetOutput(GetTile);
        }

        private void GetTile(long output)
        {
            if (x == -1 && y == 0)
            {
                Score = output;
                RenderScore();
            }
            else
            {
                screen[new Point((int)x, (int)y)] = output;
                Render(output);

                if (output == 4)
                {
                    if (x > paddleX)
                    {
                        joystickInput = 1;
                    }
                    else if (x < paddleX)
                    {
                        joystickInput = -1;
                    }
                }
                else if (output == 3)
                {
                    paddleX = x;
                }
                else
                {
                    joystickInput = 0;
                }
            }

            computer.SetOutput(GetX);
        }

        private void Render(long output)
        {
            Console.SetCursorPosition((int)x, (int)y + 1);
            Console.Write(output == 0L ? " " : output.ToString());
        }

        private void RenderScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"Score : {Score}");
        }

        private IEnumerator<long> JoystickInput()
        {
            while (true)
            {
                yield return joystickInput;
            }
        }
    }
}