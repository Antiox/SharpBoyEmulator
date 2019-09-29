using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Interfaces;


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
    }
}
