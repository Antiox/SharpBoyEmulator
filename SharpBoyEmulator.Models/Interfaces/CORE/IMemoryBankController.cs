using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IMemoryBankController
    {
        byte ReadByte(ushort address);
        void WriteByte(ushort address, byte value);
    }
}
