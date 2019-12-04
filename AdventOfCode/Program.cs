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


            _wire1 = CreateWireSegments("R75,D30,R83,U83,L12,D49,R71,U7,L72");// R75,D30,R83,U83,L12,D49,R71,U7,L72");
            _wire2 = CreateWireSegments("U62,R66,U55,R34,D71,R55,D58,R83");// U62,R66,U55,R34,D71,R55,D58,R83");
            var x1 = FindDistances2(_wire1, _wire2);
            var x2 = FindDistances2(_wire2, _wire1);
            for (var i = 0; i < x1.Count; i++)
            {
                Console.WriteLine($"{x1[i]} + {x2[i]} = {x1[i] + x2[i]}");
            }
        }
        private IList<int> FindDistances()
        {
            var distances = new List<int>();
            foreach (var l1 in _wire1)
            {
                foreach (var l2 in _wire2)
                {
                    if (DoIntersect(l1, l2))
                    {
                        var intersection = Intersection(l1, l2);
                        var distance = CalculateManhattanDistance(new Point(0, 0), intersection);

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
            foreach (var l1 in wire1)
            {
                foreach (var l2 in wire2)
                {
                    if (DoIntersect(l1, l2))
                    {
                        var intersection = Intersection(l1, l2);
                        if ((intersection.X == l1.From.X &&
                             intersection.Y == l1.From.Y)||
                            (intersection.X == l1.To.X &&
                             intersection.Y == l1.To.Y) ||
                            (intersection.X == l2.From.X &&
                             intersection.Y == l2.From.Y) ||
                            (intersection.X == l2.To.X &&
                             intersection.Y == l2.To.Y))
                        {
                            continue;
                        }
                        var intersectionLength = new WireSegment(l1.From, intersection).Length;
                        distances.Add(distanceToIntersection + intersectionLength);
                    }
                }
                distanceToIntersection += l1.Length;
            }

            return distances;
        }
        private void LoadWires()
        {
            using (var file = File.OpenRead(@"G:\Development\Code\c#\AdventOfCode\AdventOfCode\Input\input3.txt"))
            {
                using (var reader = new StreamReader(file))
                {
                    _wire1 = CreateWireSegments(reader.ReadLine());
                    _wire2 = CreateWireSegments(reader.ReadLine());
                }
                file.Close();
            }
        }
        private Point Intersection(WireSegment ws1, WireSegment ws2)
        {
            int x = (ws1.From.X == ws1.To.X) ? x = ws1.From.X : x = ws2.From.X;
            int y = (ws1.From.Y == ws1.To.Y) ? y = ws1.From.Y : y = ws2.From.Y;

            return new Point(x, y);
        }
        public int CalculateManhattanDistance(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }
        private List<WireSegment> CreateWireSegments(string str)
        {
            var wireSegments = new List<WireSegment>();
            var wireMovements = str.Split(',');
            var currentPoint = new Point(0, 0);

            for (var i = 0; i < wireMovements.Length; i++)
            {
                var newPoint = MoveToPoint(currentPoint, wireMovements[i]);
                wireSegments.Add(new WireSegment(currentPoint, newPoint));
                currentPoint = newPoint;
            }

            return wireSegments;
        }
        private Point MoveToPoint(Point currentPoint, string moveTo)
        {
            var command = moveTo.Substring(0, 1);
            var position = moveTo.Substring(1);
            int dx = 0;
            int dy = 0;
            if (command == "R")
            {
                dx = currentPoint.X + int.Parse(position);
                dy = currentPoint.Y;
            }
            else if (command == "L")
            {
                dx = currentPoint.X - int.Parse(position);
                dy = currentPoint.Y;

            }
            else if (command == "D")
            {
                dx = currentPoint.X;
                dy = currentPoint.Y - int.Parse(position);

            }
            else if (command == "U")
            {
                dx = currentPoint.X;
                dy = currentPoint.Y + int.Parse(position);

            }

            return new Point(dx, dy);
        }
        private int Max(int x1, int x2)
        {
            return x1 > x2 ? x1 : x2;
        }
        private int Min(int x1, int x2)
        {
            return x1 > x2 ? x2 : x1;
        }
        // Given three colinear points p, q, r, the function checks if 
        // point q lies on line segment 'pr' 
        private bool OnSegment(Point p, Point q, Point r)
        {
            if (q.X <= Max(p.X, r.X) && q.X >= Min(p.X, r.X) &&
                q.Y <= Max(p.Y, r.Y) && q.Y >= Min(p.Y, r.Y))
                return true;

            return false;
        }

        // To find orientation of ordered triplet (p, q, r). 
        // The function returns following values 
        // 0 --> p, q and r are colinear 
        // 1 --> Clockwise 
        // 2 --> Counterclockwise 
        private int Orientation(Point p, Point q, Point r)
        {
            // See https://www.geeksforgeeks.org/orientation-3-ordered-points/ 
            // for details of below formula. 
            int val = (q.Y - p.Y) * (r.X - q.X) -
                      (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;  // colinear 

            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }

        // The main function that returns true if line segment 'p1q1' 
        // and 'p2q2' intersect. 
        private bool DoIntersect(WireSegment l1, WireSegment l2)
        {
            Point p1 = l1.From; Point q1 = l1.To;
            Point p2 = l2.From; Point q2 = l2.To;

            // Find the four orientations needed for general and 
            // special cases 
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases 
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1 
            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;

            // p1, q1 and q2 are colinear and q2 lies on segment p1q1 
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2 
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2 
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases 
        }
        public override string ToString()
        {
            return $"Day 3 => Answer A:{_answer1}, Answer B:{_answer2}";
        }

        private class WireSegment
        {
            public WireSegment(Point from, Point to)
            {
                From = from;
                To = to;
                Length = Math.Abs(From.X - To.X) + Math.Abs(From.Y - To.Y);
            }

            public Point From { get; set; }
            public Point To { get; set; }
            public int Length { get; set; }
            public override string ToString()
            {
                return $"({From}-{To})";
            }
        }


    }
}
