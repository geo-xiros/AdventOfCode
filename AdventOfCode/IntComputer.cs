using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;

namespace AdventOfCode
{
    public class IntMemory
    {
        private Dictionary<long, long> memory;

        public IntMemory()
        {
            this.memory = new Dictionary<long, long>();
        }

        public IntMemory(long[] memory)
        {
            this.memory = Enumerable
                .Range(0, memory.Length)
                .Zip(memory, (i, m) => new KeyValuePair<long, long>(i, m))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public long this[long address]
        {
            get
            {
                memory.TryGetValue(address, out var value);
                return value;
            }

            set => memory[address] = value;
        }

        public long Length => memory.Keys.Max();
    }
    public interface IComputer
    {

    }
    public class IntCommands
    {
        private Dictionary<long, Func<long>> commands;
        private IIntComputer computer;
        public IntCommands(IIntComputer computer)
        {
            this.computer = computer;
            commands = new Dictionary<long, Func<long>>();
            commands.Add(1, AddTwoNumbers);
            commands.Add(2, MultiplyTwoNumbers);
            commands.Add(3, GetInputValue);
            commands.Add(4, GetOutputValue);
            commands.Add(5, JumpIfTrue);
            commands.Add(6, JumpIfFalse);
            commands.Add(7, Store1IfLessThan);
            commands.Add(8, Store1IfEqual);
            commands.Add(9, AdjustRelativeBase);
        }

        public long Run(long command)
            => commands[command]();

        private long AddTwoNumbers()
        {
            computer.Parameter3 = computer.Parameter1 + computer.Parameter2;
            return computer.pc + 4;
        }
        private long MultiplyTwoNumbers()
        {
            computer.Parameter3 = computer.Parameter1 * computer.Parameter2;
            return computer.pc + 4;
        }
        private long GetInputValue()
        {
            if (computer.PhaseSetting.HasValue)
            {
                computer.Parameter1 = computer.PhaseSetting.Value;
                computer.PhaseSetting = null;
            }
            else
            {
                computer.Parameter1 = computer.Input();
            }
            return computer.pc + 2;
        }
        private long GetOutputValue()
        {
            computer.Output(computer.Parameter1);
            return computer.pc + 2;
        }
        private long JumpIfTrue()
        {
            if (computer.Parameter1 != 0)
            {
                return computer.Parameter2;
            }

            return computer.pc + 3;
        }
        private long JumpIfFalse()
        {
            if (computer.Parameter1 == 0)
            {
                return computer.Parameter2;
            }

            return computer.pc + 3;
        }

        private long Store1IfLessThan()
        {
            if (computer.Parameter1 < computer.Parameter2)
            {
                computer.Parameter3 = 1;
            }
            else
            {
                computer.Parameter3 = 0;
            }

            return computer.pc + 4;
        }
        private long Store1IfEqual()
        {
            if (computer.Parameter1 == computer.Parameter2)
            {
                computer.Parameter3 = 1;
            }
            else
            {
                computer.Parameter3 = 0;
            }

            return computer.pc + 4;
        }
        private long AdjustRelativeBase()
        {
            computer.RelativeBase = computer.Parameter1;
            return computer.pc + 2;
        }
    }

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
