using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IOpcode
    {
        delegate int CycleOpcodeOperation(IGameBoy device);
        delegate void OpcodeOperation(IGameBoy device);

        string Description { get; }
        int Cycle { get; }
        int VariableCycle { get; }

        int InvokeOperation(IGameBoy device, IInstruction instruction);
    }
}
