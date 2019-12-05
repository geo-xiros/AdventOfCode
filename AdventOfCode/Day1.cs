using System.IO;

namespace AdventOfCode
{
    class Day1
    {
        private long _answer1;
        private long _answer2;

        public Day1()
        {
            using (var file = File.OpenRead("..\\..\\input1.txt"))
            {
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        var temp = (long.Parse(reader.ReadLine()) / 3) - 2;

                        _answer1 += temp;
                        
                        while (temp > 0)
                        {
                            _answer2 += temp;
                            temp = (temp / 3) - 2;
                        }
                    }
                }
            }
        }
        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }


}
