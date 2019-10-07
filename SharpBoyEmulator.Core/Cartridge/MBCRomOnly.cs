using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.Core
{
    class MbcRomOnly : IMemoryBankController
    {
        private readonly byte[]  _romData;
        private readonly byte[] _ramData;


        public MbcRomOnly(byte[] romData)
        {
            _romData = romData;
            _ramData = new byte[0x2000];
        }


        public byte ReadByte(ushort address)
        {
            return address switch
            {
                ushort n when n < 0x8000 => _romData[n],
                ushort n when n >= 0xA000 && n < 0xC000 => _ramData[n - 0xA000],
                _ => 0x00,
            };
        }

        public void WriteByte(ushort address, byte value)
        {
            switch (address)
            {
                case ushort n when n < 0x8000: return;
                case ushort n when n >= 0xA000 && n < 0xC000: _ramData[n - 0xA000] = value; break;
                default: return;
            }
        }
    }
}
