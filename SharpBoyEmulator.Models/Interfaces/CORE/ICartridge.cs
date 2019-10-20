
namespace SharpBoyEmulator.Models
{
    public interface ICartridge
    {
        void WriteByte(ushort address, byte value);
        byte ReadByte(ushort address);

        IRomHeader GetRomHeader();
    }
}
