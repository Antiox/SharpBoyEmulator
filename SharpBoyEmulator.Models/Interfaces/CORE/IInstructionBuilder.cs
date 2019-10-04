using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IInstructionBuilder
    {
        IInstruction BuildInstruction(byte operation, byte parameter1, byte parameter2);
    }
}
