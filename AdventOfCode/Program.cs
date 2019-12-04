using System;
using System.Collections.Generic;
using System.Drawing;
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
            //Console.WriteLine(new Day1());
            //Console.WriteLine(new Day2());
            Console.WriteLine(new Day3());
            Console.ReadKey();
        }
    }

    public class Day3
    {
        private int _answer1;
        private int _answer2;

        private List<WireSegment> _wire1;
        private List<WireSegment> _wire2;

        public Day3()
        {
            LoadWires();

            _answer1 = FindDistances().Min();

            //_wire1 = CreateWireSegments("R8,U5,L5,D3");
            //_wire2 = CreateWireSegments("U7,R6,D4,L4");
            _wire1 = CreateWireSegments("R75,D30,R83,U83,L12,D49,R71,U7,L72");
            _wire2 = CreateWireSegments("U62,R66,U55,R34,D71,R55,D58,R83");

            var x1 = FindDistances2(_wire1, _wire2).OrderBy(i => i).ToList();
            var x2 = FindDistances2(_wire2, _wire1).OrderBy(i => i).ToList();

            for (var i = 0; i < x1.Count; i++)
            {
                Console.WriteLine($"{x1[i]} + {x2[i]} = {x1[i] + x2[i]}");
            }
        }
        private IList<int> FindDistances()
        {
            var distances = new List<int>();
            foreach (var wireSegment1 in _wire1)
            {
                foreach (var wireSegment2 in _wire2)
                {
                    if (wireSegment1.IntersectsWith(wireSegment2, out var intersection))
                    {
                        var distance = CalculateManhattanDistance(intersection);

                        if (distance != 0)
                        {
                            distances.Add(distance);
                        }
                    }
                }
            }

            return distances;
        }
        private IList<int> FindDistances2(List<WireSegment> wire1, List<WireSegment> wire2)
        {
            var distances = new List<int>();
            int distanceToIntersection = 0;
            foreach (var wireSegment1 in wire1)
            {
                foreach (var wireSegment2 in wire2)
                {
                    if (wireSegment1.IntersectsWith(wireSegment1, out var intersection))
                    {
                        Console.WriteLine($"{wireSegment1} {wireSegment2} - {intersection}");
                        var intersectionLength = new WireSegment(wireSegment1.From, intersection).Length;
                        distances.Add(distanceToIntersection + intersectionLength);
                    }
                }
                distanceToIntersection += wireSegment1.Length;
            }

            return distances;
        }

        public int CalculateManhattanDistance(Point point1)
        {
            Point point2 = new Point(0, 0);
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }

        #region Initialize wires
        private void LoadWires()
        {
            using (var file = File.OpenRead(@"C:\Users\g.xiros\Documents\coding\adventofcode\AdventOfCode\Input\input3.txt"))
            {
                using (var reader = new StreamReader(file))
                {
                    _wire1 = CreateWireSegments(reader.ReadLine());
                    _wire2 = CreateWireSegments(reader.ReadLine());
                }
                file.Close();
            }
        }

        private List<WireSegment> CreateWireSegments(string str)
        {
            var wireSegments = new List<WireSegment>();
            var wireMovements = str.Split(',');
            var fromPoint = new Point(0, 0);

            for (var i = 0; i < wireMovements.Length; i++)
            {
                var wireSegment = new WireSegment(fromPoint, wireMovements[i]);
                wireSegments.Add(wireSegment);
                fromPoint = wireSegment.To;
            }

            return wireSegments;
        }

        private class WireSegment
        {
            public WireSegment(Point from, string moveTo) : this(from, CalculateToPoint(from, moveTo)) { }
            public WireSegment(Point from, Point to)
            {
                From = from;
                To = to;
                Length = Math.Abs(From.X - To.X) + Math.Abs(From.Y - To.Y);
            }

            public Point From { get; set; }
            public Point To { get; set; }
            public int Length { get; set; }

            public static Point CalculateToPoint(Point from, string moveTo)
            {
                var command = moveTo.Substring(0, 1);
                var position = moveTo.Substring(1);
                int dx = 0;
                int dy = 0;
                if (command == "R")
                {
                    dx = from.X + int.Parse(position);
                    dy = from.Y;
                }
                else if (command == "L")
                {
                    dx = from.X - int.Parse(position);
                    dy = from.Y;

                }
                else if (command == "D")
                {
                    dx = from.X;
                    dy = from.Y - int.Parse(position);

                }
                else if (command == "U")
                {
                    dx = from.X;
                    dy = from.Y + int.Parse(position);

                }

                return new Point(dx, dy);
            }


            public bool IntersectsWith(WireSegment wire, out Point intersectionPoint)
            {
                int x = 0;
                int y = 0;

                if ((wire.From.X == wire.To.X) && (this.From.Y == this.To.Y))
                {
                    x = wire.From.X;
                    y = this.From.Y;
                }
                else if ((wire.From.Y == wire.To.Y) && (this.From.X == this.To.X))
                {
                    x = this.From.X;
                    y = wire.From.Y;
                }

                intersectionPoint = new Point(x, y);

                if (x == 0 && y == 0)
                {
                    return false;
                }

                return wire.IntersectsWith(intersectionPoint) &&
                       this.IntersectsWith(intersectionPoint);
            }
            private bool IntersectsWith(Point p)
            {
                if (From.X == To.X)
                {
                    if (From.Y < To.Y)
                    {
                        return (p.Y > From.Y && p.Y < To.Y);
                    }
                    else
                    {
                        return (p.Y > To.Y && p.Y < From.Y);
                    }
                }
                else if (From.Y == To.Y)
                {
                    if (From.X < To.X)
                    {
                        return (p.X > From.X && p.X < To.X);
                    }
                    else
                    {
                        return (p.X > To.X && p.X < From.X);
                    }
                }

                return false;
            }

            public override string ToString()
            {
                return $"({From}-{To})";
            }
        }
        #endregion

        public override string ToString()
        {
            return $"Day 3 => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}
