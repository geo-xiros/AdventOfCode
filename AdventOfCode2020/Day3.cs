using System.Linq;

namespace AdventOfCode2020
{
    public class Day3 : Day<long>
    {
        public Day3() : base(3)
        {
        }

        protected override long GetAnswer1()
        {
            return Trees(3, 1);
        }

        protected override long GetAnswer2()
        {
            var rights = new[] { 1, 3, 5, 7, 1 };
            var downs = new[] { 1, 1, 1, 1, 2 };
            var trees = rights.Zip(downs, (r, d) => Trees(r, d));

            return trees.Aggregate((total, current) => total * current);
        }

        private long Trees(int right, int down)
        {
            int trees = 0;
            int mapWidth = input[0].Length;

            for (int x = 0, y = 0; y < input.Length; x += right, y += down)
            {
                trees += input[y][x % mapWidth] == '#' ? 1 : 0;
            }

            return trees;
        }

    }
}
