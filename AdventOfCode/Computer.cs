using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Computer
    {
        private Dictionary<int, Action<int, int, int>> _instructions;
        private Func<int[]> _loadMemory;
        private int[] _Memory;
        private int _inputParameter;
        public Computer(Func<int[]> loadMemory, int inputParameter)
        {
            _loadMemory = loadMemory;
            _inputParameter = inputParameter;
            _instructions = new Dictionary<int, Action<int, int, int>>();
            _instructions.Add(1, (value1, value2, address) => _Memory[address] = value1 + value2);
            _instructions.Add(2, (value1, value2, address) => _Memory[address] = value1 * value2);
            _instructions.Add(3, (value1, value2, address) => _Memory[address] = _inputParameter);
            _instructions.Add(4, (value1, value2, address) => Console.WriteLine($"Output value: {_Memory[address]}"));
            _Memory = _loadMemory();
        }
        public int RunWithNounVerb(int noun, int verb)
        {
            _Memory = _loadMemory();
            _Memory[1] = noun;
            _Memory[2] = verb;

            return Run();
        }
        public int Run()
        {

            for (var i = 0; i < _Memory.Length;)
            {
                var command = _Memory[i] % 100;
                var parameterMode1 = (_Memory[i] / 100) % 10;
                var parameterMode2 = (_Memory[i] / 1000) % 10;
                var parameterMode3 = (_Memory[i] / 10000) % 10;

                if (command == 99)
                {
                    break;
                }

                _instructions.TryGetValue(command, out var instruction);

                int parameter1 = _Memory[i + 1];
                int parameter2 = _Memory[i + 2];
                int parameter3 = _Memory[i + 3];

                if (parameterMode1 == 0)
                {
                    parameter1 = _Memory[parameter1];
                }

                if (parameterMode2 == 0)
                {
                    parameter2 = _Memory[parameter2];
                }
                
                //if (parameterMode3 == 0)
                //{
                //    parameter3 = _Memory[parameter3];
                //}

                instruction(parameter1, parameter2, parameter3);

                if (command == 3 || command == 4)
                {
                    i += 2;
                }
                else
                {
                    i += 4;
                }

            }
            return _Memory[0];
        }
        public int FindVerbAndNounForOutput(int output)
        {
            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    if (RunWithNounVerb(noun, verb) == output)
                    {
                        return (100 * noun) + verb;
                    }
                }
            }

            return 0;
        }
    }
}
