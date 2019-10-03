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

        public byte GetByteValue(int index)
        {
            return Data[index].Value;
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

        public void SetMemoryCell(int address, byte data)
        {
            Data[address].Value = data;
        }
        public void SetMemoryCells(byte[] data)
        {
            // The GameBoy Memory contains 0x10000 bytes (64KB) of Data, no matter what
            Data = new MemoryCell[0x10000];

            // We load into the Memory the first 0x8000 (32KB) bytes of the ROM, because it's how the GameBoy works. The rest is basically just RAM and I/O Registers
            for (int i = 0; i < Data.Length; i++)
            {
                var dataToLoad = i < 0x8000 ? data[i] : (byte)0;
                Data[i] = new MemoryCell(i, dataToLoad);
            }
        }
        public void PowerUpSequence()
        {
            Data[0xFF05].Value = 0x00;
            Data[0xFF06].Value = 0x00;
            Data[0xFF07].Value = 0x00;
            Data[0xFF10].Value = 0x80;
            Data[0xFF11].Value = 0xBF;
            Data[0xFF12].Value = 0xF3;
            Data[0xFF14].Value = 0xBF;
            Data[0xFF16].Value = 0x3F;
            Data[0xFF17].Value = 0x00;
            Data[0xFF19].Value = 0xBF;
            Data[0xFF1A].Value = 0x7F;
            Data[0xFF1B].Value = 0xFF;
            Data[0xFF1C].Value = 0x9F;
            Data[0xFF1E].Value = 0xBF;
            Data[0xFF20].Value = 0xFF;
            Data[0xFF21].Value = 0x00;
            Data[0xFF22].Value = 0x00;
            Data[0xFF23].Value = 0xBF;
            Data[0xFF24].Value = 0x77;
            Data[0xFF25].Value = 0xF3;
            Data[0xFF26].Value = 0xF1;
            Data[0xFF40].Value = 0x91;
            Data[0xFF42].Value = 0x00;
            Data[0xFF43].Value = 0x00;
            Data[0xFF45].Value = 0x00;
            Data[0xFF47].Value = 0xFC;
            Data[0xFF48].Value = 0xFF;
            Data[0xFF49].Value = 0xFF;
            Data[0xFF4A].Value = 0x00;
            Data[0xFF4B].Value = 0x00;
            Data[0xFFFF].Value = 0x00;
        }
    }
}
