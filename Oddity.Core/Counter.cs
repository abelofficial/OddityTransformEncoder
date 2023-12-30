namespace Oddity.Core
{

    public class Counter
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public int Count { get; set; }
        public byte Byte { get; set; }

        public override string ToString()
        {
            return $"\t | \t C = {Count}, \t Column = {Column}, \t Row = {Row}";
        }
    }
}