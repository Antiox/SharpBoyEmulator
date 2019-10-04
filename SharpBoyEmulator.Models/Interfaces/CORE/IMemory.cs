using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IMemory
    {
        IMemoryCell[] Data { get; set; }

        byte[] GetByteData();
        byte[] GetByteData(int endIndex);
        byte[] GetByteData(int startIndex, int endIndex);

        IMemoryCell[] GetMemoryCells();
        IMemoryCell[] GetMemoryCells(int endIndex);
        IMemoryCell[] GetMemoryCells(int startIndex, int endIndex);

        void SetMemoryCells(byte[] data);
    }
}
