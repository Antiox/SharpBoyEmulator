using SharpBoyEmulator.Core;
using SharpBoyEmulator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SharpBoyEmulator.Test
{
    public class CpuInstructionsTest
    {
        private IProcessor processor;

        [Theory]
        [InlineData(0x01, 0x01, 0x02, 0x00)]
        [InlineData(0x00, 0x00, 0x00, 0x80)]
        public void AssertAdd(byte value, byte registerA, byte expectedSum, byte expectedF)
        {
            processor = new Processor(null);
            processor.Registers.A = registerA;
            processor.Registers.F = 0;

            Assert.Equal(expectedSum, processor.Add(value));
            Assert.Equal(expectedF, processor.Registers.F);
        }
    }
}
