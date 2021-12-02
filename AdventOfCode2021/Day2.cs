using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day2
    {
        private IEnumerable<Command> movingCommands;

        public Day2()
        {
            movingCommands = File.ReadAllLines(@"..\..\..\input2.txt")
                .Select(i => i.Split(' '))
                .Select(s => new Command(s[0], int.Parse(s[1])));
        }

        public int GetAnswear1()
        {
            var movingStrategy = new Dictionary<string, Action<Submarine, int>>()
            {
                { "forward", (sm, x) => sm.HorizontalPosition += x},
                { "up", (sm, x) => sm.Depth -= x},
                { "down", (sm, x) => sm.Depth += x},
            };

            return CalculatePositionFor(movingStrategy);
        }

        public int GetAnswear2()
        {
            var movingStrategy = new Dictionary<string, Action<Submarine, int>>()
            {
                { "forward", (sm, x) =>
                    {
                        sm.HorizontalPosition += x;
                        sm.Depth += sm.Aim * x;
                    }
                },
                { "up", (sm, x) => sm.Aim -= x},
                { "down", (sm, x) => sm.Aim += x},
            };
         
            return CalculatePositionFor(movingStrategy);
        }

        private int CalculatePositionFor(Dictionary<string, Action<Submarine, int>> movingStrategy)
        {
            var sm = new Submarine(movingStrategy);

            foreach (var command in movingCommands)
            {
                sm.Move(command);
            }

            return sm.Position;
        }
    }

    public record Command(string Move, int X);

    public class Submarine
    {
        private Dictionary<string, Action<Submarine, int>> movingStrategy;

        public int HorizontalPosition { get; set; }

        public int Depth { get; set; }

        public int Aim { get; set; }

        public int Position => HorizontalPosition * Depth;

        public Submarine(Dictionary<string, Action<Submarine, int>> movingStrategy)
        {
            this.movingStrategy = movingStrategy;
        }

        public void Move(Command command)
        {
            movingStrategy[command.Move](this, command.X);
        }
    }

}
