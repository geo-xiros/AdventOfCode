using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day2
    {
        private int _answer1;
        private int _answer2;
        public Day2()
        {
            var computer = new Computer(LoadFile);

            _answer1 = computer.RunWithNounVerb(12, 2);
            _answer2 = FindVerbAndNounForOutput(computer, 19690720);

        }
        private int FindVerbAndNounForOutput(Computer computer, int output)
        {
            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    if (computer.RunWithNounVerb(noun, verb) == output)
                    {
                        return (100 * noun) + verb;
                    }
                }
            }

            return 0;
        }
        private int[] LoadFile()
        {
            return File
                .ReadAllText("..\\..\\input2.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}
