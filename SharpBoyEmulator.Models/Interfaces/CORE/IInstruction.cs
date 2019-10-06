using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IInstruction
    {
        ushort Address { get; }
        string HexAddress { get; }
        string RawBytes { get; }
        IOpcode Opcode { get; }
        byte[] Parameters { get; }
        public byte Parameter8 { get; }
        public ushort Parameter16 { get; }


        int Execute(IGameBoy device);
    }
}
