using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AdventOfCode
{
    public interface IIntComputer
    {
        long MemoryZeroAddress { get; }
        long RelativeBase { get; set; }
        long Parameter1 { get; set; }
        long Parameter2 { get; set; }
        long Parameter3 { get; set; }
        long pc { get; }

        IntComputer LoadProgram(Func<IEnumerable<long>> loadProgram);
        IntComputer Run();
        IntComputer Using(long noun, long verb);
        BlockingCollection<long> Input { get; set; }
        BlockingCollection<long> Output { get; set; }
        long? PhaseSetting { get; set; }
    }
}