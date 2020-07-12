using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AdventOfCode
{
    public interface IIntComputer
    {
        long Parameter1 { get; set; }
        long Parameter2 { get; set; }
        long Parameter3 { get; set; }
        long RelativeBase { get; set; }
        long PC { get; }
        Func<long> Input { get; }
        Action<long> Output { get; }
    }
}