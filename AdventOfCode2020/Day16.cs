using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day16 : Day<long>
    {
        private FieldRules[] fieldsRules;
        private Ticket[] nearbyTickets;
        private Ticket ticket;
        private int countOfFields;

        public Day16() : base(16)
        {
            fieldsRules = input
                .TakeWhile(i => i != "")
                .Select(i => new FieldRules(i))
                .ToArray();

            nearbyTickets = input
                .SkipWhile(i => i != "nearby tickets:")
                .Skip(1)
                .Select(i => new Ticket(i))
                .ToArray();

            ticket = input
                .SkipWhile(i => i != "your ticket:")
                .Skip(1)
                .Take(1)
                .Select(i => new Ticket(i))
                .First();

            countOfFields = input
                .SkipWhile(i => i != "your ticket:")
                .Skip(1)
                .Take(1)
                .SelectMany(i => i.Split(','))
                .Count();
        }

        protected override long GetAnswer1()
        {
            return nearbyTickets
                .SelectMany(t => t.InvalidFieldsValues(fieldsRules))
                .Sum();
        }

        protected override long GetAnswer2()
        {
            var ticketsPossibleValues = nearbyTickets
                .Where(t => t.AllFieldsAreValid(fieldsRules))
                .Select(t => t.PossibleFieldNames(fieldsRules))
                .ToArray();

            var foundFields = new List<string>();
            var fieldsNames = new Dictionary<string, int>();

            while (fieldsNames.Count() < countOfFields)
            {
                for (var f = 0; f < countOfFields; f++)
                {
                    var x = ticketsPossibleValues.Select(t => t[f].Except(foundFields).ToArray()).Aggregate((p, c) => p.Intersect(c).ToArray());
                    if (x.Length == 1)
                    {
                        foundFields.Add(x[0]);
                        fieldsNames.Add(x[0], f);
                        System.Console.WriteLine($"field {f} {x[0]}");
                    }
                }
            }

            return fieldsNames
                .Where(kv => kv.Key.IndexOf("departure") >= 0)
                .Aggregate(1L, (t, kv) => t * (long)ticket[kv.Value]);
        }

        public class Ticket
        {
            public int[] Fields { get; }

            public Ticket(string fields)
            {
                Fields = fields
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();
            }

            public IEnumerable<int> InvalidFieldsValues(FieldRules[] ticketRules)
                => Fields.Where(n => !IsFieldValid(ticketRules, n));

            public bool AllFieldsAreValid(FieldRules[] ticketRules)
                => Fields.All(n => IsFieldValid(ticketRules, n));

            private static bool IsFieldValid(FieldRules[] ticketRules, int n)
                => ticketRules.Any(tr => tr.IsValid(n));

            public string[][] PossibleFieldNames(FieldRules[] ticketRules)
                => Fields
                    .Select(number
                        => ValidNamesFor(number, ticketRules))
                    .ToArray();

            private static string[] ValidNamesFor(int number, FieldRules[] ticketRules)
                => ticketRules
                    .Where(tr => tr.IsValid(number))
                    .Select(tr => tr.FieldName)
                    .ToArray();

            public int this[int index]
                => Fields[index];
        }

        public class FieldRules
        {
            public string FieldName { get; }
            private IEnumerable<int> validRange;

            public FieldRules(string rule)
            {
                var rule1 = rule.Split(": ");
                FieldName = rule1[0];
                var ranges = rule1[1].Split(" or ");
                var range1 = ranges[0].Split('-').Select(int.Parse).ToArray();
                var range2 = ranges[1].Split('-').Select(int.Parse).ToArray();

                validRange = Enumerable.Range(range1[0], range1[1] - range1[0] + 1)
                    .Concat(Enumerable.Range(range2[0], range2[1] - range2[0] + 1));
            }

            public bool IsValid(int field)
            {
                return validRange.Contains((int)field);
            }
        }
    }

}
