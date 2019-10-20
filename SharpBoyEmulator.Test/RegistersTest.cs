using SharpBoyEmulator.Core;
using SharpBoyEmulator.Models;
using Xunit;

namespace SharpBoyEmulator.Test
{
    public class RegistersTest
    {
        private IGameBoy _gameboy;

        [Theory]
        [InlineData(0x5474, 0x5474)]
        [InlineData(0x0144, 0x0144)]
        [InlineData(0x0000, 0x0000)]
        public void AssertRegisters16(ushort value, ushort expected)
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.AF = value;
            _gameboy.Processor.Registers.BC = value;
            _gameboy.Processor.Registers.DE = value;
            _gameboy.Processor.Registers.HL = value;
            _gameboy.Processor.Registers.SP = value;
            _gameboy.Processor.Registers.PC = value;

            Assert.Equal(_gameboy.Processor.Registers.AF, expected);
            Assert.Equal(_gameboy.Processor.Registers.BC, expected);
            Assert.Equal(_gameboy.Processor.Registers.DE, expected);
            Assert.Equal(_gameboy.Processor.Registers.HL, expected);
            Assert.Equal(_gameboy.Processor.Registers.SP, expected);
            Assert.Equal(_gameboy.Processor.Registers.PC, expected);
        }

        [Theory]
        [InlineData(0x54, 0x54)]
        [InlineData(0x00, 0x00)]
        [InlineData(0x25, 0x25)]
        public void AssertRegisters8(byte value, byte expected)
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.A = value;
            _gameboy.Processor.Registers.B = value;
            _gameboy.Processor.Registers.C = value;
            _gameboy.Processor.Registers.D = value;
            _gameboy.Processor.Registers.E = value;
            _gameboy.Processor.Registers.H = value;
            _gameboy.Processor.Registers.L = value;
            _gameboy.Processor.Registers.F = value;

            Assert.Equal(_gameboy.Processor.Registers.A, expected);
            Assert.Equal(_gameboy.Processor.Registers.B, expected);
            Assert.Equal(_gameboy.Processor.Registers.C, expected);
            Assert.Equal(_gameboy.Processor.Registers.D, expected);
            Assert.Equal(_gameboy.Processor.Registers.E, expected);
            Assert.Equal(_gameboy.Processor.Registers.H, expected);
            Assert.Equal(_gameboy.Processor.Registers.L, expected);
            Assert.Equal(_gameboy.Processor.Registers.F, expected);
        }

        [Theory]
        [InlineData(0x54, 0x54, 0x5454)]
        [InlineData(0x22, 0x47, 0x2247)]
        [InlineData(0x00, 0x04, 0x0004)]
        public void AssertRegisters8to16(byte value1, byte value2, ushort expected)
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.A = value1;
            _gameboy.Processor.Registers.F = value2;

            _gameboy.Processor.Registers.B = value1;
            _gameboy.Processor.Registers.C = value2;

            _gameboy.Processor.Registers.D = value1;
            _gameboy.Processor.Registers.E = value2;

            _gameboy.Processor.Registers.H = value1;
            _gameboy.Processor.Registers.L = value2;

            Assert.Equal(expected, _gameboy.Processor.Registers.A * 0x100 | _gameboy.Processor.Registers.F);
            Assert.Equal(expected, _gameboy.Processor.Registers.B * 0x100 | _gameboy.Processor.Registers.C);
            Assert.Equal(expected, _gameboy.Processor.Registers.D * 0x100 | _gameboy.Processor.Registers.E);
            Assert.Equal(expected, _gameboy.Processor.Registers.H * 0x100 | _gameboy.Processor.Registers.L);
        }

        [Theory]
        [InlineData(0x6464, 0x64, 0x64)]
        [InlineData(0x5741, 0x57, 0x41)]
        [InlineData(0x5900, 0x59, 0x00)]
        public void AssertRegisters16to8(ushort value, byte expected1, byte expected2)
        {
            _gameboy = new GameBoy();

            _gameboy.Processor.Registers.AF = value;
            _gameboy.Processor.Registers.BC = value;
            _gameboy.Processor.Registers.DE = value;
            _gameboy.Processor.Registers.HL = value;

            Assert.Equal(expected1 * 0x100 | expected2, _gameboy.Processor.Registers.AF);
            Assert.Equal(expected1 * 0x100 | expected2, _gameboy.Processor.Registers.BC);
            Assert.Equal(expected1 * 0x100 | expected2, _gameboy.Processor.Registers.DE);
            Assert.Equal(expected1 * 0x100 | expected2, _gameboy.Processor.Registers.HL);
        }

        [Fact]
        public void AssertIME()
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.IME = true;
            Assert.True(_gameboy.Processor.Registers.IME);
        }

        [Fact]
        public void AssertEnabledInterrupts()
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.EnabledInterrupts = 0x38;
            Assert.Equal(0x38, _gameboy.Processor.Registers.EnabledInterrupts);
        }

        [Fact]
        public void AssertCycleCount()
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.CycleCount = 1234567;
            Assert.Equal(1234567, _gameboy.Processor.Registers.CycleCount);
        }

        [Theory]
        [InlineData(0x00, 0, 0, 0, 0)]
        [InlineData(0x84, 1, 0, 0, 0)]
        [InlineData(0xF0, 1, 1, 1, 1)]
        public void AssertFlags(byte F, byte expectedZ, byte expectedN, byte expectedH, byte expectedC)
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.F = F;

            Assert.Equal(_gameboy.Processor.Registers.ZFlag, expectedZ);
            Assert.Equal(_gameboy.Processor.Registers.NFlag, expectedN);
            Assert.Equal(_gameboy.Processor.Registers.HFlag, expectedH);
            Assert.Equal(_gameboy.Processor.Registers.CFlag, expectedC);
        }
    
        [Fact]
        public void AssertInitialize()
        {
            _gameboy = new GameBoy();
            _gameboy.Processor.Registers.Initialize();
            Assert.Equal(0x01B0, _gameboy.Processor.Registers.AF);
            Assert.Equal(0x0013, _gameboy.Processor.Registers.BC);
            Assert.Equal(0x00D8, _gameboy.Processor.Registers.DE);
            Assert.Equal(0x014D, _gameboy.Processor.Registers.HL);
            Assert.Equal(0x0100, _gameboy.Processor.Registers.PC);
            Assert.Equal(0xFFFE, _gameboy.Processor.Registers.SP);
            Assert.True(_gameboy.Processor.Registers.IME);
        }
    }
}
