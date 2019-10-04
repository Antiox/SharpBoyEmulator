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

        public string Description { get; set; }

        public byte Operation { get; set; }
        public byte Parameter1 { get; set; }
        public byte Parameter2 { get; set; }
        public int Cycle { get; set; }


        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
