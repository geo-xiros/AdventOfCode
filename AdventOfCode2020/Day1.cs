using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day1 : Day<int>
    {
        private int[] expenseReport;

        public Day1() : base(1)
        {
            expenseReport = input.Select(int.Parse).ToArray();
        }

        protected override int GetAnswer1()
        {
            for (int i = 0; i < expenseReport.Length; i++)
                for (int j = i + 1; j < expenseReport.Length; j++)
                {
                    if (expenseReport[i] + expenseReport[j] == 2020)
                    {
                        return expenseReport[i] * expenseReport[j];
                    }
                }

            return 0;
        }

        protected override int GetAnswer2()
        {
            for (int i = 0; i < expenseReport.Length; i++)
                for (int j = i + 1; j < expenseReport.Length; j++)
                    for (int k = j + 1; k < expenseReport.Length; k++)
                    {
                        if (expenseReport[i] + expenseReport[j] + expenseReport[k] == 2020)
                        {
                            return expenseReport[i] * expenseReport[j] * expenseReport[k];
                        }
                    }

            return 0;
        }
    }
}
