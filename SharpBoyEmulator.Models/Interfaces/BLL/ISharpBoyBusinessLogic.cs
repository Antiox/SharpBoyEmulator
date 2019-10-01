using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.Interfaces
{
    public interface ISharpBoyBusinessLogic
    {
        IEmulator Emulator { get; set; }

        void LoadRomData(string romPath);
        IRomHeader GetROMHeader();
        void ResetEmulator();
    }
}
