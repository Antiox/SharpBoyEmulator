using SharpBoyEmulator.Core;
using SharpBoyEmulator.Models;
using Xunit;

namespace SharpBoyEmulator.Test
{
    public class CpuInstructionsTest
    {
        private IGameBoy _gameboy;

        [Theory]
        [InlineData(0x00, 0x00, 0x00, 0x00, 0x80)]
        [InlineData(0x0E, 0x0E, 0xA0, 0x1C, 0x20)]
        [InlineData(0x07, 0xB0, 0x00, 0xB7, 0x00)]
        [InlineData(0x98, 0x30, 0x50, 0xC8, 0x00)]
        public void AssertAdd(byte value, byte registerA, byte registerF, byte expectedSum, byte expectedF)
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.A = registerA;
            _gameboy.Processor.Registers.F = registerF;

            Assert.Equal(expectedSum, _gameboy.Processor.Add(value));
            Assert.Equal(expectedF, _gameboy.Processor.Registers.F);
        }

        [Theory]
        [InlineData(0x9840, 0x0020, 0x10, 0x9860, 0x00)]
        [InlineData(0x6536, 0x0018, 0x20, 0x654e, 0x00)]
        public void AssertAddUshort(ushort value1, ushort value2, byte registerF, ushort expectedSum, byte expectedF)
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.F = registerF;

            Assert.Equal(expectedSum, _gameboy.Processor.Add(value1, value2));
            Assert.Equal(expectedF, _gameboy.Processor.Registers.F);
        }


        [Theory]
        [InlineData(0x00, 0x7F, 0x10, 0x80, 0x20)]
        [InlineData(0x08, 0x2a, 0x10, 0x33, 0x20)]
        [InlineData(0x08, 0x7F, 0x10, 0x88, 0x20)]
        public void AssertAdc(byte value, byte registerA, byte registerF, byte expectedSum, byte expectedF)
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.A = registerA;
            _gameboy.Processor.Registers.F = registerF;

            Assert.Equal(expectedSum, _gameboy.Processor.Adc(value));
            Assert.Equal(expectedF, _gameboy.Processor.Registers.F);
        }

        [Theory]
        [InlineData(0x00, 0x59, 0x70, 0x58, 0x40)]
        [InlineData(0x08, 0x58, 0x40, 0x50, 0x40)]
        [InlineData(0x08, 0x50, 0x40, 0x48, 0x60)]
        public void AssertSbc(byte value, byte registerA, byte registerF, byte expectedSum, byte expectedF)
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.A = registerA;
            _gameboy.Processor.Registers.F = registerF;

            Assert.Equal(expectedSum, _gameboy.Processor.Sbc(value));
            Assert.Equal(expectedF, _gameboy.Processor.Registers.F);
        }

        [Theory]
        [InlineData(0x00, 0x00, 0x60, 0x00, 0xA0)]
        [InlineData(0x01, 0x01, 0x40, 0x01, 0x20)]
        [InlineData(0x0F, 0x10, 0x20, 0x00, 0xA0)]
        public void AssertAnd(byte value, byte registerA, byte registerF, byte expectedSum, byte expectedF)
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.A = registerA;
            _gameboy.Processor.Registers.F = registerF;

            Assert.Equal(expectedSum, _gameboy.Processor.And(value));
            Assert.Equal(expectedF, _gameboy.Processor.Registers.F);
        }
    }
}
