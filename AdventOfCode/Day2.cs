using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day2
    {
        private long _Answer1;
        private string _Answer2;
        private Dictionary<int, Action<int, int, int>> _instructions;
        private int[] _memory;
        public Day2()
        {
            _instructions = new Dictionary<int, Action<int, int, int>>();
            _instructions.Add(1, (parameter1, parameter2, address) => _memory[address] = _memory[parameter1] + _memory[parameter2]);
            _instructions.Add(2, (parameter1, parameter2, address) => _memory[address] = _memory[parameter1] * _memory[parameter2]);

            RunProgram(12, 2);

            _Answer1 = _memory[0];
            _Answer2 = Answer2();

        }

        private string Answer2()
        {
            for (var noun = 0; noun < 99; noun++)
            {
                for (var verb = 0; verb < 99; verb++)
                {
                    RunProgram( noun, verb);
                    if (_memory[0] == 19690720)
                    {
                        return $"{noun}{verb}";
                    }

                }
            }
            return string.Empty;
        }

        private void RunProgram(int noun, int verb)
        {
            LoadFile();

            _memory[1] = noun;
            _memory[2] = verb;

            for (var i = 0; i < _memory.Length; i += 4)
            {
                if (!_instructions.TryGetValue(_memory[i], out var instruction))
                {
                    return;
                }

                instruction(_memory[i + 1], _memory[i + 2], _memory[i + 3]);

            }
        }
        private void LoadFile()
        {
            _memory = File
                .ReadAllText("..\\..\\input2.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();
        }

        public override string ToString()
        {
            return $"Day 2 => Answer A:{_Answer1}, Answer B:{_Answer2}";
        }
    }
}
