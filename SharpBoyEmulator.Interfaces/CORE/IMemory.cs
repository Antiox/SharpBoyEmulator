using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Interfaces
{
    public interface IMemory
    {
        byte[] Data { get; set; }

        void Write(int adress, byte value);
        byte Read(int adress);
    }
}
