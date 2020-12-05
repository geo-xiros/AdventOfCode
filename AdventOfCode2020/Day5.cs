using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day5 : Day<int>
    {
        private IEnumerable<int> passBoardsSeats;

        public Day5() : base(5)
        {
            var boardingPassHelper = new BoardingPassHelper();
            passBoardsSeats = input.Select(bp => boardingPassHelper.GetSeatIdFrom(bp));
        }

        protected override int GetAnswer1()
        {
            return passBoardsSeats.Max();
        }

        protected override int GetAnswer2()
        {
            int seat = 0;
            
            // works for now but i dont like it.
            passBoardsSeats
                .OrderBy(s => s)
                .Aggregate((prev, cur) =>
                  {
                      if (cur - prev > 1)
                      {
                          seat = prev + 1;
                      }
                   
                      return cur;
                  });

            return seat;
        }
    }
}
