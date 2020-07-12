using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System;

namespace AdventOfCode
{
    public partial class Day7
    {
        private long _answer1;
        private long _answer2;
        
        public Day7() { }

        public async Task Run()
        {
            _answer1 = await HighestSignalSentToTheThrusters(new long[] { 0, 1, 2, 3, 4 });
            _answer2 = await HighestSignalSentToTheThrusters(new long[] { 5, 6, 7, 8, 9 });
        }

        private async Task<long> HighestSignalSentToTheThrusters(long[] phaseSettings)
        {
            var permutationsTasks = Permutations<long>.AllFor(phaseSettings)
                .Select(p => GetOutputFor(p));

            var results = await Task.WhenAll(permutationsTasks);

            return results.Max();
        }

        private async Task<long> GetOutputFor(long[] phaseSettings)
        {
            var outputA = new BlockingCollection<long>();
            var outputB = new BlockingCollection<long>();
            var outputC = new BlockingCollection<long>();
            var outputD = new BlockingCollection<long>();
            var outputE = new BlockingCollection<long>();

            
            outputA.Add(phaseSettings[1]);
            outputB.Add(phaseSettings[2]);
            outputC.Add(phaseSettings[3]);
            outputD.Add(phaseSettings[4]);
            outputE.Add(phaseSettings[0]);
            outputE.Add(0);

            var A = RunComputerWithWiredIO(phaseSettings[0], outputE, outputA);
            var B = RunComputerWithWiredIO(phaseSettings[1], outputA, outputB);
            var C = RunComputerWithWiredIO(phaseSettings[2], outputB, outputC);
            var D = RunComputerWithWiredIO(phaseSettings[3], outputC, outputD);
            var E = RunComputerWithWiredIO(phaseSettings[4], outputD, outputE);

            await Task.WhenAll(A, B, C, D, E);

            return outputE.LastOrDefault();
        }

        private Task RunComputerWithWiredIO(long phase, BlockingCollection<long> input, BlockingCollection<long> output)
            => Task.Run(()
                => new IntComputer()
                    .LoadProgram(FileUtils.LoadDataFor(7))
                    .SetInput(() => input.Take())//InputEnumerator(phase, input))
                    .SetOutput((o) => output.Add(o))
                    .Run());

        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}