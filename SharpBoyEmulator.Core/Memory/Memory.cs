using SharpBoyEmulator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Core
{
    public class Memory : IMemory
    {
        private readonly IGameBoy _device;


        public Memory(IGameBoy device)
        {
            _device = device;
        }
         


        public void WriteByte(ushort address, byte value)
        {

        }
        public void WriteUShort(ushort address, ushort value)
        {

        }
        public byte ReadByte(ushort address)
        {
            return 0;
        }
        public byte ReadUShort(ushort address)
        {
            return 0;
        }
    }
}
