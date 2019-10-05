using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IGameBoy
    {
        IProcessor Processor { get; set; }
        IMemory Memory { get; set; }


        void LoadMemory(byte[] data);
        IRomHeader GetRomHeader();
        void ResetEmulator();
        IMemoryCell[] GetMemoryCells(int startIndex, int endIndex);
        void Start();

    }
}
