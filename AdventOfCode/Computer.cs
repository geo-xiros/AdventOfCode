using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Computer
    {
        private Dictionary<int, Action<int, int, int>> _instructions;
        private Func<int[]> _loadMemory;
        private int[] _Memory;

        public Computer(Func<int[]> loadMemory)
        {
            _loadMemory = loadMemory;
            _instructions = new Dictionary<int, Action<int, int, int>>();
            _instructions.Add(1, (parameter1, parameter2, address) => _Memory[address] = _Memory[parameter1] + _Memory[parameter2]);
            _instructions.Add(2, (parameter1, parameter2, address) => _Memory[address] = _Memory[parameter1] * _Memory[parameter2]);

        }
        public int Run(int noun, int verb)
        {
            _Memory = _loadMemory();
            _Memory[1] = noun;
            _Memory[2] = verb;

            for (var i = 0; i < _Memory.Length && _Memory[i] != 99; i += 4)
            {

                _instructions.TryGetValue(_Memory[i], out var instruction);
                instruction(_Memory[i + 1], _Memory[i + 2], _Memory[i + 3]);

            }
            return _Memory[0];
        }
        public int FindVerbAndNounForOutput(int output)
        {
            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    if (Run(noun, verb) == output)
                    {
                        return (100 * noun) + verb;
                    }
                }
            }

            return 0;
        }
    }
}
