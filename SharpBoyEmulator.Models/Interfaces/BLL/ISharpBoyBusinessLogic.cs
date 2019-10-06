using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface ISharpBoyBusinessLogic
    {
        IGameBoy Device { get; set; }

        void LoadCartridge(string romPath);
        IRomHeader GetROMHeader();
        void ResetEmulator();

        IMemoryCell[] GetMemoryCells(int startIndex, int endIndex);
        List<IInstruction> GetAllInstructions();
        IRegisters GetRegisters();
        void Step();
        void StartEmulator();
    }
}
