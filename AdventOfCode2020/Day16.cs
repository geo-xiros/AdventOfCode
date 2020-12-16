using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day16 : Day<long>
    {
        private FieldRules[] fieldsRules;
        private Ticket[] nearbyTickets;
        private Ticket ticket;

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
        }

        protected override long GetAnswer1()
        {
            return nearbyTickets
                .SelectMany(t => t.InvalidFieldsValues(fieldsRules))
                .Sum();
        }

        protected override long GetAnswer2()
        {

            var countOfFields = nearbyTickets.Select(t => t.Fields.Length).Max();

            var ticketsPossibleValues = nearbyTickets.Where(t => t.AllFieldsAreValid(fieldsRules)).Select(t => t.PossibleFieldNames(fieldsRules)).ToArray();
            var foundFields = new List<string>();
            var fieldNamesByIndex = new Dictionary<string, int>();

            for (var j = 0; j < 10; j++)
            {
                for (var f = 0; f < countOfFields; f++)
                {
                    var x = ticketsPossibleValues.Select(t => t[f].Except(foundFields).ToArray()).Aggregate((p, c) => p.Intersect(c).ToArray());
                    if (x.Length == 1)
                    {
                        foundFields.Add(x[0]);
                        fieldNamesByIndex.Add(x[0], f);
                        System.Console.WriteLine($"field {f} {x[0]}");
                    }
                }
            }

            return fieldNamesByIndex
                .Where(kv => kv.Key.IndexOf("departure") >= 0)
                .Aggregate(1L, (t, kv) => t * ticket[kv.Value]);
            //return 0;
        }
        public class Ticket
        {
            public long[] Fields { get; }

            public Ticket(string fields)
            {
                Fields = fields
                    .Split(',')
                    .Select(long.Parse)
                    .ToArray();
            }

            public IEnumerable<long> InvalidFieldsValues(FieldRules[] ticketRules)
                => Fields.Where(n => !IsFieldValid(ticketRules, n));

            public bool AllFieldsAreValid(FieldRules[] ticketRules)
                => Fields.All(n => IsFieldValid(ticketRules, n));

            private static bool IsFieldValid(FieldRules[] ticketRules, long n)
                => ticketRules.Any(tr => tr.IsValid(n));

            //public IEnumerable<(int FieldIndex, IEnumerable<string> ValidNames)> PossibleFieldNames(FieldRule[] ticketRules)
            //{
            //    return Fields
            //        .Select((number, i)
            //            => (Index: i, ValidNames: ValidNamesFor(number, ticketRules)));
            //        //.SelectMany(t => t.ValidNames, (t, vn) => (t.Index, vn));
            //}
            public string[][] PossibleFieldNames(FieldRules[] ticketRules)
            {
                return Fields
                    .Select(number
                        => ValidNamesFor(number, ticketRules))
                    .ToArray();
                //.SelectMany(t => t.ValidNames, (t, vn) => (t.Index, vn));
            }

            private static string[] ValidNamesFor(long number, FieldRules[] ticketRules)
                => ticketRules
                    .Where(tr => tr.IsValid(number))
                    .Select(tr => tr.FieldName)
                    .ToArray();

            public long this[int index] => Fields[index];
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

            public bool IsValid(long field)
            {
                return validRange.Contains((int)field);
            }
        }
    }

}
