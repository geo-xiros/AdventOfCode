using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Tests
{
    [TestClass]
    public class Day10UnitTest
    {
        private int[] sequence1;
        private int[] sequence2;

        [TestInitialize]
        public void Initialize()
        {
            sequence1 = new int[] { 16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4 };
            sequence2 = new int[] { 28, 33, 18, 42, 31, 14, 46, 20, 48, 47, 24, 23, 49, 45, 19, 38, 39, 11, 1, 32, 25, 35, 8, 17, 7, 9, 4, 2, 34, 10, 3 };
        }

        [TestMethod]
        public void TestPreamble()
        {
            var diff = new Dictionary<int, int>();

            var adapters = sequence1
                .OrderBy(s => s)
                .Aggregate(0, (p, c) =>
                {
                    var dif = c - p;
                    if (!diff.TryGetValue(dif, out var difCounter))
                    {
                        diff.Add(dif, 0);
                    }
                    diff[dif]++;
                    return c;
                });
            Assert.AreEqual(7, diff.Where(kv => kv.Key == 1).Select(kv => kv.Value).Sum());
            Assert.AreEqual(4, diff.Where(kv => kv.Key == 3).Select(kv => kv.Value).Sum());
        }

        [TestMethod]
        public void TestPreamble2()
        {
            var diff = new Dictionary<int, int>();

            var adapters = sequence2
                .OrderBy(s => s)
                .Aggregate(0, (p, c) =>
                {
                    var dif = c - p;
                    if (!diff.TryGetValue(dif, out var difCounter))
                    {
                        diff.Add(dif, 0);
                    }
                    diff[dif]++;
                    return c;
                });
            Assert.AreEqual(22, diff.Where(kv => kv.Key == 1).Select(kv => kv.Value).Sum());
            Assert.AreEqual(9, diff.Where(kv => kv.Key == 3).Select(kv => kv.Value).Sum());
        }

    }

    
}
