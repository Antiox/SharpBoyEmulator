using Xunit;
using SharpBoyEmulator.Core;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.Test
{
    public class CartridgeTest
    {
        private ICartridge _cartridge;


        [Theory]
        [InlineData(0x0147, 0xFF)]
        [InlineData(0x1000, 0xED)]
        [InlineData(0x8000, 0x00)]
        public void ReadByteTest(ushort address, byte expected)
        {
            var data = new byte[0x8000];
            if(address < data.Length)
                data[address] = expected;

            _cartridge = new Cartridge(data);

            Assert.Equal(expected, _cartridge.ReadByte(address));
        }

        [Theory]
        [InlineData(0x0147, 0xFF, 0x00)]
        [InlineData(0x1000, 0xED, 0x00)]
        [InlineData(0x8000, 0x01, 0x00)]
        [InlineData(0xA001, 0x01, 0x01)]
        public void WriteByteTest(ushort address, byte value, byte expected)
        {
            var data = new byte[0x8000];

            _cartridge = new Cartridge(data);


            _cartridge.WriteByte(address, value);


            Assert.Equal(expected, _cartridge.ReadByte(address));
        }
    }
}
