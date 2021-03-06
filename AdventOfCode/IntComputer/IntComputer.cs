﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;

namespace AdventOfCode
{

    public class IntComputer : IIntComputer
    {
        private IntMemory memory;
        private IntCommands commands;
        private long Command => memory[PC] % 100;

        public Func<long> Input { get; private set; }
        public Action<long> Output { get; private set; }
        public long PC { get; private set; }
        public long MemoryZeroAddress => memory[0];
        public long RelativeBase { get; set; }
        public long Parameter1 { get => memory[GetParameter(1)]; set => memory[GetParameter(1)] = value; }
        public long Parameter2 { get => memory[GetParameter(2)]; set => memory[GetParameter(2)] = value; }
        public long Parameter3 { get => memory[GetParameter(3)]; set => memory[GetParameter(3)] = value; }

        public IntComputer()
        {
            commands = new IntCommands(this);
        }

        public IntComputer SetMemory(long quarters)
        {
            memory[0] = quarters;
            return this;
        }

        public IntComputer Using(long noun, long verb)
        {
            memory[1] = noun;
            memory[2] = verb;
            return this;
        }

        public IntComputer SetOutput(Action<long> output)
        {
            Output = output;
            return this;
        }

        public IntComputer SetInput(Func<long> input)
        {
            Input = input;
            return this;
        }

        public IntComputer LoadProgram(IEnumerable<long> loadProgram)
        {
            memory = new IntMemory(loadProgram.ToArray());
            return this;
        }

        public IntComputer Run()
        {
            PC = 0;
            RelativeBase = 0;

            while (true)
            {
                var command = Command;
                if (command == 99)
                {
                    break;
                }

                PC = commands.Run(command);
            }

            return this;
        }

        private long GetParameter(int i)
        {
            var positionMode = memory[PC] / (long)Math.Pow(10, i + 1) % 10;

            var relativeBase = positionMode == 2
                ? RelativeBase
                : 0;

            return positionMode == 1
                ? PC + i
                : memory[PC + i] + relativeBase;
        }
    }
}
