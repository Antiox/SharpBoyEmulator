using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface ISharpBoyBusinessLogic
    {
        IEmulator Emulator { get; set; }

        void LoadRomData(string romPath);
        IRomHeader GetROMHeader();
        void ResetEmulator();

        IMemoryCell[] GetMemoryCells(int startIndex, int endIndex);
    }
}
