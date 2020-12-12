using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day11 : Day<long>
    {
        private (int x, int y)[] seatOffsets =
            new (int x, int y)[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

        public Day11() : base(11)
        {

        }

        protected override long GetAnswer1()
        {
            var (updatedMap, changed) = UpdateSeats(input, input, NoOccupiedSeats, FourOrMoreOccupiedSeats);
            while (changed)
            {
                (updatedMap, changed) = UpdateSeats(input, updatedMap, NoOccupiedSeats, FourOrMoreOccupiedSeats);
            }

            return updatedMap.SelectMany(s => s).Count(c => c == '#');
        }

        protected override long GetAnswer2()
        {
            var (updatedMap, changed) = UpdateSeats(input, input, NoOccupiedSeats2, FourOrMoreOccupiedSeats2);
            while (changed)
            {
                (updatedMap, changed) = UpdateSeats(input, updatedMap, NoOccupiedSeats2, FourOrMoreOccupiedSeats2);
            }

            return updatedMap.SelectMany(s => s).Count(c => c == '#');
        }

        public (string[], bool) UpdateSeats(string[] seatsMap, string[] seats,
            Func<string[], int, int, bool> NoOccupiedSeats,
            Func<string[], int, int, bool> FourOrMoreOccupiedSeats)
        {
            var changed = false;
            var newSeats = new List<string>();

            for (var i = 0; i < seatsMap.Length; i++)
            {
                var sb = new StringBuilder();

                for (var j = 0; j < seatsMap[i].Length; j++)
                {
                    var s = seats[i][j];

                    if (s == 'L')
                    {
                        if (NoOccupiedSeats(seats, i, j))
                        {
                            changed = true;
                        }
                        sb.Append(NoOccupiedSeats(seats, i, j) ? '#' : s);
                    }
                    else if (s == '#')
                    {
                        if (FourOrMoreOccupiedSeats(seats, i, j))
                        {
                            changed = true;
                        }
                        sb.Append(FourOrMoreOccupiedSeats(seats, i, j) ? 'L' : s);
                    }
                    else
                    {
                        sb.Append(s);
                    }
                }

                newSeats.Add(sb.ToString());
            }

            return (newSeats.ToArray(), changed);
        }

        private bool FourOrMoreOccupiedSeats(string[] seats, int i, int j)
        {
            return seatOffsets
                .Select(t => GetSeatInfo1(seats, i, j, t.x, t.y))
                .Sum() >= 4;
        }

        private bool NoOccupiedSeats(string[] seats, int i, int j)
        {
            return seatOffsets
                .Select(t => GetSeatInfo1(seats, i, j, t.x, t.y))
                .Sum() == 0;
        }

        private bool FourOrMoreOccupiedSeats2(string[] seats, int i, int j)
        {
            return seatOffsets
                .Select(t => GetSeatInfo2(seats, i, j, t.x, t.y))
                .Sum() >= 5;
        }

        private bool NoOccupiedSeats2(string[] seats, int i, int j)
        {
            return seatOffsets
                .Select(t => GetSeatInfo2(seats, i, j, t.x, t.y))
                .Sum() == 0;
        }

        private int GetSeatInfo1(string[] seats, int y, int x, int offX, int offY)
        {
            int ny = y + offY;
            int nx = x + offX;

            if (ny < 0 || ny > seats.Length - 1 ||
                nx < 0 || nx > seats[0].Length - 1 ||
                seats[ny][nx] == 'L')
            {
                return 0;
            }

            if (seats[ny][nx] == '#')
            {
                return 1;
            }

            return 0;
        }

        private int GetSeatInfo2(string[] seats, int y, int x, int offX, int offY)
        {
            for (int ny = y + offY, nx = x + offX; ; ny += offY, nx += offX)
            {

                if (ny < 0 || ny > seats.Length - 1 ||
                    nx < 0 || nx > seats[0].Length - 1 ||
                    seats[ny][nx] == 'L')
                {
                    return 0;
                }

                if (seats[ny][nx] == '#')
                {
                    return 1;
                }
            }
        }
    }
}
