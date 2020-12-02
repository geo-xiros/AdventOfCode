using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    public abstract class Day<T>
    {
        private int day;

        protected IEnumerable<string> input;

        public Day(int day)
        {
            this.day = day;
            input = File.ReadAllLines(@$"..\\..\\..\\input\input{day}.txt");
        }

        public string Answer1 => $"Day {day} Answer 1: {GetAnswer1()}";
        public string Answer2 => $"Day {day} Answer 2: {GetAnswer2()}";

        protected abstract T GetAnswer1();

        protected abstract T GetAnswer2();
    }
}
