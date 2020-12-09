using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Tests
{
    [TestClass]
    public class Day8UnitTest
    {
        private GameConsole gameColsole;
        private InfiniteLoop infiniteLoop;

        [TestInitialize]
        public void Initialize()
        {
            gameColsole = new GameConsole();
            infiniteLoop = new InfiniteLoop();
        }

        [TestMethod]
        public void AccumulatorShouldBeZero()
        {
            Assert.AreEqual(0, gameColsole.Accumulator);
        }

        [TestMethod]
        public void TestAccExecution()
        {
            int pc = 0;

            pc = gameColsole.Acc(1, pc);
            Assert.AreEqual(1, gameColsole.Accumulator);
            Assert.AreEqual(1, pc);

            pc = gameColsole.Acc(1, pc);
            Assert.AreEqual(2, gameColsole.Accumulator);
            Assert.AreEqual(2, pc);

            pc = gameColsole.Acc(-2, pc);
            Assert.AreEqual(0, gameColsole.Accumulator);
            Assert.AreEqual(3, pc);
        }

        [TestMethod]
        public void TestJmpExecution()
        {
            int pc = 0;

            pc = gameColsole.Jmp(2, pc);
            Assert.AreEqual(0, gameColsole.Accumulator);
            Assert.AreEqual(2, pc);

            pc = gameColsole.Jmp(2, pc);
            Assert.AreEqual(0, gameColsole.Accumulator);
            Assert.AreEqual(4, pc);

            pc = gameColsole.Jmp(-4, pc);
            Assert.AreEqual(0, gameColsole.Accumulator);
            Assert.AreEqual(0, pc);
        }

        [TestMethod]
        public void TestNopExecution()
        {
            int pc = 0;

            pc = gameColsole.Nop(0, pc);
            Assert.AreEqual(0, gameColsole.Accumulator);
            Assert.AreEqual(1, pc);

            pc = gameColsole.Nop(0, pc);
            Assert.AreEqual(0, gameColsole.Accumulator);
            Assert.AreEqual(2, pc);

            pc = gameColsole.Nop(0, pc);
            Assert.AreEqual(0, gameColsole.Accumulator);
            Assert.AreEqual(3, pc);
        }

        [TestMethod]
        public void TestInfiniteLoopDetector()
        {
            var inloop = infiniteLoop.At(0);
            Assert.AreEqual(false, inloop);

            inloop = infiniteLoop.At(1);
            Assert.AreEqual(false, inloop);

            inloop = infiniteLoop.At(2);
            Assert.AreEqual(false, inloop);

            inloop = infiniteLoop.At(3);
            Assert.AreEqual(false, inloop);

            inloop = infiniteLoop.At(1);
            Assert.AreEqual(true, inloop);

            inloop = infiniteLoop.At(2);
            Assert.AreEqual(true, inloop);

            inloop = infiniteLoop.At(3);
            Assert.AreEqual(true, inloop);
        }

        [TestMethod]
        public void TestInstructionExecution1()
        {
            var instructions = new (Func<int, int, int> operation, int argument)[]
            {
                (gameColsole.Nop, 0),
                (gameColsole.Acc, 1),
                (gameColsole.Jmp, 4),
                (gameColsole.Acc, 3),
                (gameColsole.Jmp, -3),
                (gameColsole.Acc, -99),
                (gameColsole.Acc, 1),
                (gameColsole.Jmp, -4),
                (gameColsole.Acc, 6)
            };

            var completed = gameColsole.Execute(instructions);

            Assert.AreEqual(false, completed);
            Assert.AreEqual(5, gameColsole.Accumulator);
        }
        

        [TestMethod]
        public void TestInstructionExecution2()
        {
            var instructions = new (Func<int, int, int> operation, int argument)[]
            {
                (gameColsole.Nop, 0),
                (gameColsole.Acc, 1),
                (gameColsole.Jmp, 4),
                (gameColsole.Acc, 3),
                (gameColsole.Jmp, -3),
                (gameColsole.Acc, -99),
                (gameColsole.Acc, 1),
                (gameColsole.Nop, -4),
                (gameColsole.Acc, 6)
            };

            var completed = gameColsole.Execute(instructions);

            Assert.AreEqual(true, completed);
            Assert.AreEqual(8, gameColsole.Accumulator);
        }

    }

    public class InfiniteLoop
    {
        private List<int> linesExecuted = new List<int>();

        internal bool At(int line)
        {
            if (linesExecuted.Contains(line))
            {
                return true;
            }

            linesExecuted.Add(line);

            return false;
        }
    }

    public class GameConsole
    {
        public int Accumulator { get; set; }
        
        public bool Execute((Func<int, int, int> operation, int argument)[] instructions)
        {
            var infiniteLoop = new InfiniteLoop();
            int pc = 0;

            while (pc < instructions.Length && !infiniteLoop.At(pc))
            {
                var (operation, argument) = instructions[pc];
                pc = operation(argument, pc);
            }

            return pc == instructions.Length;
        }

        internal int Acc(int argument, int pc)
        {
            Accumulator += argument;
            return pc + 1;
        }

        internal int Jmp(int argument, int pc)
        {
            return pc + argument;
        }

        internal int Nop(int argument, int pc)
        {
            return pc + 1;
        }
    }
}
