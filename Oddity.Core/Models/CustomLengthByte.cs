namespace Oddity.Core
{

    public class CustomLengthByte
    {
        public bool[] BitArray { get; private set; }
        public int Base { get; private set; }
        public int Value { get; private set; }


        public CustomLengthByte(int byteBase, int value)
        {
            if (Math.Pow(2, byteBase) <= value)
                throw new ArgumentException($"Byte base {byteBase} can only present numbers from 0 - {Math.Pow(2, byteBase)}");
            this.Base = byteBase;
            this.Value = value;
            this.BitArray = this.generateBitArray();
        }

        private bool[] generateBitArray()
        {
            int[] bitPosValues = Enumerable
                                .Range(0, this.Base)
                                .Select(x => (int)Math.Pow(2, x))
                                .ToArray();

            int totalCounter = 0;
            int[] activeBits = bitPosValues
                                .Reverse()
                                .Where(x => totalCounterSafeIncrement(x, ref totalCounter))
                                .ToArray();

            return bitPosValues
                   .Select(x => activeBits.Contains(x))
                   .ToArray();
        }

        private bool totalCounterSafeIncrement(int current, ref int totalCounter)
        {
            if (totalCounter + current <= this.Value)
            {
                totalCounter += current;
                return true;
            }
            return false;
        }
    }
}

