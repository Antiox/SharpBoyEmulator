
namespace SharpBoyEmulator.Models
{
    public interface IMemory
    {
        void Initialize();


        void WriteByte(ushort address, byte value);
        void WriteUShort(ushort address, ushort value);

        byte ReadByte(ushort address);
        ushort ReadUShort(ushort address);
        byte[] ReadBytes(ushort address, int length);
    }
}
