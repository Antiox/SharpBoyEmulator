
namespace SharpBoyEmulator.Models
{
    public interface IMemoryBankController
    {
        byte ReadByte(ushort address);
        void WriteByte(ushort address, byte value);
    }
}
