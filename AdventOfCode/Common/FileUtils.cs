using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace AdventOfCode
{
    public static class FileUtils
    {
        public static IEnumerable<long> LoadDataFor(int day)
        {
            return File
                .ReadAllText($"..\\..\\inputs\\input{day}.txt")
                .Split(',')
                .Select(long.Parse);
        }

        public static IEnumerable<string> LoadDataLines(int day)
        {
            return File
                .ReadAllText($"..\\..\\inputs\\input{day}.txt")
                .Split('\n')
                .Where(l => l.Length != 0);
        }
    }
}