namespace SharpBoyEmulator.Models
{
    public interface IMemoryCell
    {
        int Address { get; set; }
        byte Value { get; set; }
        string HexAddress { get; }
        string HexValue { get; }
    }
}
