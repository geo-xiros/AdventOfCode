using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Computer
    {
        private int[] _Memory;
        private int _input;

        public int Output => _Memory[0];
        public int DiagnosticCode;

        public Computer()
        {
            _commands = new Dictionary<int, Action>();
            _commands.Add(1, AddTwoNumbers);
            _commands.Add(2, MultiplyTwoNumbers);
            _commands.Add(3, GetInputValue);
            _commands.Add(4, GetOutputValue);
            _commands.Add(5, JumpIfTrue);
            _commands.Add(6, JumpIfFalse);
            _commands.Add(7, Store1IfLessThan);
            _commands.Add(8, Store1IfEqual);
        }

        #region Computer command Helpers
        private int _programCounter = 0;
        private int _command => _Memory[_programCounter] % 100;
        private bool _isParameter1Immediate => ((_Memory[_programCounter] / 100) % 10) == 1;
        private bool _isParameter2Immediate => ((_Memory[_programCounter] / 1000) % 10) == 1;
        private bool _isParameter3Immediate => ((_Memory[_programCounter] / 10000) % 10) == 1;
        private int _parameter1() => _isParameter1Immediate ? _programCounter + 1 : _Memory[_programCounter + 1];
        private int _parameter2() => _isParameter2Immediate ? _programCounter + 2 : _Memory[_programCounter + 2];
        private int _parameter3() => _isParameter3Immediate ? _programCounter + 3 : _Memory[_programCounter + 3];

        #endregion

        #region commands
        private Dictionary<int, Action> _commands;
        private void AddTwoNumbers()
        {
            _Memory[_parameter3()] = _Memory[_parameter1()] + _Memory[_parameter2()];
            _programCounter += 4;
        }
        private void MultiplyTwoNumbers()
        {
            _Memory[_parameter3()] = _Memory[_parameter1()] * _Memory[_parameter2()];
            _programCounter += 4;
        }
        private void GetInputValue()
        {
            _Memory[_parameter1()] = _input;
            _programCounter += 2;
        }
        private void GetOutputValue()
        {
            DiagnosticCode = _Memory[_parameter1()];
            _programCounter += 2;
        }
        private void JumpIfTrue()
        {
            if (_Memory[_parameter1()] != 0)
            {
                _programCounter = _Memory[_parameter2()];
            }
            else
            {
                _programCounter += 3;
            }
        }
        private void JumpIfFalse()
        {
            if (_Memory[_parameter1()] == 0)
            {
                _programCounter = _Memory[_parameter2()];
            }
            else
            {
                _programCounter += 3;
            }
        }

        private void Store1IfLessThan()
        {
            if (_Memory[_parameter1()] < _Memory[_parameter2()])
            {
                _Memory[_parameter3()] = 1;
            }
            else
            {
                _Memory[_parameter3()] = 0;
            }
            _programCounter += 4;
        }
        private void Store1IfEqual()
        {
            if (_Memory[_parameter1()] == _Memory[_parameter2()])
            {
                _Memory[_parameter3()] = 1;
            }
            else
            {
                _Memory[_parameter3()] = 0;
            }
            _programCounter += 4;
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
        public Computer Using(int noun, int verb)
        {
            _Memory[1] = noun;
            _Memory[2] = verb;
            return this;
        }
        public Computer Set(int input)
        {
            _input = input;
            return this;
        }
        public Computer LoadProgram(Func<IEnumerable<int>> loadProgram)
        {
            _Memory = loadProgram().ToArray();
            return this;
        }
        #endregion
    }
}
