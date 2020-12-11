using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day10 : Day<long>
    {
        private int[] adapters;

        public Day10() : base(10)
        {
            adapters = input
                .Select(int.Parse)
                .Prepend(0)
                .OrderBy(s => s)
                .ToArray();
        }

        protected override long GetAnswer1()
        {
            var diff = new Dictionary<int, int>();

            adapters.Aggregate(0, (p, c) =>
            {
                var dif = c - p;
                if (!diff.TryGetValue(dif, out var difCounter))
                {
                    diff.Add(dif, 0);
                }
                diff[dif]++;
                return c;
            });

            var onesCount = diff.Where(kv => kv.Key == 1).Select(kv => kv.Value).Sum();
            var fivesCount = diff.Where(kv => kv.Key == 3).Select(kv => kv.Value).Sum() + 1;

            return onesCount * fivesCount;
        }

        protected override long GetAnswer2()
        {
            return new TreeAdapter(adapters)
                .Count;
        }

        private class TreeAdapter
        {
            private int index;
            private int[] sequense;
            private TreeAdapter adapter1;
            private TreeAdapter adapter2;
            private TreeAdapter adapter3;

            public TreeAdapter Adapter1 => adapter1
                ?? (adapter1 = TreeAdapter.Find(1, index, sequense));

            public TreeAdapter Adapter2 => adapter2
                ?? (adapter2 = TreeAdapter.Find(2, index, sequense));
            public TreeAdapter Adapter3 => adapter3
                ?? (adapter3 = TreeAdapter.Find(3, index, sequense));

            public int Value => sequense[index];

            private long count = 0L;
            public long Count
            {
                get
                {
                    if (count == 0)
                    {
                        if (Adapter1 == null && Adapter2 == null && Adapter3 == null)
                        {
                            count = 1L;
                        }
                        else
                        {
                            count = (Adapter1?.Count ?? 0) +
                                (Adapter2?.Count ?? 0) +
                                (Adapter3?.Count ?? 0);
                        }
                    }

                    return count;
                }
            }

            public TreeAdapter(int[] sequense)
            {
                this.sequense = sequense;
            }

            public TreeAdapter(int index, int[] sequense) : this(sequense)
            {
                this.index = index;
            }

            private static Dictionary<int, TreeAdapter> keyValuePairs = new Dictionary<int, TreeAdapter>();

            private static TreeAdapter Find(int nextIndex, int index, int[] sequense)
            {
                var newIndex = index + nextIndex;

                if (newIndex < sequense.Length &&
                    sequense[newIndex] - sequense[index] <= 3)
                {
                    if (!keyValuePairs.TryGetValue(newIndex, out var adapter))
                    {
                        adapter = new TreeAdapter(newIndex, sequense);
                        keyValuePairs.Add(newIndex, adapter);
                    }

                    return adapter;
                }

                return null;
            }

        }
    }
}
