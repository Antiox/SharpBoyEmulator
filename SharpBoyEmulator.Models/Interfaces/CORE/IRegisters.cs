using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Models
{
    public interface IRegisters
    {
        bool IME { get; set; } // Interrupt Master Enable : bit qui détermine si on doit gérer ou non les interruptions
        bool InterruptsEnabled { get; set; }
        bool CanInterrupt { get; }


        byte A { get; set; }
        byte F { get; set; }
        byte B { get; set; }
        public byte C { get; set; }
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


        public byte ZFlag { get; set; }
        public byte NFlag { get; set; }
        public byte HFlag { get; set; }
        public byte CFlag { get; set; }
    }
}
