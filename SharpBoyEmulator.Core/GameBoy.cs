using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Models;
using System.Linq;

namespace SharpBoyEmulator.Core
{
    public class GameBoy : IGameBoy
    {
        public IProcessor Processor { get; set; }
        public IMemory Memory { get; set; }


        public GameBoy()
        {
            Memory = new Memory(this);
            Processor = new Processor(this);
        }


        public void LoadMemory(byte[] data)
        {
        }


        public IRomHeader GetRomHeader()
        {
            return null;
        }

        public void ResetEmulator()
        {
            throw new NotImplementedException();
        }

        public IMemoryCell[] GetMemoryCells(int startIndex, int endIndex)
        {
            return null;
        }

        public void Start()
        {
        }
    }
}
