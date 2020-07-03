using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AdventOfCode
{
    public interface IIntComputer
    {
        long RelativeBase { get; set; }
        long Parameter1 { get; set; }
        long Parameter2 { get; set; }
        long Parameter3 { get; set; }
        long pc { get; }
        Func<long> Input { get; set; }
        Action<long> Output { get; set; }
        long? PhaseSetting { get; set; }
    }
}