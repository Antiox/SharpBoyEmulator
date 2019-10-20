using Xunit;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.Test
{
    public class MemoryCellTest
    {
        private IMemoryCell _memoryCell;

        [Fact]
        public void AssertMemoryCellProperties()
        {
            _memoryCell = new MemoryCell(0x0012, 0xF5);

            Assert.Equal(0x012, _memoryCell.Address);
            Assert.Equal(0xF5, _memoryCell.Value);
            Assert.Equal("0xF5", _memoryCell.HexValue);
            Assert.Equal("0x0012", _memoryCell.HexAddress);
        }
    }
}
