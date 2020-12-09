using AdventOfCode2020.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Tests
{
    [TestClass]
    public class Day9UnitTest
    {
        private long[] sequence;

        [TestInitialize]
        public void Initialize()
        {
            sequence = new long[] { 35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576 };
        }

        [TestMethod]
        public void TestPreamble()
        {
            var input = sequence.SplitPreambles(5).ToList();
            var preambles = input.Select(p => string.Join(',', p.Preamble)).ToArray();
            var numbers = input.Select(p => string.Join(',', p.NextNumber)).ToArray();

            var expectedPreamble = new int[][]
            {
                new int[]{ 35, 20, 15, 25, 47 },
                new int[]{ 20, 15, 25, 47, 40 },
                new int[]{ 15, 25, 47, 40, 62 },
                new int[]{ 25, 47, 40, 62, 55 },
                new int[]{ 47, 40, 62, 55, 65 },
                new int[]{ 40, 62, 55, 65, 95 },
                new int[]{ 62, 55, 65, 95, 102 },
                new int[]{ 55, 65, 95, 102, 117 },
                new int[]{ 65, 95, 102, 117, 150 },
                new int[]{ 95, 102, 117, 150, 182 },
                new int[]{ 102, 117, 150, 182, 127 },
                new int[]{ 117, 150, 182, 127, 219 },
                new int[]{ 150, 182, 127, 219, 299 },
                new int[]{ 182, 127, 219, 299, 277 },
                new int[]{ 127, 219, 299, 277, 309 }
            }.Select(i => string.Join(',', i)).ToArray();

            var expectedNumbers = new int[] { 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576 }
                .Select(i => string.Join(',', i)).ToArray();


            Assert.AreEqual(string.Join(',', expectedPreamble), string.Join(',', preambles));
            Assert.AreEqual(string.Join(',', expectedNumbers), string.Join(',', numbers));

            var firstInvalidNumber = sequence
                .SplitPreambles(5)
                .FindInvalidNumbers()
                .First()
                .NextNumber;

            Assert.AreEqual(127, firstInvalidNumber);

        }
    }


}
