using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IRegisters
    {
        bool IME { get; set; } // Interrupt Master Enable : bit qui détermine si on doit gérer ou non les interruptions
        byte EnabledInterrupts { get; set; }


        byte A { get; set; }
        byte F { get; set; }
        byte B { get; set; }
        byte C { get; set; }
        byte D { get; set; }
        byte E { get; set; }
        byte H { get; set; }
        byte L { get; set; }
        ushort PC { get; set; }
        ushort SP { get; set; }
        ushort AF { get; set; }
        ushort BC { get; set; }
        ushort DE { get; set; }
        ushort HL { get; set; }
        long CycleCount { get; set; }


        byte ZFlag { get; set; }
        byte NFlag { get; set; }
        byte HFlag { get; set; }
        byte CFlag { get; set; }


        void Initialize();
    }
}
