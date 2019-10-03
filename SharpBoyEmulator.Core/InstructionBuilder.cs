using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.Core
{
    public class InstructionBuilder : IInstructionBuilder
    {


        public InstructionBuilder()
        {
        }


        public IInstruction BuildInstruction(byte operation, byte parameter1, byte parameter2)
        {
            var instruction = new Instruction();

            instruction.Operation = operation;
            

            return null;
        }
    }
}
