using System.Linq;

namespace AdventOfCode2020
{
    public class BoardingPassHelper
    {
        private static BinarySpacePartitioning rows = new BinarySpacePartitioning(0, 127);
        private static BinarySpacePartitioning columns = new BinarySpacePartitioning(0, 7);

        public static int GetSeatIdFrom(string boardingPass)
        {
            var (row, column) = GetRowColumnFrom(boardingPass);

            return row * 8 + column;
        }

        public static (int Row, int Column) GetRowColumnFrom(string boardingPass)
        {
            var (row, column) = boardingPass.Aggregate((rows, columns),
                (rc, c) =>
                {
                    var (rows, columns) = rc;

                    switch (c)
                    {
                        case 'F':
                            return (rows.LowerHalf, columns);
                        case 'B':
                            return (rows.UpperHalf, columns);
                        case 'L':
                            return (rows, columns.LowerHalf);
                        case 'R':
                            return (rows, columns.UpperHalf);
                        default:
                            break;
                    }

                    return rc;
                });

            return (row.Value, column.Value);
        }
    }
}
