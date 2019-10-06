using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface ICartridge
    {
        void WriteByte(ushort address, byte value);
        byte ReadByte(ushort address);

        IRomHeader GetRomHeader();
    }
}
