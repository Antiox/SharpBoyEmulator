using SharpBoyEmulator.Interfaces;
using SharpBoyEmulator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Core
{
    public class Memory : IMemory
    {
        public IMemoryCell[] Data { get; set; }

        public Memory() { }
        public Memory(byte[] data)
        {
            SetMemoryCells(data);
        }


        public byte[] GetByteData()
        {
            return GetByteData(Data.Length);
        }
        public byte[] GetByteData(int endIndex)
        {
            return GetByteData(0, endIndex);
        }
        public byte[] GetByteData(int startIndex, int endIndex)
        {
            var bytes = new byte[Data.Length];

            for (int i = startIndex; i < endIndex; i++)
                bytes[i] = Data[i].Value;

            return bytes;
        }

        public IMemoryCell[] GetMemoryCells()
        {
            return GetMemoryCells(Data.Length);
        }
        public IMemoryCell[] GetMemoryCells(int endIndex)
        {
            return GetMemoryCells(0, endIndex);
        }
        public IMemoryCell[] GetMemoryCells(int startIndex, int endIndex)
        {
            var cells = new IMemoryCell[endIndex - startIndex + 1];

            for (int i = startIndex; i <= endIndex; i++)
                cells[i - startIndex] = Data[i];

            return cells;
        }

        public void SetMemoryCells(byte[] data)
        {
            Data = new MemoryCell[data.Length];

            for (int i = 0; i < data.Length; i++)
                Data[i] = new MemoryCell(i, data[i]);
        }
    }
}
