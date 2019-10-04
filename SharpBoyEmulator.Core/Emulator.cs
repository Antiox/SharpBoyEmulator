using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Models;
using System.Linq;

namespace SharpBoyEmulator.Core
{
    public class Emulator : IEmulator
    {
        public IProcessor Processor { get; set; }
        public IMemory Memory { get; set; }
        public byte[] RomData { get; set; }

        private readonly IInstructionBuilder instructionBuilder;


        public Emulator()
        {
            Memory = new Memory();
            Processor = new Processor();
            instructionBuilder = new InstructionBuilder();
        }


        public void LoadMemory(byte[] data)
        {
            RomData = data;
            Memory.SetMemoryCells(data);
        }


        public IRomHeader GetRomHeader()
        {
            return new RomHeader(Memory.GetByteData(0x100, 0x14F));
        }

        public void ResetEmulator()
        {
            throw new NotImplementedException();
        }

        public IMemoryCell[] GetMemoryCells(int startIndex, int endIndex)
        {
            return Memory.GetMemoryCells(startIndex, endIndex);
        }

        public void Start()
        {
            Memory.PowerUpSequence();
        }

        public IInstruction BuildInstructionFromAddress(int address)
        {
            var operation = Memory.GetByteValue(address);
            var parameter1 = Memory.GetByteValue(address + 1);
            var parameter2 = Memory.GetByteValue(address + 2);
            return instructionBuilder.BuildInstruction(operation, parameter1, parameter2);
        }
    }
}
