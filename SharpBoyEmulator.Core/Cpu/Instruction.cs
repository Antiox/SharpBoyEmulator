using SharpBoyEmulator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Core
{
    class Instruction : IInstruction
    {
        public ushort Address { get; }
        public IOpcode Opcode { get; }
        public byte[] Parameters { get; }

        public byte Parameter8 => Parameters[0];
        public ushort Parameter16 => BitConverter.ToUInt16(Parameters, 0);

        public string Description
        {
            get => Parameters.Length switch
            {
                1 => string.Format(Opcode.Description, Parameter8),
                2 => string.Format(Opcode.Description, Parameter16),
                _ => Opcode.Description
            };
        }

        public Instruction(ushort address, Opcode opcode, byte[] parameters)
        {
            Address = address;
            Opcode = opcode;
            Parameters = parameters;
        }

        public int Execute(IGameBoy device)
        {
            return Opcode.InvokeOperation(device, this);
        }


        public override string ToString()
        {
            return Address.ToString("X4") + ": " + Description;
        }
    }
}
