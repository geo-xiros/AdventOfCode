using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Computer
    {
        private long[] _Memory;
        private long _input;
        private long _relativeBase;
        public long Output => _Memory[0];
        public long DiagnosticCode => Output2.Last();
        public List<long> Output2 = new List<long>();
        public long this[long address]
        {
            get
            {
                if (address >= _Memory.Length)
                {
                    Array.Resize(ref _Memory, (int)address+1);
                }

                return _Memory[address];
            }
            set
            {
                if (address >= _Memory.Length)
                {
                    Array.Resize(ref _Memory, (int)address+1);
                }

                _Memory[address] = value;
            }
        }
        public Computer()
        {
            _commands = new Dictionary<long, Action>();
            _commands.Add(1, AddTwoNumbers);
            _commands.Add(2, MultiplyTwoNumbers);
            _commands.Add(3, GetInputValue);
            _commands.Add(4, GetOutputValue);
            _commands.Add(5, JumpIfTrue);
            _commands.Add(6, JumpIfFalse);
            _commands.Add(7, Store1IfLessThan);
            _commands.Add(8, Store1IfEqual);
            _commands.Add(9, AdjustRelativeBase);
        }

        #region Computer command Helpers
        private long _programCounter = 0;
        private long _command => this[_programCounter] % 100;
        private long _parameter(int i)
        {
            var positionMode = this[_programCounter] / (long)Math.Pow(10, i + 1) % 10;
            var relativeBase = positionMode == 2
                ? _programCounter + i + _relativeBase
                : _programCounter + i;

            return positionMode == 1
                ? _programCounter + i
                : this[relativeBase];
        }
        #endregion

        #region commands
        private Dictionary<long, Action> _commands;
        private void AddTwoNumbers()
        {
            this[_parameter(3)] = this[_parameter(1)] + this[_parameter(2)];
            _programCounter += 4;
        }
        private void MultiplyTwoNumbers()
        {
            this[_parameter(3)] = this[_parameter(1)] * this[_parameter(2)];
            _programCounter += 4;
        }
        private void GetInputValue()
        {
            this[_parameter(1)] = _input;
            _programCounter += 2;
        }
        private void GetOutputValue()
        {
            Console.WriteLine(this[_parameter(1)]);
            Output2.Add(this[_parameter(1)]);
            _programCounter += 2;
        }
        private void JumpIfTrue()
        {
            if (this[_parameter(1)] != 0)
            {
                _programCounter = this[_parameter(2)];
            }
            else
            {
                _programCounter += 3;
            }
        }
        private void JumpIfFalse()
        {
            if (this[_parameter(1)] == 0)
            {
                _programCounter = this[_parameter(2)];
            }
            else
            {
                _programCounter += 3;
            }
        }

        private void Store1IfLessThan()
        {
            if (this[_parameter(1)] < this[_parameter(2)])
            {
                this[_parameter(3)] = 1;
            }
            else
            {
                this[_parameter(3)] = 0;
            }
            _programCounter += 4;
        }
        private void Store1IfEqual()
        {
            if (this[_parameter(1)] == this[_parameter(2)])
            {
                this[_parameter(3)] = 1;
            }
            else
            {
                this[_parameter(3)] = 0;
            }
            _programCounter += 4;
        }
        private void AdjustRelativeBase()
        {
            _relativeBase = this[_parameter(1)];
            _programCounter += 2;
        }
        #endregion

        #region Run Program
        public Computer Run()
        {
            for (_programCounter = 0; _programCounter < _Memory.Length && _command != 99;)
            {
                _commands[_command]();
            }
            return this;
        }
        public Computer Using(long noun, long verb)
        {
            this[1] = noun;
            this[2] = verb;
            return this;
        }
        public Computer Set(long input)
        {
            _input = input;
            return this;
        }
        public Computer LoadProgram(Func<IEnumerable<long>> loadProgram)
        {
            _Memory = loadProgram().ToArray();
            return this;
        }
        #endregion
    }
}
