using AdventOfCode2020.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AdventOfCode2020.Tests
{
    [TestClass]
    public class Day6UnitTest
    {
        private string[] input;
        private Day6 day;

        [TestInitialize]
        public void Setup()
        {
            input = new string[] {
                "abc",
                "",
                "a", "b", "c",
                "",
                "ab", "ac",
                "",
                "a", "a", "a", "a",
                "",
                "b"};
            day = new Day6();
        }

        [TestMethod]
        public void TestPart1()
        {
            var actualResult = input
                .SplitByEmptyLines(day.GetAffirmativeAnswersFromAnyone)
                .SelectMany(s => s)
                .Count();

            Assert.AreEqual(11, actualResult);
        }

        [TestMethod]
        public void TestPart2()
        {
            var actualResult = input
                .SplitByEmptyLines(day.GetAffirmativeAnswersFromEveryone)
                .SelectMany(s => s)
                .Count();

            Assert.AreEqual(6, actualResult);
        }
    }

}
