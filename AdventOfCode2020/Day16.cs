using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day16 : Day<long>
    {
        private FieldRules[] fieldsRules;
        private Ticket[] tickets;
        private int countOfFields;

        public Day16() : base(16)
        {
            fieldsRules = input
                .TakeWhile(i => i != "")
                .Select(i => new FieldRules(i))
                .ToArray();

            var nearbyTickets = input
                .SkipWhile(i => i != "nearby tickets:")
                .Skip(1)
                .Select(i => new Ticket(i, fieldsRules));

            var myTicket = input
                .SkipWhile(i => i != "your ticket:")
                .Skip(1)
                .Take(1)
                .Select(i => new Ticket(i, fieldsRules))
                .First();

            tickets = nearbyTickets
                .Prepend(myTicket)
                .ToArray();

            countOfFields = input
                .SkipWhile(i => i != "your ticket:")
                .Skip(1)
                .Take(1)
                .SelectMany(i => i.Split(','))
                .Count();
        }

        protected override long GetAnswer1()
        {
            return tickets
                .SelectMany(t => t.InvalidFieldValues)
                .Sum();
        }

        protected override long GetAnswer2()
        {
            var validTickets = tickets
                .Where(t => t.IsValid)
                .ToArray();

            var fieldsByTickets = validTickets
                .Select(t => t.Fields.Select(f => (f.Index, f.Names)).ToDictionary(t => t.Index, t => t.Names))
                .ToArray();

            while (validTickets.Any(t => t.FieldsWithoutName))
            {
                for (var f = 0; f < countOfFields; f++)
                {
                    var fieldNamesForAllTickets = fieldsByTickets
                        .Select(d => d[f])
                        .Aggregate((p, c) => p.Intersect(c).ToArray())
                        .ToArray();

                    if (fieldNamesForAllTickets.Length == 1)
                    {
                        validTickets
                            .ToList()
                            .ForEach(t => t.SetFieldName(f, fieldNamesForAllTickets[0]));
                    }
                }
            }

            return validTickets
                .First()
                .Fields
                .Where(f => f.Name.IndexOf("departure") >= 0)
                .Aggregate(1L, (t, kv) => t * kv.Number);
        }

        public class Ticket
        {
            public Field[] Fields { get; }

            public IEnumerable<int> InvalidFieldValues { get; }

            public bool IsValid => !InvalidFieldValues.Any();

            public bool FieldsWithoutName => Fields.Any(f => f.Name == null);

            public Ticket(string fields, FieldRules[] ticketRules)
            {
                Fields = fields
                    .Split(',')
                    .Select(int.Parse)
                    .Select((number, i)
                        => new Field(number, ValidNamesFor(number, ticketRules), i))
                    .ToArray();

                InvalidFieldValues = Fields
                    .Select(f => f.Number)
                    .Where(n => !IsFieldValid(ticketRules, n));
            }

            private static bool IsFieldValid(FieldRules[] ticketRules, int n)
                => ticketRules.Any(tr => tr.IsValid(n));

            private static string[] ValidNamesFor(int number, FieldRules[] ticketRules)
                => ticketRules
                    .Where(tr => tr.IsValid(number))
                    .Select(tr => tr.FieldName)
                    .ToArray();

            internal void SetFieldName(int index, string name)
            {
                Fields[index].SetName(name);

                foreach (var f in Fields)
                {
                    f.Remove(name);
                }
            }
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

        public class Field
        {
            private List<string> names;

            public IEnumerable<string> Names => names;

            public string Name { get; set; }

            public int Index { get; }

            public int Number { get; }

            public Field(int number, IEnumerable<string> names, int index)
            {
                this.names = names.ToList();
                Index = index;
                Number = number;
            }

            public void SetName(string name)
            {
                Name = name;
                names.Clear();
            }

            public void Remove(string name)
            {
                names.Remove(name);
            }
        }
    }

}
