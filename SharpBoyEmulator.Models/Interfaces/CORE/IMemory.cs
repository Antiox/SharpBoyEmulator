using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IMemory
    {
        void WriteByte(ushort address, byte value);
        byte ReadByte(ushort address);
        void WriteUShort(ushort address, ushort value);
        byte ReadUShort(ushort address);
    }
}
