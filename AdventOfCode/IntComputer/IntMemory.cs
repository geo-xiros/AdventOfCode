using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class IntMemory
    {
        private Dictionary<long, long> memory;

        public IntMemory()
        {
            this.memory = new Dictionary<long, long>();
        }

        public IntMemory(long[] memory)
        {
            this.memory = Enumerable
                .Range(0, memory.Length)
                .Zip(memory, (i, m) => new KeyValuePair<long, long>(i, m))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public long this[long address]
        {
            get
            {
                memory.TryGetValue(address, out var value);
                return value;
            }

            set => memory[address] = value;
        }
    }
}
