using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Interfaces;
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
            Memory.Data = data;
        }


        public IRomHeader GetRomHeader()
        {
            return new RomHeader(Memory.Data);
        }

        public void ResetEmulator()
        {
            throw new NotImplementedException();
        }
    }
}
