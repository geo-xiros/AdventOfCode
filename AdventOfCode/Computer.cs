using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Computer
    {
        private Func<int[]> _loadMemory;
        private int[] _Memory;
        private int _diagnosticCode;
        public Computer(Func<int[]> loadMemory)
        {
            _loadMemory = loadMemory;
        }
        public int RunWithNounVerb(int noun, int verb)
        {
            _Memory = _loadMemory();
            _Memory[1] = noun;
            _Memory[2] = verb;

            return Run();
        }
        public int GetDiagnosticCodeFor(int input)
        {
            _Memory = _loadMemory();
            Run(input);
            return _diagnosticCode;
        }
        private int Run(int input = 0)
        {
            for (var i = 0; i < _Memory.Length;)
            {
                int tmp = _Memory[i];
                var command = _Memory[i] % 100; tmp /= 100;
                var parameterMode1 = tmp % 10; tmp /= 10;
                var parameterMode2 = tmp % 10; tmp /= 10;
                var parameterMode3 = tmp % 10;

                if (command == 99)
                {
                    break;
                }

                Func<int> parameter1 = () => parameterMode1 == 1 ? i + 1 : _Memory[i + 1];
                Func<int> parameter2 = () => parameterMode2 == 1 ? i + 2 : _Memory[i + 2];
                Func<int> parameter3 = () => parameterMode3 == 1 ? i + 3 : _Memory[i + 3];

                switch (command)
                {
                    case 1:
                        _Memory[parameter3()] = _Memory[parameter1()] + _Memory[parameter2()];
                        i += 4;
                        break;
                    case 2:
                        _Memory[parameter3()] = _Memory[parameter1()] * _Memory[parameter2()];
                        i += 4;
                        break;
                    case 3:
                        _Memory[parameter1()] = input;
                        i += 2;
                        break;
                    case 4:
                        _diagnosticCode = _Memory[parameter1()];
                        i += 2;
                        break;
                    //Opcode 5 is jump -if-true: if the first parameter is non - zero, it sets the instruction pointer to the value from the second parameter.Otherwise, it does nothing.
                    case 5:
                        if (_Memory[parameter1()] != 0)
                        {
                            i = _Memory[parameter2()];
                        }
                        else
                        {
                            i += 3;
                        }
                        break;
                    //Opcode 6 is jump -if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter.Otherwise, it does nothing.
                    case 6:
                        if (_Memory[parameter1()] == 0)
                        {
                            i = _Memory[parameter2()];
                        }
                        else
                        {
                            i += 3;
                        }
                        break;
                    //Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter.Otherwise, it stores 0.
                    case 7:
                        if (_Memory[parameter1()] < _Memory[parameter2()])
                        {
                            _Memory[parameter3()] = 1;
                        }
                        else
                        {
                            _Memory[parameter3()] = 0;
                        }
                        i += 4;
                        break;
                    //Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter.Otherwise, it stores 0.
                    case 8:
                        if (_Memory[parameter1()] == _Memory[parameter2()])
                        {
                            _Memory[parameter3()] = 1;
                        }
                        else
                        {
                            _Memory[parameter3()] = 0;
                        }
                        i += 4;
                        break;
                    default:
                        break;
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
