namespace AdventOfCode2020
{
    public class BinarySpacePartitioning
    {
        private BinarySpacePartitioning lowerHalf;
        private BinarySpacePartitioning upperHalf;
        
        public int LowValue { get; }

        public int HiValue { get; }

        public int Value => LowValue == HiValue
            ? LowValue
            : -1;

        public BinarySpacePartitioning LowerHalf => lowerHalf
            ?? (lowerHalf = new BinarySpacePartitioning(LowValue, LowValue + (HiValue - LowValue) / 2));

        public BinarySpacePartitioning UpperHalf => upperHalf
            ?? (upperHalf = new BinarySpacePartitioning(HiValue - (HiValue - LowValue) / 2, HiValue));

        public BinarySpacePartitioning(int lowValue, int hiValue)
        {
            this.LowValue = lowValue;
            this.HiValue = hiValue;
        }
    }
}
