using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SharpBoyEmulator.Core;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.Test
{
    public class InstructionTest
    {
        private IInstruction _instruction;

        [Fact]
        public void AssertInstructionExecute()
        {
            var gameboy = new GameBoy();
            var opcode = InstructionSet.StandardOpcodes[0x0B];
            _instruction = new Instruction(0, opcode, null);
            gameboy.Processor.Registers.BC = 0x01;
            _instruction.Execute(gameboy);
            Assert.Equal(0, gameboy.Processor.Registers.BC);
        }

        [Fact]
        public void AssertInstructionProperties()
        {
            var opcode = InstructionSet.StandardOpcodes[1];
            _instruction = new Instruction(0x123, opcode, new byte[] { 0x23, 0x55 });

            Assert.Equal(0x123, _instruction.Address);
            Assert.Equal(0x23, _instruction.Parameter8);
            Assert.Equal(0x5523, _instruction.Parameter16);
            Assert.Equal(new byte[] { 0x23, 0x55 }, _instruction.Parameters);
            Assert.Equal("0x0123", _instruction.HexAddress);
            Assert.Equal("01 23 55", _instruction.RawBytes);
            Assert.Same(opcode, _instruction.Opcode);
        }

        [Theory]
        [InlineData(0, new byte[] { }, "NOP")]
        [InlineData(1, new byte[] { 0x23, 0x01 }, "LD BC, 0123")]
        [InlineData(6, new byte[] { 0xAF }, "LD B, AF")]
        public void AssertInstructionDescription(int opcodeNumber, byte[] parameters, string expectedDescription)
        {
            var opcode = InstructionSet.StandardOpcodes[opcodeNumber];
            _instruction = new Instruction(0, opcode, parameters);
            Assert.Equal(expectedDescription, _instruction.Description);
        }
    }
}
