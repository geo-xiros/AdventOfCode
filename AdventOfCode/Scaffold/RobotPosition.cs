using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AdventOfCode
{
    public class RobotPosition
    {
        public char Direction { get; }
        public Point Position { get; }
        public RobotPosition NextPosition { get; private set; }

        public RobotPosition(char direction, Point position)
        {
            this.Direction = direction;
            this.Position = position;
        }

        public RobotPosition Move(Func<Point, bool> isValidPosition)
        {
            NextPosition = Movements
                .Where(rp => isValidPosition(rp.Position))
                .OrderByDescending(p => p.Direction == Direction)
                .FirstOrDefault();

            return NextPosition;
        }

        private IEnumerable<RobotPosition> Movements
            => PointsAroundPosition.Zip("^v<>".ToCharArray(), (p, d) => new RobotPosition(d, p));

        private Point[] PointsAroundPosition
            => new Point[]
                  {
                    new Point(Position.X - 1, Position.Y),
                    new Point(Position.X + 1, Position.Y),
                    new Point(Position.X, Position.Y - 1),
                    new Point(Position.X, Position.Y + 1)
                  };

        internal string GetTurn()
        {
            Dictionary<(char, char), char> directions = new Dictionary<(char, char), char>
            {
                [('^', '>')] = 'R',
                [('>', 'v')] = 'R',
                [('v', '<')] = 'R',
                [('<', '^')] = 'R',
                [('^', '<')] = 'L',
                [('<', 'v')] = 'L',
                [('v', '>')] = 'L',
                [('>', '^')] = 'L',
            };

            return $"\n{directions[(Direction, NextPosition.Direction)]},";
        }
    }
}