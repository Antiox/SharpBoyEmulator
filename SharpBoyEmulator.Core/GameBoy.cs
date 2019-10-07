using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Models;
using System.Linq;

namespace SharpBoyEmulator.Core
{
    public class GameBoy : IGameBoy
    {
        public IProcessor Processor { get; private set; }
        public IMemory Memory { get; private set; }
        public ICartridge Cartridge { get; private set; }


        public GameBoy()
        {
            Memory = new Memory(this);
            Processor = new Processor(this);
        }


        public void LoadCartridge(byte[] data)
        {
            Cartridge = new Cartridge(data);
        }


        public IRomHeader GetRomHeader()
        {
            return Cartridge.GetRomHeader();
        }

        public void ResetEmulator()
        {
            throw new NotImplementedException();
        }

        public IMemoryCell[] GetMemoryCells(int startIndex, int endIndex)
        {
            var cells = new IMemoryCell[endIndex - startIndex + 1];

            for (int i = startIndex; i <= endIndex; i++)
                cells[i - startIndex] = new MemoryCell(i, Memory.ReadByte((ushort)i));

            return cells;
        }

        public void Start()
        {
        }

        public IInstruction GetInstruction(ushort address)
        {
            var instructionAddress = address;
            var code = Memory.ReadByte(address++);
            var opcode = code == 0xCB ? InstructionSet.PrefixOpcodes[Memory.ReadByte(address++)] : InstructionSet.StandardOpcodes[code];
            var parameters = Memory.ReadBytes(address, opcode.OperandLength);
            return new Instruction(instructionAddress, opcode, parameters);
        }

        public IRegisters GetRegisters()
        {
            return Processor.Registers;
        }

        public void Step()
        {
            Processor.Step();
        }

        public void Initialize()
        {
            Processor.Initialize();
            Memory.Initialize();
        }
    }
}
