using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public class MemoryCell : IMemoryCell
    {
        public int Address { get; set; }
        public byte Value { get; set; }
        public string HexAddress { get { return $"0x{Address.ToString("X4")}"; } }
        public string HexValue { get { return $"0x{Value.ToString("X2")}"; } }


        public MemoryCell(int a, byte v)
        {
            Address = a;
            Value = v;
        }
    }
}
