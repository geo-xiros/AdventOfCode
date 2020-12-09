using AdventOfCode2020.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day9 : Day<long>
    {
        private long[] sequence;
        private long answer1;

        public Day9() : base(9)
        {
            sequence = input
                .Select(long.Parse)
                .ToArray();

            answer1 = sequence
                .SplitPreambles(25)
                .FindInvalidNumbers()
                .First()
                .NextNumber;
        }

        protected override long GetAnswer1()
        {
            return answer1;
        }

        protected override long GetAnswer2()
        {
            var contiguousSet = ContiguousSetsSumOf(sequence, answer1)
                .First();

            return contiguousSet.Min() + contiguousSet.Max();
        }

        private IEnumerable<IEnumerable<long>> ContiguousSetsSumOf(long[] sequence, long number)
        {
            for (int j = 0; j < sequence.Length; j++)
            {
                for (int i = j; i < sequence.Length; i++)
                {
                    var subSequense = sequence.Skip(j).Take(i - j + 1);
                    if (subSequense.Sum() == number)
                    {
                        yield return subSequense;
                    }
                    else if (subSequense.Sum() > number)
                    {
                        break;
                    }
                }

            }
        }
    }
}
