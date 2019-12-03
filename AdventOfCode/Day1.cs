using System.IO;

namespace AdventOfCode
{
    class Day1
    {
        private long _total1;
        private long _total2;

        public Day1()
        {
            using (var file = File.OpenRead(@"C:\Users\g.xiros\Documents\coding\adventofcode\AdventOfCode\Input\input1.txt"))
            {
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        var temp = (long.Parse(reader.ReadLine()) / 3) - 2;

                        _total1 += temp;
                        
                        while (temp > 0)
                        {
                            _total2 += temp;
                            temp = (temp / 3) - 2;
                        }
                    }
                }
            }
        }
        public override string ToString()
        {
            return $"Day 1 => Answer A:{_total1}, Answer B:{_total2}";
        }
    }
}
