using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AdventOfCode2020.Tests
{
    [TestClass]
    public class Day5UnitTest
    {
        [DataTestMethod]
        [DataRow("FBFBBFFRLR", 44, 5)]
        [DataRow("BFFFBBFRRR", 70, 7)]
        [DataRow("FFFBBBFRRR", 14, 7)]
        [DataRow("BBFFBBFRLL", 102, 4)]
        public void ShoulReturnSeatRowAndColumnFromBoardingpass(string boardingPass, int expectedRow, int expectedColumn)
        {
            var (actualRow, actualColumn) = BoardingPassHelper.GetRowColumnFrom(boardingPass);

            Assert.AreEqual(expectedRow, actualRow);
            Assert.AreEqual(expectedColumn, actualColumn);
        }

        [DataTestMethod]
        [DataRow("FBFBBFFRLR", 357)]
        [DataRow("BFFFBBFRRR", 567)]
        [DataRow("FFFBBBFRRR", 119)]
        [DataRow("BBFFBBFRLL", 820)]
        public void ShoulReturnSeatIdFromBoardingpass(string boardingPass, int expectedSeatId)
        {
            var actualSeatId = BoardingPassHelper.GetSeatIdFrom(boardingPass);

            Assert.AreEqual(expectedSeatId, actualSeatId);
        }
        
        [TestMethod]
        public void TestBinarySpacePartitioning()
        {
            var bsp = new BinarySpacePartitioning(0, 7);

            var bsp0_3 = bsp.LowerHalf;
            Assert.AreEqual(0, bsp0_3.LowValue);
            Assert.AreEqual(3, bsp0_3.HiValue);

            var bsp0_1 = bsp0_3.LowerHalf;
            Assert.AreEqual(0, bsp0_1.LowValue);
            Assert.AreEqual(1, bsp0_1.HiValue);
            
            Assert.AreEqual(0, bsp0_1.LowerHalf.Value);
            Assert.AreEqual(1, bsp0_1.UpperHalf.Value);

            var bsp2_3 = bsp0_3.UpperHalf;
            Assert.AreEqual(2, bsp2_3.LowValue);
            Assert.AreEqual(3, bsp2_3.HiValue);
            Assert.AreEqual(2, bsp2_3.LowerHalf.Value);
            Assert.AreEqual(3, bsp2_3.UpperHalf.Value);

            
            var bsp4_7 = bsp.UpperHalf;
            Assert.AreEqual(4, bsp4_7.LowValue);
            Assert.AreEqual(7, bsp4_7.HiValue);

            var bsp4_5 = bsp4_7.LowerHalf;
            Assert.AreEqual(4, bsp4_5.LowValue);
            Assert.AreEqual(5, bsp4_5.HiValue);
            Assert.AreEqual(4, bsp4_5.LowerHalf.Value);
            Assert.AreEqual(5, bsp4_5.UpperHalf.Value);

            var bsp6_7 = bsp4_7.UpperHalf;
            Assert.AreEqual(6, bsp6_7.LowValue);
            Assert.AreEqual(7, bsp6_7.HiValue);
            Assert.AreEqual(6, bsp6_7.LowerHalf.Value);
            Assert.AreEqual(7, bsp6_7.UpperHalf.Value);
        }
    }

}
