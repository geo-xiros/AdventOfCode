using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Scaffold
    {
        private IntComputer computer = new IntComputer();
        private Dictionary<Point, char> screen = new Dictionary<Point, char>();
        private List<Point> intersections = new List<Point>();
        private int x;
        private int y;

        public int SumOfAlignmentParamters =>
            intersections.Select(i => i.X * i.Y).Sum();

        public Scaffold()
        {
            computer.LoadProgram(FileUtils.LoadDataFor(17))
                .SetOutput(CameraOutput)
                .Run();

            FindIntersections();

            //Print(true);

            RobotPosition currentPosition = screen.Where(s => !(s.Value == '#' || s.Value == '.')).Select(s => new RobotPosition(s.Value, s.Key)).FirstOrDefault();
            List<Point> scaffoldPath = screen.Where(s => s.Value == 35).Select(s => s.Key).ToList();
            scaffoldPath.AddRange(intersections);

            FindScaffoldPath(scaffoldPath, currentPosition);

            Console.WriteLine(GetDust());
        }
        public long GetDust()
        {
            var movements = GetAsciiInput(new string[] { "A,B,A,B,C,C,B,C,B,A", "R,12,L,8,R,12", "R,8,R,6,R,6,R,8", "R,8,L,8,R,8,R,4,R,4" });
            var asciiInput = new Queue<long>(movements);

            computer.LoadProgram(FileUtils.LoadDataFor(17))
                    .SetMemory(2)
                    .SetOutput(DustCollected)
                    .SetInput(() => asciiInput.Dequeue())
                    .Run();

            return dustCollected;
        }
        private long dustCollected;

        private void DustCollected(long dust)
        {
            Console.Write((char)dust);
            dustCollected = dust;
        }

        private IEnumerable<long> GetAsciiInput(string[] inputs)
        {

            foreach (var input in inputs)
            {
                foreach (var c in input.ToCharArray())
                {
                    yield return (long)c;
                }

                yield return 10;
            }

            yield return 'n';

            yield return 10;
        }

        public void FindScaffoldPath(List<Point> validPath, RobotPosition currentPosition)
        {
            RobotPosition newPosition = currentPosition;

            while ((newPosition = newPosition.Move(position => validPath.Contains(position))) != null)
            {
                validPath.Remove(newPosition.Position);
            }

            newPosition = currentPosition;

            Console.WriteLine();

            while (newPosition != null)
            {
                if (newPosition.NextPosition != null)
                {
                    if (newPosition.Direction != newPosition.NextPosition.Direction)
                    {
                        Console.Write(newPosition.GetTurn());
                    }
                    Console.Write("1,");
                }
                //Console.SetCursorPosition(newPosition.Position.Y, newPosition.Position.X);
                //Console.Write(newPosition.Direction);

                newPosition = newPosition.NextPosition;
            }
        }

        private void CameraOutput(long ascii)
        {
            if (ascii == 10)
            {
                y = 0;
                x++;
            }
            else
            {
                var p = new Point(x, y++);
                screen[p] = ascii == 35 ? '#' : ascii == 46 ? '.' : (char)ascii;
            }
        }

        private void FindIntersections()
        {
            var path = screen
                .Where(s => s.Value == 35)
                .Select(s => s.Key).ToList();

            var availableTurnsForEachPathPoint = path
                .ToDictionary(p => p, p => PointsAround(p)
                .Join(path, i => i, pa => pa, (i, pa) => pa));

            intersections = availableTurnsForEachPathPoint
                .Where(i => HavingMoreThanTwoTurns(i.Value))
                .Select(i => i.Key).ToList();
        }

        private bool HavingMoreThanTwoTurns(IEnumerable<Point> points)
            => points.Count() > 2;

        private Point[] PointsAround(Point point)
            => new Point[]
                  {
                    new Point(point.X - 1, point.Y),
                    new Point(point.X + 1, point.Y),
                    new Point(point.X, point.Y - 1),
                    new Point(point.X, point.Y + 1)
                  };

        public void Print(bool withIntersections = false)
        {
            foreach (var p in screen)
            {
                Console.SetCursorPosition(p.Key.Y, p.Key.X);
                Console.Write(p.Value);
            }

            if (!withIntersections)
            {
                return;
            }

            foreach (var p in intersections)
            {
                Console.SetCursorPosition(p.Y, p.X);
                Console.Write("O");
            }
        }

    }
}