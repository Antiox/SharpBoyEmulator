using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.BLL
{
    public class SharpBoyBusinessLogic : ISharpBoyBusinessLogic
    {
        public IGameBoy Device { get; set; }


        public SharpBoyBusinessLogic(IGameBoy device)
        {
            Device = device;
        }


        public void LoadRomData(string romPath)
        {
            using var stream = new FileStream(romPath, FileMode.Open, FileAccess.Read);
            var _data = new byte[stream.Length];
            stream.Read(_data, 0, _data.Length);
            Device.LoadMemory(_data);
        }

        public IRomHeader GetROMHeader()
        {
            return Device.GetRomHeader();
        }

        public void ResetEmulator()
        {
            Device.ResetEmulator();
        }

        public IMemoryCell[] GetMemoryCells(int startIndex, int endIndex)
        {
            return Device.GetMemoryCells(startIndex, endIndex);
        }
    }
}
