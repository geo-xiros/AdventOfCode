using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day7 : Day<int>
    {
        private Dictionary<string, Bag> bags = new Dictionary<string, Bag>();
        private record ContainedBag(Bag Bag, int Quantity);

        public Day7() : base(7)
        {
            PrepareInput();
        }

        protected override int GetAnswer1()
        {
            return bags.Values
                .Count(b => b.CanContain("shiny gold"))
                - 1; // 'shiny gold bag' rule should not count
        }

        protected override int GetAnswer2()
        {
            return bags["shiny gold"].Quantity;
        }

        private void PrepareInput()
        {
            input.ToList().ForEach(line =>
            {
                var bagRule = Regex.Match(line, @"(\w+\s\w+)\sbags\scontain").Groups[1].Value;
                var bag = GetOrAdd(bagRule);

                Regex
                    .Matches(line, @"(?<quantity>[0-9]+)\s(?<code>\w+\s\w+)\sbags?")
                    .Select(m => new ContainedBag(GetOrAdd(m.Groups["code"].Value), int.Parse(m.Groups["quantity"].Value)))
                    .ToList()
                    .ForEach(cb => bag.Add(cb));
            });
        }

        private Bag GetOrAdd(string bagColorCode)
        {
            if (bags.TryGetValue(bagColorCode, out Bag bag))
            {
                return bag;
            }

            bag = new Bag(bagColorCode);
            bags[bagColorCode] = bag;

            return bag;
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

            public void Add(ContainedBag containedBag)
            {
                containedBags.Add(containedBag);
            }

            public bool CanContain(string colorCode)
            {
                return ColorCode.Equals(colorCode) ||
                    containedBags.Any(cb => cb.Bag.CanContain(colorCode));
            }
        }
    }
}
