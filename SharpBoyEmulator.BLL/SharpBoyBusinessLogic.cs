using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SharpBoyEmulator.Interfaces;

namespace SharpBoyEmulator.BLL
{
    public class SharpBoyBusinessLogic : ISharpBoyBusinessLogic
    {
        public IEmulator Emulator { get; set; }


        public SharpBoyBusinessLogic(IEmulator emulator)
        {
            Emulator = emulator;
        }


        public void LoadRomData(string romPath)
        {
            using (var stream = new FileStream(romPath, FileMode.Open, FileAccess.Read))
            {
                var _data = new byte[stream.Length];
                stream.Read(_data, 0, _data.Length);
                Emulator.LoadMemory(_data);
            }
        }


    }
}
