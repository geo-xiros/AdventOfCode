using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day1
    {
        private long _answer1;
        private long _answer2;

        public Day1()
        {
            var fuelsRequiredByModule = FileUtils.LoadDataLines(1)
                .Select(long.Parse)
                .Select(mass => FuelRequired(mass));

            _answer1 = fuelsRequiredByModule
                .Sum();

            _answer2 = fuelsRequiredByModule
                .Select(FuelRequiredByFuel)
                .Sum();
        }

        private long FuelRequired(long mass)
            => (mass / 3) - 2;

        private long FuelRequiredByFuel(long fuels)
        {
            long totalFuels = 0;

            while (fuels > 0)
            {
                totalFuels += fuels;
                fuels = FuelRequired(fuels);
            }

            return totalFuels;
        }

        

        public override string ToString()
            => $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
    }
}
