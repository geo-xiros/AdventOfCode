using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Computer
    {
        private Func<int[]> _loadMemory;
        private int[] _Memory;
        private int _diagnosticCode;
        private int _input;

        public Computer(Func<int[]> loadMemory)
        {
            _loadMemory = loadMemory;
            _commands = new Dictionary<int, Action<Func<int>, Func<int>, Func<int>>>();
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
        private bool _isParameter1Immediate => ((_Memory[_programCounter] / 100) % 10) == 1;
        private bool _isParameter2Immediate => ((_Memory[_programCounter] / 1000) % 10) == 1;
        private bool _isParameter3Immediate => ((_Memory[_programCounter] / 10000) % 10) == 1;
        private int _command => _Memory[_programCounter] % 100;
        private int _parameter1() => _isParameter1Immediate ? _programCounter + 1 : _Memory[_programCounter + 1];
        private int _parameter2() => _isParameter2Immediate ? _programCounter + 2 : _Memory[_programCounter + 2];
        private int _parameter3() => _isParameter3Immediate ? _programCounter + 3 : _Memory[_programCounter + 3];
        #endregion

        #region commands
        private Dictionary<int, Action<Func<int>, Func<int>, Func<int>>> _commands;
        private void AddTwoNumbers(Func<int> parameter1, Func<int> parameter2, Func<int> parameter3)
        {
            _Memory[parameter3()] = _Memory[parameter1()] + _Memory[parameter2()];
            _programCounter += 4;
        }
        private void MultiplyTwoNumbers(Func<int> parameter1, Func<int> parameter2, Func<int> parameter3)
        {
            _Memory[parameter3()] = _Memory[parameter1()] * _Memory[parameter2()];
            _programCounter += 4;
        }
        private void GetInputValue(Func<int> parameter1, Func<int> parameter2, Func<int> parameter3)
        {
            _Memory[parameter1()] = _input;
            _programCounter += 2;
        }
        private void GetOutputValue(Func<int> parameter1, Func<int> parameter2, Func<int> parameter3)
        {
            _diagnosticCode = _Memory[parameter1()];
            _programCounter += 2;
        }
        private void JumpIfTrue(Func<int> parameter1, Func<int> parameter2, Func<int> parameter3)
        {
            if (_Memory[parameter1()] != 0)
            {
                _programCounter = _Memory[parameter2()];
            }
            else
            {
                _programCounter += 3;
            }
        }
        private void JumpIfFalse(Func<int> parameter1, Func<int> parameter2, Func<int> parameter3)
        {
            if (_Memory[parameter1()] == 0)
            {
                _programCounter = _Memory[parameter2()];
            }
            else
            {
                _programCounter += 3;
            }
        }

        private void Store1IfLessThan(Func<int> parameter1, Func<int> parameter2, Func<int> parameter3)
        {
            if (_Memory[parameter1()] < _Memory[parameter2()])
            {
                _Memory[parameter3()] = 1;
            }
            else
            {
                _Memory[parameter3()] = 0;
            }
            _programCounter += 4;
        }
        private void Store1IfEqual(Func<int> parameter1, Func<int> parameter2, Func<int> parameter3)
        {
            if (_Memory[parameter1()] == _Memory[parameter2()])
            {
                _Memory[parameter3()] = 1;
            }
            else
            {
                _Memory[parameter3()] = 0;
            }
            _programCounter += 4;
        }
        #endregion

        #region Run Program
        public int GetDiagnosticCodeFor(int input)
        {
            _Memory = _loadMemory();
            _input = input;

            Run();
            
            return _diagnosticCode;
        }

        public int RunWithNounVerb(int noun, int verb)
        {
            _Memory = _loadMemory();
            _Memory[1] = noun;
            _Memory[2] = verb;

            Run();

            return _Memory[0];
        }
        private void Run()
        {
            for (_programCounter = 0; _programCounter < _Memory.Length && _command != 99;)
            {
                _commands[_command](_parameter1, _parameter2, _parameter3);
            }
        }
        #endregion
    }
}
