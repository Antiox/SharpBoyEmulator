using SharpBoyEmulator.Core;
using SharpBoyEmulator.Models;
using Xunit;

namespace SharpBoyEmulator.Test
{
    public class MemoryTests
    {
        private IMemory _memory;

        [Fact]
        public void InitializeTest()
        {
            var device = new GameBoy();
            _memory = new Memory(device);

            _memory.Initialize();

            Assert.Equal(0x3F, _memory.ReadByte(0xFF00));
            Assert.Equal(0x80, _memory.ReadByte(0xFF10));
            Assert.Equal(0xBF, _memory.ReadByte(0xFF11));
            Assert.Equal(0xF3, _memory.ReadByte(0xFF12));
        }

        [Theory]
        [InlineData(0x0000, 0xFF, 0x00)]
        [InlineData(0x8000, 0xFF, 0xFF)]
        [InlineData(0xA000, 0xFF, 0xFF)]
        [InlineData(0xC000, 0xFF, 0xFF)]
        [InlineData(0xE000, 0xFF, 0xFF)]
        [InlineData(0xFE00, 0xFF, 0xFF)]
        [InlineData(0xFEA0, 0xFF, 0x00)]
        [InlineData(0xFF00, 0xFF, 0xFF)]
        [InlineData(0xFF4C, 0xFF, 0xFF)]
        [InlineData(0xFFFF, 0xFF, 0xFF)]
        public void ReadWriteByteTest(ushort address, byte value, byte expected)
        {
            var device = new GameBoy();
            _memory = new Memory(device);


            _memory.WriteByte(address, value);


            Assert.Equal(expected, _memory.ReadByte(address));
        }

        [Theory]
        [InlineData(0x8000, 0x1234, 0x1234)]
        [InlineData(0xC000, 0x4321, 0x4321)]
        public void ReadWriteUShortTest(ushort address, ushort value, ushort expected)
        {
            var device = new GameBoy();
            _memory = new Memory(device);


            _memory.WriteUShort(address, value);


            Assert.Equal(expected, _memory.ReadUShort(address));
        }
    }
}
