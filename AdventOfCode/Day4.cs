using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day4
    {
        private int _answer1;
        private int _answer2;
        public Day4()
        {
            _answer1 = NumbersBetween(240298, 784956)
                .Where(HasOnlyIncreasingDigits)
                .Where(HasAtLeastTwoAdjacentDigits)
                .Count();
            
            _answer2 = NumbersBetween(240298, 784956)
                .Where(HasOnlyIncreasingDigits)
                .Where(HasExactlyTwoAdjacentDigits)
                .Count(); 
        }

        private bool HasAtLeastTwoAdjacentDigits(int number) 
            => DoubleDigitsOf(number).Any(i => i >= 1);
        
        private bool HasExactlyTwoAdjacentDigits(int number) 
            => DoubleDigitsOf(number).Any(i => i == 1);

        private IList<int> DoubleDigitsOf(int number)
        {
            var numberCounts = new Dictionary<int, int>();
            
            DigitsOf(number).Aggregate((p, c) =>
            {
                if (p == c)
                {
                    numberCounts.TryGetValue(c, out int count);
                    numberCounts[c] = count + 1;
                }
            
                return c;
            });
            
            return numberCounts.Values.ToList();

        }
        private bool HasOnlyIncreasingDigits(int number)
        {
            return DigitsOf(number).Aggregate((p, c) => p < c ? 0 : c) != 0;
        }

        public IEnumerable<int> DigitsOf(int number)
        {
            for (; number > 0; number /= 10)
            {
                yield return number % 10;
            }
        }
        
        public IEnumerable<int> NumbersBetween(int from, int to)
        {
            for (var i= from; i<= to; i++)
            {
                yield return i;
            }
        }

        public override string ToString()
        {
            return $"Day 1 => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}
