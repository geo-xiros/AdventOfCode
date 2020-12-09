using AdventOfCode2020.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day8 : Day<int>
    {
        private Instruction[] instructions;

        public Day8() : base(8)
        {
            instructions = input
                .Select(l => Computer.Create(l))
                .ToArray();
        }

        protected override int GetAnswer1()
        {
            return new Computer()
                .Execute(instructions)
                .Accumulator;
        }

        protected override int GetAnswer2()
        {
            return instructions
                .Select((i, ix) => (Instruction: i, Index: ix))
                .Where(i => i.Instruction is JmpInstruction || i.Instruction is NopInstruction)
                .Select(i => i.Index)
                .Select(ix => new Computer().Execute(instructions, ix))
                .First(c => c.Terminated)
                .Accumulator;
        }

        #region Computer 

        private interface IComputer
        {
            void UpdateAccBy(int number);
            void UpdatePcBy(int number);
        }

        private class Computer : IComputer
        {
            private int pc;

            public int Accumulator { get; set; }

            public bool Terminated { get; set; }

            public Computer Execute(Instruction[] instructions, int indexToExecutePatchedInstruction = -1)
            {
                var instr = instructions.ToIndexedDictionary();
                var executedLines = new List<int>();

                pc = 0;
                Accumulator = 0;
                Terminated = false;

                while (pc < instructions.Length && !executedLines.Contains(pc))
                {
                    var instruction = instr[pc];
                    executedLines.Add(pc);

                    if (indexToExecutePatchedInstruction == pc)
                    {
                        instruction.Patched.Execute(this);
                    }
                    else
                    {
                        instruction.Execute(this);
                    }

                }

                Terminated = (pc == instructions.Length);

                return this;
            }

            public static Instruction Create(string line)
            {
                var instruction = Regex.Match(line, @"(?<operation>\w+) (?<argument>[+|-][0-9]+)");
                var operation = instruction.Groups["operation"].Value;
                var argument = int.Parse(instruction.Groups["argument"].Value);

                switch (operation)
                {
                    case "jmp":
                        return new JmpInstruction(argument);
                    case "acc":
                        return new AccInstruction(argument);
                    case "nop":
                        return new NopInstruction(argument);
                    default:
                        throw new InvalidOperationException();
                }
            }

            public void UpdateAccBy(int number)
            {
                Accumulator += number;
            }

            public void UpdatePcBy(int number)
            {
                pc += number;
            }

        }

        #endregion

        #region Computer Instructions

        private abstract class Instruction
        {

            protected int Argument { get; }

            public Instruction(int argument)
            {
                this.Argument = argument;
            }

            public abstract void Execute(IComputer computer);

            public virtual Instruction Patched => this;
        }

        private class JmpInstruction : Instruction
        {
            public JmpInstruction(int argument) : base(argument) { }

            public override void Execute(IComputer computer)
            {
                computer.UpdatePcBy(Argument);
            }

            public override Instruction Patched
                => new NopInstruction(Argument);
        }

        private class AccInstruction : Instruction
        {
            public AccInstruction(int argument) : base(argument) { }

            public override void Execute(IComputer computer)
            {
                computer.UpdateAccBy(Argument);
                computer.UpdatePcBy(1);
            }
        }

        private class NopInstruction : Instruction
        {
            public NopInstruction(int argument) : base(argument) { }

            public override void Execute(IComputer computer)
            {
                computer.UpdatePcBy(1);
            }

            public override Instruction Patched
                => new JmpInstruction(Argument);
        }

        #endregion

    }
}
