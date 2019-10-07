using SharpBoyEmulator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Core
{
    class Cartridge : ICartridge
    {
        private readonly IMemoryBankController _mbc;
        private readonly IRomHeader _romHeader;


        public Cartridge(byte[] romData)
        {
            _romHeader = new RomHeader(romData);
            _mbc = (romData[0x0147]) switch
            {
                _ => new MbcRomOnly(romData),
            };
        }



        public void WriteByte(ushort address, byte value)
        {
            _mbc.WriteByte(address, value);
        }
        public byte ReadByte(ushort address)
        {
            return _mbc.ReadByte(address);
        }

        public IRomHeader GetRomHeader()
        {
            return _romHeader;
        }
    }
}
