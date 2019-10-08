using SharpBoyEmulator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Core
{
    public class Opcode : IOpcode
    {
        public delegate int VariableOpcodeOperation(IGameBoy device, IInstruction instruction);
        public delegate void OpcodeOperation(IGameBoy device, IInstruction instruction);


        public string Description { get; private set; }
        public int Cycle { get; private set; }
        public int VariableCycle { get; private set; }
        public byte Operator { get; private set; }
        public int PC { get; private set; }

        public readonly int OperandLength;
        public readonly VariableOpcodeOperation Operation;

        public Opcode(string description, byte p1, int length, int cycle, int pc, OpcodeOperation operation)
            : this(description, p1, length, cycle, cycle, pc, (d, i) => { operation(d, i); return cycle; }) 
        {
        }
        public Opcode(string description, byte p1, int length, int cycle, int variableCycle, int pc, VariableOpcodeOperation operation)
        {
            Description = description;
            Operator = p1;
            OperandLength = length;
            Cycle = cycle;
            VariableCycle = variableCycle;
            Operation = operation;
            PC = pc;
        }


        public int InvokeOperation(IGameBoy device, IInstruction instruction)
        {
            return Operation.Invoke(device, instruction);
        }
    }
}
