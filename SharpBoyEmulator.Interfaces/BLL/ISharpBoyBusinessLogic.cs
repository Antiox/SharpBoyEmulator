using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Interfaces
{
    public interface ISharpBoyBusinessLogic
    {
        IEmulator Emulator { get; set; }

        void LoadRomData(string romPath);
    }
}
