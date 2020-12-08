using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day7 : Day<int>
    {
        private List<Bag> bags;
        private record ContainedBag(Bag Bag, int Quantity);

        public Day7() : base(7)
        {
            bags = PrepareInput();
        }

        protected override int GetAnswer1()
        {
            return bags
                .Count(b => b.CanContain("shiny gold"))
                - 1; // 'shiny gold bag' rule should not count
        }

        protected override int GetAnswer2()
        {
            return bags
                .Where(b => b.ColorCode.Equals("shiny gold"))
                .First()
                .Quantity;
        }

        private List<Bag> PrepareInput()
        {
            var bags = input
                .Select(l => Bag.Create(l))
                .ToDictionary(b => b.ColorCode);

            bags.Values
                .Zip(input, (bag, line) => (bag, line))
                .ToList()
                .ForEach(t => t.bag.SetContainedBags(t.line, bags));

            return bags.Values
                .ToList();
        }

        private class Bag
        {
            private List<ContainedBag> containedBags = new List<ContainedBag>();

            public string ColorCode { get; set; }

            public int Quantity => containedBags
                .Select(cb => cb.Quantity + cb.Quantity * cb.Bag.Quantity)
                .Sum();

            public Bag(string colorCode)
            {
                ColorCode = colorCode;
            }

            public bool CanContain(string colorCode)
                => ColorCode.Equals(colorCode) ||
                    containedBags.Any(cb => cb.Bag.CanContain(colorCode));

            internal void SetContainedBags(string line, Dictionary<string, Bag> bags)
            {
                containedBags = Regex
                    .Matches(line, @"(?<quantity>[0-9]+)\s(?<code>\w+\s\w+)\sbags?")
                    .Select(m => new ContainedBag(bags[m.Groups["code"].Value], int.Parse(m.Groups["quantity"].Value)))
                    .ToList();
            }

            public static Bag Create(string line)
            {
                var colorCode = Regex.Match(line, @"(\w+\s\w+)\sbags\scontain").Groups[1].Value;
                return new Bag(colorCode);
            }

        }

    }
}
