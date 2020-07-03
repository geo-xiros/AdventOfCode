using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    public class Day3
    {
        private int _answer1;
        private int _answer2;

        private List<WireSegment> _wire1;
        private List<WireSegment> _wire2;
        private Dictionary<Point, int> DistancesByIntersections;
        public Day3()
        {
            LoadWires();

            _answer1 = FindDistances().Min();

            DistancesByIntersections = new Dictionary<Point, int>();

            FindDistances2(_wire1, _wire2);
            FindDistances2(_wire2, _wire1);

            _answer2 = DistancesByIntersections.Values.Min();

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
        private void FindDistances2(List<WireSegment> wire1, List<WireSegment> wire2)
        {
            int distanceToIntersection = 0;

            foreach (var wireSegment1 in wire1)
            {
                foreach (var wireSegment2 in wire2)
                {
                    if (wireSegment1.IntersectsWith(wireSegment2, out var intersection))
                    {
                        var intersectionLength = new WireSegment(wireSegment1.From, intersection).Length;

                        DistancesByIntersections.TryGetValue(intersection, out int totalDistance);
                        DistancesByIntersections[intersection] = totalDistance + distanceToIntersection + intersectionLength;
                    }
                }
                distanceToIntersection += wireSegment1.Length;
            }
        }

        public int CalculateManhattanDistance(Point point1)
        {
            Point point2 = new Point(0, 0);
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }

        #region Initialize wires
        private void LoadWires()
        {
            using (var file = File.OpenRead("..\\..\\inputs\\input3.txt"))
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
                        return (p.Y >= From.Y && p.Y <= To.Y);
                    }
                    else
                    {
                        return (p.Y >= To.Y && p.Y <= From.Y);
                    }
                }
                else if (From.Y == To.Y)
                {
                    if (From.X < To.X)
                    {
                        return (p.X >= From.X && p.X <= To.X);
                    }
                    else
                    {
                        return (p.X >= To.X && p.X <= From.X);
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
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}
