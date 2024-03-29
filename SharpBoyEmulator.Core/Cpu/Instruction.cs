﻿using SharpBoyEmulator.Models;
using System;

namespace SharpBoyEmulator.Core
{
    public class Instruction : IInstruction
    {
        public ushort Address { get; }
        public IOpcode Opcode { get; }
        public byte[] Parameters { get; }
        public string HexAddress => $"0x{Address.ToString("X4")}";
        public string RawBytes => $"{Opcode.Operator.ToString("X2")} {BitConverter.ToString(Parameters).Replace("-", " ")}";

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
    }
}
