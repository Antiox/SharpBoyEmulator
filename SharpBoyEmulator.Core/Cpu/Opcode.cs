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

        public readonly byte Operand1;
        public readonly byte Operand2;
        public readonly int OperandLength;
        public readonly VariableOpcodeOperation Operation;

        public Opcode(string description, byte p1, byte p2, int length, int cycle, OpcodeOperation operation)
            : this(description, p1, p2, length, cycle, cycle, (d, i) => { operation(d, i); return cycle; }) 
        {
        }
        public Opcode(string description, byte p1, byte p2, int length, int cycle, int variableCycle, VariableOpcodeOperation operation)
        {
            Description = description;
            Operand1 = p1;
            Operand2 = p2;
            OperandLength = length;
            Cycle = cycle;
            VariableCycle = variableCycle;
            Operation = operation;
        }


        public int InvokeOperation(IGameBoy device, IInstruction instruction)
        {
            return Operation.Invoke(device, instruction);
        }
    }
}
