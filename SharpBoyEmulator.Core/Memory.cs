using SharpBoyEmulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Core
{
    public class Memory : IMemory
    {
        public byte[] Data { get; set; }

        public Memory() { }
        public Memory(byte[] data) => Data = data;
        public void Write(int adress, byte value) => Data[adress] = value;
        public byte Read(int adress) => Data[adress];



    }
}
