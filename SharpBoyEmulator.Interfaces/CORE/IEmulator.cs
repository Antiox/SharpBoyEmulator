using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Interfaces;

namespace SharpBoyEmulator.Interfaces
{
    public interface IEmulator
    {
        IProcessor Processor { get; set; }
        IMemory Memory { get; set; }


        void LoadMemory(byte[] data);
    }
}
