using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IGameBoy
    {
        IProcessor Processor { get; set; }
        IMemory Memory { get; set; }
        ICartridge Cartridge { get; set; }


        void LoadCartridge(byte[] data);
        IRomHeader GetRomHeader();
        void ResetEmulator();
        IMemoryCell[] GetMemoryCells(int startIndex, int endIndex);
        void Start();


        IInstruction GetInstruction(ushort address);
        IRegisters GetRegisters();
        void Step();
        void Initialize();
    }
}
