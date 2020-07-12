using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
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
            return computer.PC + 4;
        }
        private long MultiplyTwoNumbers()
        {
            computer.Parameter3 = computer.Parameter1 * computer.Parameter2;
            return computer.PC + 4;
        }
        private long GetInputValue()
        {
            computer.Parameter1 = computer.Input();

            return computer.PC + 2;
        }
        private long GetOutputValue()
        {
            computer.Output(computer.Parameter1);
            return computer.PC + 2;
        }
        private long JumpIfTrue()
        {
            return (computer.Parameter1 != 0)
                ? computer.Parameter2
                : computer.PC + 3;
        }
        private long JumpIfFalse()
        {
            return (computer.Parameter1 == 0)
                ? computer.Parameter2
                : computer.PC + 3;
        }

        private long Store1IfLessThan()
        {
            computer.Parameter3 = (computer.Parameter1 < computer.Parameter2)
                ? 1
                : 0;

            return computer.PC + 4;
        }
        private long Store1IfEqual()
        {
            computer.Parameter3 = (computer.Parameter1 == computer.Parameter2)
                ? 1
                : 0;

            return computer.PC + 4;
        }
        private long AdjustRelativeBase()
        {
            computer.RelativeBase += computer.Parameter1;
            return computer.PC + 2;
        }
    }
}
