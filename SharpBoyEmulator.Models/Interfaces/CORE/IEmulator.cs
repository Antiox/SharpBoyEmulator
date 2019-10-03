﻿using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Interfaces;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.Interfaces
{
    public interface IEmulator
    {
        IProcessor Processor { get; set; }
        IMemory Memory { get; set; }
        byte[] RomData { get; set; }


        void LoadMemory(byte[] data);
        IRomHeader GetRomHeader();
        void ResetEmulator();
        IMemoryCell[] GetMemoryCells(int startIndex, int endIndex);
        void Start();

    }
}
