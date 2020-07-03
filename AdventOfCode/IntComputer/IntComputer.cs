using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;

namespace AdventOfCode
{

    public class IntComputer : IIntComputer
    {
        public Func<long> Input { get; set; }
        public Action<long> Output { get; set; }
        public long pc { get; private set; }
        private long Command => memory[pc] % 100;
        public long MemoryZeroAddress => memory[0];
        public long RelativeBase { get; set; }

        public IntMemory memory;
        private IntCommands commands;

        public IntComputer()
        {
            commands = new IntCommands(this);
        }

        #region Run Program
        public IntComputer Run()
        {
            pc = 0;

            while (true)
            {
                var command = Command;
                if (command == 99)
                {
                    break;
                }

                pc = commands.Run(command);
            }

            return this;
        }

        public long Parameter1 { get => memory[GetParameter(1)]; set => memory[GetParameter(1)] = value; }
        public long Parameter2 { get => memory[GetParameter(2)]; set => memory[GetParameter(2)] = value; }
        public long Parameter3 { get => memory[GetParameter(3)]; set => memory[GetParameter(3)] = value; }
        public long? PhaseSetting { get; set; }

        private long GetParameter(int i)
        {
            var positionMode = memory[pc] / (long)Math.Pow(10, i + 1) % 10;
            var relativeBase = positionMode == 2
                ? RelativeBase
                : 0;

            return positionMode == 1
                ? pc + i
                : memory[pc + i] + relativeBase;
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

        public IntComputer LoadProgram(Func<IEnumerable<long>> loadProgram)
        {
            memory = new IntMemory(loadProgram().ToArray());
            return this;
        }

        internal IntComputer SetPhase(long phaseSetting)
        {
            PhaseSetting = phaseSetting;
            return this;
        }
        #endregion
    }
}
