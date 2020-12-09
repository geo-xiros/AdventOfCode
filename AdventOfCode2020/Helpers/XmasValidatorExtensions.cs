using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Helpers
{
    public static class XmasValidatorExtensions
    {

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
