using System.Collections.Generic;
using System.IO;
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


        public void LoadCartridge(string romPath)
        {
            using var stream = new FileStream(romPath, FileMode.Open, FileAccess.Read);
            var _data = new byte[stream.Length];
            stream.Read(_data, 0, _data.Length);
            Device.LoadCartridge(_data);
            stream.Dispose();
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

        public List<IInstruction> GetAllInstructions()
        {
            var instructions = new List<IInstruction>();

            for (ushort i = 0; i < 0xFFFF;)
            {
                var instr = Device.GetInstruction(i);
                instructions.Add(instr);
                i += (ushort)(instr.Parameters.Length + 1);
            }

            return instructions;
        }

        public IRegisters GetRegisters()
        {
            return Device.GetRegisters();
        }

        public void Step()
        {
            Device.Step();
        }

        public void StartEmulator()
        {
            Device.Initialize();
            Device.Start();
        }
    }
}
