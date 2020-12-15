using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day15 : Day<long>
    {
        public Day15() : base(15)
        {
        }

        protected override long GetAnswer1()
        {
            return GetNumberAt(2020);
        }

        protected override long GetAnswer2()
        {
            return GetNumberAt(30000000);
        }

        private long GetNumberAt(int numberSpoken)
        {
            var numbers = new Dictionary<int, (int First, int Second)>()
            {
                [2] = (-1, 0),
                [1] = (-1, 1),
                [10] = (-1, 2),
                [11] = (-1, 3),
                [0] = (-1, 4),
                [6] = (-1, 5)
            };

            int number = 6;
            for (int index = 6; index < numberSpoken; index++)
            {
                if (numbers.TryGetValue(number, out var foundIndex))
                {
                    if (foundIndex.First == -1)
                    {
                        number = 0;
                    }
                    else
                    {
                        number = foundIndex.Second - foundIndex.First;
                    }
                    if (numbers.TryGetValue(number, out var x))
                    {
                        numbers[number] = (x.Second, index);
                    }
                    else
                    {
                        numbers[number] = (-1, index);
                    }
                }
                else
                {
                    number = 0;
                    numbers[number] = (-1, index);
                }
            }

            return number;
        }
    }
}
