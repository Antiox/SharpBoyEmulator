using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IInstruction
    {
        int Address { get; set; }
        string HexAddress { get; }
        string Description { get; set; }
        byte Operation { get; set; }
        byte Parameter1 { get; set; }
        byte Parameter2 { get; set; }
        int Cycle { get; set; }


        void Execute();
    }
}
