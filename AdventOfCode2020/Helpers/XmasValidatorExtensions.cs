using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Helpers
{
    public static class XmasValidatorExtensions
    {
        public static IEnumerable<IEnumerable<long>> ContiguousSetsSumOf(this long[] sequence, long number)
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

        public static IEnumerable<(IEnumerable<long> Preamble, long NextNumber)> SplitPreambles(this long[] sequense, int preambleLength)
        {
            for (var i = 0; i < sequense.Length - preambleLength; i++)
            {
                yield return (sequense.Skip(i).Take(preambleLength), sequense[i + preambleLength]);
            }
        }

        public static IEnumerable<(IEnumerable<long> Preamble, long NextNumber)> FindInvalidNumbers(this IEnumerable<(IEnumerable<long> Preamble, long NextNumber)> sequense)
        {
            return sequense.Where(t => !t.IsValid());
        }

        public static bool IsValid(this (IEnumerable<long> Preamble, long NextNumber) preamblesTuple)
        {
            var preamble = preamblesTuple.Preamble.ToArray();

            for (var i = 0; i < preamble.Length; i++)
            {
                for (var j = i + 1; j < preamble.Length; j++)
                {
                    if ((preamble[i] + preamble[j]) == preamblesTuple.NextNumber)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
