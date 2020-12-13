using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day13 : Day<long>
    {
        private IEnumerable<long> busIds;

        public Day13() : base(13)
        {
            busIds = input[1]
                .Replace('x', '0')
                .Split(',')
                .Select(b => long.Parse(b));
        }

        protected override long GetAnswer1()
        {
            var departTimestamp = int.Parse(input[0]);
            var (timestamp, busId) = FindEarliestBusAfter(departTimestamp);

            return (timestamp - departTimestamp) * busId;
        }

        protected override long GetAnswer2()
        {
            var input = busIds
               .Select((i, ix) => (Num: i, Index: ix))
               .Where(t => t.Num != 0)
               .ToArray();

            return FindOptimizedStep(input);
        }

        private (int timestamp, long busId) FindEarliestBusAfter(int timestamp)
        {
            long busId = 0;
            var busIdsWithoutZeros = busIds
                .Where(b => b != 0)
                .ToArray();

            while (true)
            {
                busId = busIdsWithoutZeros
                    .FirstOrDefault(b => timestamp % b == 0L);

                if (busId != 0)
                {
                    break;
                }

                timestamp++;
            }

            return (timestamp, busId);
        }

        private long  FindOptimizedStep((long Num, int Index)[] input)
        {
            long timestamp = 0;
            long step = 1;
            for (var i = 0; i < input.Length; i++)
            {
                (timestamp, step) = GetBusTimestampAndStep(input.Take(i + 1).ToArray(), timestamp, step);
            }

            return timestamp;
        }

        private  (long timestamp, long step) GetBusTimestampAndStep((long Num, int Index)[] input, long startingTimestamp, long step)
        {

            var occuredAtTimestamp = GetBusTimestamp(input, startingTimestamp, step);
            var secondTimestamp = GetBusTimestamp(input, occuredAtTimestamp, step);

            return (occuredAtTimestamp, secondTimestamp - occuredAtTimestamp);
        }

        private  long GetBusTimestamp((long Num, int Index)[] input, long timestamp, long step)
        {
            while (true)
            {
                timestamp += step;

                if (input
                    .All(t => ((timestamp + t.Index) % t.Num) == 0))
                {
                    return timestamp;
                }
            }
        }
    }
}
