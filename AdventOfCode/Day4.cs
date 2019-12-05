using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day4
    {
        private int _answer1;
        private int _answer2;
        private int _fromRange = 240298;
        private int _toRange = 784956;
        public Day4()
        {
            _answer1 = NumbersRange()
                .Where(DigitsNeverDecrease)
                .Where(AtLeastOneDouble)
                .Count();
            
            _answer2 = NumbersRange()
                .Where(DigitsNeverDecrease)
                .Where(TwoDigitsOnly)
                .Count(); 
        }

        private bool AtLeastOneDouble(int number)
        {
            var strNumber = number.ToString();

            for (int i = 1; i < strNumber.Length; i++)
            {

                if (strNumber[i - 1] == strNumber[i])
                {
                    return true;
                }
            }
            return false;
        }

        private bool TwoDigitsOnly(int number)
        {
            var strNumber = number.ToString();
            var numberCounts = new Dictionary<char, int>();

            for (int i = 1; i < strNumber.Length; i++)
            {

                if (strNumber[i - 1] == strNumber[i])
                {
                    numberCounts.TryGetValue(strNumber[i], out int count);
                    numberCounts[strNumber[i]] = count + 1;
                }
            }
            return numberCounts.Values.Any(i => i == 1);
        }
        private bool DigitsNeverDecrease(int number)
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
        public IEnumerable<int> NumbersRange()
        {
            for (var i= 240298; i<= 784956; i++)
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
