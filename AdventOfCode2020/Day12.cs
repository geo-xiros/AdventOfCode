using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day12 : Day<int>
    {
        private (char Action, int Value)[] actions;

        public Day12() : base(12)
        {
            actions = input.Select(l =>
            {
                var matches = Regex.Match(l, @"(?<action>[FNESWLR]+)(?<value>[0-9]+)");
                var action = matches.Groups["action"].Value[0];
                var value = matches.Groups["value"].Value;

                return (Action: action, Value: int.Parse(value));
            }).ToArray();
        }

        protected override int GetAnswer1()
        {
            var ship = new ShipNavigation();

            foreach (var action in actions)
            {
                ship.Move(action.Action, action.Value);
            }

            return ship.ManhattanDistance;
        }

        protected override int GetAnswer2()
        {
            var ship = new ShipNavigation();
            var waypoint = new WaypointNavigation(ship);

            foreach (var action in actions)
            {
                waypoint.Move(action.Action, action.Value);
            }

            return ship.ManhattanDistance;
        }

        public abstract class NavigationComputer
        {
            protected (int X, int Y) direction;
            protected Dictionary<char, (int X, int Y)> directionsOffsets;

            public record Coordinates(int X, int Y);
            public (int X, int Y) Position { get; set; }

            public NavigationComputer()
            {
                Position = (0, 0);
                direction = (1, 0);

                directionsOffsets = new Dictionary<char, (int X, int Y)>()
                {
                    ['N'] = (0, 1),
                    ['S'] = (0, -1),
                    ['E'] = (1, 0),
                    ['W'] = (-1, 0)
                };
            }

            public void Move(char action, int value)
            {
                switch (action)
                {
                    case 'R':
                        TurnRight(value);
                        break;
                    case 'L':
                        TurnLeft(value);
                        break;
                    case 'F':
                        MoveForward(value);
                        break;
                    default:
                        Position = (
                            Position.X + directionsOffsets[action].X * value,
                            Position.Y + directionsOffsets[action].Y * value);
                        break;
                }
            }

            public abstract void MoveForward(int value);

            public virtual void TurnRight(int degrees) => direction = RotateR(direction, degrees);

            public virtual void TurnLeft(int degrees) => direction = RotateL(direction, degrees);

            protected (int X, int Y) RotateL((int X, int Y) coordinates, int degrees)
            {
                for (var i = 0; i < degrees / 90; i++)
                {
                    coordinates = (-coordinates.Y, coordinates.X);
                }

                return coordinates;
            }

            protected (int X, int Y) RotateR((int X, int Y) coordinates, int degrees)
            {
                for (var i = 0; i < degrees / 90; i++)
                {
                    coordinates = (coordinates.Y, -coordinates.X);
                }

                return coordinates;
            }
        }

        public class ShipNavigation : NavigationComputer
        {
            public int ManhattanDistance => Math.Abs(Position.X) +
                Math.Abs(Position.Y);

            public override void MoveForward(int value)
            {
                Position = (
                    Position.X + (direction.X * value),
                    Position.Y + (direction.Y * value));
            }

        }

        public class WaypointNavigation : NavigationComputer
        {
            private ShipNavigation ship;

            public WaypointNavigation(ShipNavigation ship)
            {
                this.ship = ship;
                Position = (10, 1);
            }

            public override void MoveForward(int value)
            {
                ship.Move(Position.X >= 0 ? 'E' : 'W', Math.Abs(value * Position.X));
                ship.Move(Position.Y >= 0 ? 'N' : 'S', Math.Abs(value * Position.Y));
            }

            public override void TurnRight(int degrees)
            {
                Position = RotateR(Position, degrees);
            }

            public override void TurnLeft(int degrees)
            {
                Position = RotateL(Position, degrees);
            }
        }
    }
}
