using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    public class Day7
    {
        private long _answer1;
        private long _answer2;
        public Day7()
        {
            _answer1 = HighestSignalSentToTheThrusters(new long[] { 0, 1, 2, 3, 4 });
        }

        private long HighestSignalSentToTheThrusters(long[] phaseSettings)
        {
            long highestOutput = 0;

            foreach (var permutation in Permutations<long>.AllFor(phaseSettings))
            {
                var output = GetOutputfor(permutation);
                if (output > highestOutput) highestOutput = output;
            }
            return highestOutput;
        }
        private long GetOutputfor(long[] phaseSettings)
        {
            long output = 0;
            IntComputer computer;

            foreach (var ps in phaseSettings)
            {
                computer = new IntComputer()
                  .LoadProgram(LoadFile)
                  .Set(new Queue<long>(new long[] { ps, output }))
                  .Run();

                output = computer.Output2.FirstOrDefault();
            }
            return output;
        }
        private IEnumerable<long> LoadFile()
        {
            return File
                .ReadAllText("..\\..\\input7.txt")
                .Split(',')
                .Select(long.Parse);
        }
        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }

        private class Permutations<T>
        {
            public static System.Collections.Generic.IEnumerable<T[]> AllFor(T[] array)
            {
                if (array == null || array.Length == 0)
                {
                    yield return new T[0];
                }
                else
                {
                    for (int pick = 0; pick < array.Length; ++pick)
                    {
                        T item = array[pick];
                        int i = -1;
                        T[] rest = System.Array.FindAll<T>(array, delegate (T p) { return ++i != pick; });
                        foreach (T[] restPermuted in AllFor(rest))
                        {
                            i = -1;
                            yield return System.Array.ConvertAll<T, T>(array, (T p) =>
                            {
                                return ++i == 0 ? item : restPermuted[i - 1];
                            });
                        }
                    }
                }
            }
        }
    }
}