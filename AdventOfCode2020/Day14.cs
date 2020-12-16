using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day14 : Day<long>
    {
        public Day14() : base(14)
        {
        }

        protected override long GetAnswer1()
        {
            return new Computer()
                .Execute(input);
        }

        protected override long GetAnswer2()
        {
            return new ComputerWithMemoryDecoder()
                .Execute(input);
        }

        private record State(string mask, Dictionary<string, long> memory);

        private class Computer
        {
            public long Execute(string[] input)
            {
                var state = new State(null, new Dictionary<string, long>());

                state = input.Aggregate(state,
                    (state, command) => Run(command, state));

                return state.memory
                    .Select(m => m.Value)
                    .Aggregate((total, memory) => total + memory);
            }

            private State Run(string command, State state)
            {
                if (command.IndexOf("mask = ") >= 0)
                {
                    return UpdateMask(command, state);
                }

                return UpdateMemory(command, state);
            }

            protected virtual State UpdateMask(string command, State state)
            {
                var mask = Regex.Match(command, @"[X01]+").Value;
                return new State(mask, state.memory);
            }

            protected virtual State UpdateMemory(string command, State state)
            {
                var match = Regex.Match(command, @"mem\[(?<mem>[0-9]+)\] = (?<num>[0-9]+)");
                var memory = match.Groups["mem"].Value;
                var value = long.Parse(match.Groups["num"].Value);

                state.memory[memory] = ConvertWithMask(value, state.mask);
                return state;
            }

            protected long ConvertWithMask(long i, string mask)
            {
                var bwOr = Convert.ToInt64(mask.Replace('X', '0'), 2);
                var bwAnd = Convert.ToInt64(mask.Replace('X', '1'), 2);

                return i & bwAnd | bwOr;
            }
        }

        private class ComputerWithMemoryDecoder : Computer
        {
            protected override State UpdateMemory(string command, State state)
            {
                var match = Regex.Match(command, @"mem\[(?<mem>[0-9]+)\] = (?<num>[0-9]+)");
                var address = long.Parse(match.Groups["mem"].Value);
                var value = long.Parse(match.Groups["num"].Value);

                string result = ConvertWithMask(address, state.mask);
                GetAddressesFrom(result)
                    .ToList()
                    .ForEach(address => state.memory[address] = value);
                return state;
            }

            private new string ConvertWithMask(long address, string mask)
            {
                var addressMask = Convert.ToString(address, 2).PadLeft(36, '0');

                return new string(addressMask
                    .Zip(mask)
                    .Select(t => t.Second == 'X'
                        ? 'X'
                        : t.Second == '1'
                            ? '1'
                            : t.First)
                    .ToArray());
            }

            private IEnumerable<string> GetAddressesFrom(string result)
            {
                var bitCounts = result.Count(m => m.Equals('X'));
                for (long i = 0; i < Math.Pow(2, bitCounts); i++)
                {
                    var possibleBits = Convert.ToString(i, 2).PadLeft(bitCounts, '0');
                    yield return GetPossibleAddress(result, possibleBits);
                }
            }

            private string GetPossibleAddress(string mask, string possibleBits)
            {
                var newMask = new StringBuilder();

                for (int i = 0, j = 0; i < mask.Length; i++)
                {
                    if (mask[i] == 'X')
                    {
                        newMask.Append(possibleBits[j]);
                        j++;
                    }
                    else
                    {
                        newMask.Append(mask[i]);
                    }
                }

                return newMask.ToString();
            }
        }
    }
}
