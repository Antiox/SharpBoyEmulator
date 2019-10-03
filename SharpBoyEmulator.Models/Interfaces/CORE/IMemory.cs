using SharpBoyEmulator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Interfaces
{
    public interface IMemory
    {
        IMemoryCell[] Data { get; set; }

        byte[] GetByteData();
        byte[] GetByteData(int endIndex);
        byte[] GetByteData(int startIndex, int endIndex);

        byte GetByteValue(int index);

        IMemoryCell[] GetMemoryCells();
        IMemoryCell[] GetMemoryCells(int endIndex);
        IMemoryCell[] GetMemoryCells(int startIndex, int endIndex);

        void SetMemoryCell(int address, byte data);
        void SetMemoryCells(byte[] data);
        void PowerUpSequence();
    }
}
