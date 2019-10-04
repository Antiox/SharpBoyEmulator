using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Models;
using System.Linq;

namespace SharpBoyEmulator.Core
{
    public class Emulator : IEmulator
    {
        public IProcessor Processor { get; set; }
        public IMemory Memory { get; set; }



        public Emulator()
        {
            Memory = new Memory();
            Processor = new Processor();
        }


        public void LoadMemory(byte[] data)
        {
            Memory.SetMemoryCells(data);
        }


        public IRomHeader GetRomHeader()
        {
            return new RomHeader(Memory.GetByteData(0x100, 0x14F));
        }

        public void ResetEmulator()
        {
            throw new NotImplementedException();
        }

        public IMemoryCell[] GetMemoryCells(int startIndex, int endIndex)
        {
            return Memory.GetMemoryCells(startIndex, endIndex);
        }
    }
}
