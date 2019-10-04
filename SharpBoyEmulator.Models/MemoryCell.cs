namespace SharpBoyEmulator.Models
{
    public class MemoryCell : IMemoryCell
    {
        public int Address { get; set; }
        public byte Value { get; set; }
        public string HexAddress => $"0x{Address.ToString("X4")}";
        public string HexValue => $"0x{Value.ToString("X2")}";


        public MemoryCell(int a, byte v)
        {
            Address = a;
            Value = v;
        }
    }
}
