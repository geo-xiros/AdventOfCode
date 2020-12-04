using System;
using System.Linq;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var dayType = typeof(IDay);

            AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => dayType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .Select(t => (IDay)Activator.CreateInstance(t))
                .ToList()
                .ForEach(Solve);
        }

        private static void Solve(IDay day)
        {
            Console.WriteLine(day.Answer1);
            Console.WriteLine(day.Answer2);
        }
    }
}
