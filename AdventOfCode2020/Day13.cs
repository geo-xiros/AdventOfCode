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

        protected override long GetAnswer2()
        {
            return busIds
                .Select((i, ix) => (Num: i, Index: ix))
                .Where(t => t.Num != 0)
                .ToArray()
                .Aggregate((timestamp: 0L, step: 1L, busses: new (long Num, int Index)[] { }),
                    (p, c) => GetBusTimestampAndStep(p.busses.Append(c).ToArray(), p.timestamp, p.step))
                .timestamp;
        }

        private (long timestamp, long step, (long Num, int Index)[] busses) GetBusTimestampAndStep((long Num, int Index)[] busses, long startingTimestamp, long step)
        {

            var occuredAtTimestamp = GetBusTimestamp(busses, startingTimestamp, step);
            var secondTimestamp = GetBusTimestamp(busses, occuredAtTimestamp, step);

            return (occuredAtTimestamp, secondTimestamp - occuredAtTimestamp, busses);
        }

        private long GetBusTimestamp((long Num, int Index)[] busses, long timestamp, long step)
        {
            while (true)
            {
                timestamp += step;

                if (busses
                    .All(t => ((timestamp + t.Index) % t.Num) == 0))
                {
                    return timestamp;
                }
            }
        }
    }
}
