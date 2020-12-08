using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Helpers
{
    public static class IEnumerableExtensions
    {
        public static Dictionary<int, T> ToIndexedDictionary<T>(this IEnumerable<T> list)
        {
            return list
                .Select((i, ix) => (Item: i, Index: ix))
                .ToDictionary(i => i.Index, i => i.Item);
        }
    }
}
