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
            MakeInput();
        }

        protected override int GetAnswer1()
        {
            return bags.Values
                .Count(b => b.CanContain("shiny gold bag")) 
                - 1; // 'shiny gold bag' rule should not count
        }

        protected override int GetAnswer2()
        {
            return bags["shiny gold bag"].Quantity;
        }

        private void MakeInput()
        {
            input.ToList().ForEach(line =>
            {
                var splitLine = line.Split(" contain ");
                var quantities = splitLine[1].Split(',').Select(s => GetQuantity(s) ?? "0").Select(s => string.IsNullOrEmpty(s) ? 0 : int.Parse(s)).ToList();
                var bagColorCodes = splitLine[1].Split(',').Select(s => GetColorCode(s)).ToList();
                var bagColorCode = GetColorCode(splitLine[0]);

                var bag = GetOrAdd(bagColorCode);

                bagColorCodes
                    .Zip(quantities, (colorCode, quantity) => new ContainedBag(GetOrAdd(colorCode), quantity))
                    .ToList()
                    .ForEach(cb => bag.Add(cb));
            });
        }

        private Bag GetOrAdd(string bagColorCode)
        {
            if (!bags.TryGetValue(bagColorCode, out Bag bag))
            {
                bag = new Bag(bagColorCode);
                bags[bagColorCode] = bag;
            }
            return bag;
        }

        private string GetColorCode(string line) => Regex.Match(line, @"(?<color>\w*\s\w*\sbag)").Value;

        private string GetQuantity(string line) => Regex.Match(line, @"\s*[0-9]*").Value.Trim();

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
