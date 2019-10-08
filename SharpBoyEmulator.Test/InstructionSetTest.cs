using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SharpBoyEmulator.Models;
using SharpBoyEmulator.Core;

namespace SharpBoyEmulator.Test
{
    public class InstructionSetTest
    {
        [Fact]
        public void AssertOpcodeGetter()
        {
            var opcode = InstructionSet.StandardOpcodes[0];
            Assert.NotNull(opcode);
            Assert.Equal("NOP", opcode.Description);
        }

        [Fact]
        public void AssertCbOpcodeGetter()
        {
            var opcode = InstructionSet.PrefixOpcodes[0];
            Assert.NotNull(opcode);
            Assert.Equal("RLC B", opcode.Description);
        }
    }
}
