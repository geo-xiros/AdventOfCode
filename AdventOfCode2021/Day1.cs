using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day1
    {
        private int[] depths;

        public Day1()
        {
            depths = File.ReadAllLines(@"..\..\..\input1.txt")
                .Select(int.Parse)
                .ToArray();
        }

        public int GetAnswear1()
        {
            return CountOfIncreased(1);
        }

        public int GetAnswear2()
        {
            return CountOfIncreased(3);
        }

        private int CountOfIncreased(int skip)
        {
            return depths
                .Skip(skip)
                .Where((d, i) => d > depths[i])
                .Count();
        }
    }

}
