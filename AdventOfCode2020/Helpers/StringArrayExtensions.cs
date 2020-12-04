using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Helpers
{
    public static class StringArrayExtensions
    {
        public static IEnumerable<IEnumerable<T>> SplitByEmptyLines<T>(this string[] lines, Func<IEnumerable<string>, IEnumerable<T>> linesFactory)
        {
            var skip = 0;

            while (true)
            {
                var linesUntilNextEmptyLine = lines
                    .Skip(skip)
                    .TakeWhile(s => !string.IsNullOrEmpty(s));

                if (!linesUntilNextEmptyLine.Any())
                {
                    break;
                }

                yield return linesFactory(linesUntilNextEmptyLine);

                skip += linesUntilNextEmptyLine.Count() + 1;
            }
        }
    }

}
