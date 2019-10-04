using SharpBoyEmulator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Core
{
    class Instruction : IInstruction
    {
        public int Address { get; set; }
        public string HexAddress => $"0x{Address.ToString("X4")}";

        public string Description => throw new NotImplementedException();

        public byte Function { get; set; }
        public byte Parameter1 { get; set; }
        public byte Parameter2 { get; set; }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
