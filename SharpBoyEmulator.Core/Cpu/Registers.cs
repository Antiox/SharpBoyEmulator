using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.Core
{
    public class Registers : IRegisters
    {
        public bool IME { get; set; } // Interrupt Master Enable : bit qui détermine si on doit gérer ou non les interruptions
        public byte EnabledInterrupts { get; set; }


        public byte A { get; set; }
        public byte F { get; set; }
        public byte B { get; set; }
        public byte C { get; set; }
        public byte D { get; set; }
        public byte E { get; set; }
        public byte H { get; set; }
        public byte L { get; set; }
        public ushort PC { get; set; }
        public ushort SP { get; set; }
        public ushort AF
        {
            get => (ushort)(A * 0x100 | F); // Retourne la "concaténation" du registre A avec le registre F
            set
            {
                A = (byte)((value & 0xFF00) / 0x100); // Remplace le registre A par les 8 bits de poids fort des deux octets envoyés
                F = (byte)(value & 0x00FF); // Remplace le registre F par les 8 bits de poids faible des deux octets envoyés
            }
        }
        public ushort BC
        {
            get => (ushort)(B * 0x100 | C); // Retourne la "concaténation" du registre B avec le registre C
            set
            {
                B = (byte)((value & 0xFF00) / 0x100); // Remplace le registre B par les 8 bits de poids fort des deux octets envoyés
                C = (byte)(value & 0x00FF); // Remplace le registre C par les 8 bits de poids faible des deux octets envoyés
            }
        }
        public ushort DE
        {
            get  => (ushort)(D * 0x100 | E);  // Retourne la "concaténation" du registre D avec le registre E
            set
            {
                D = (byte)((value & 0xFF00) / 0x100); // Remplace le registre D par les 8 bits de poids fort des deux octets envoyés
                E = (byte)(value & 0x00FF);  // Remplace le registre E par les 8 bits de poids faible des deux octets envoyés
            }
        }
        public ushort HL
        {
            get => (ushort)(H * 0x100 | L); // Retourne la "concaténation" du registre H avec le registre L
            set
            {
                H = (byte)((value & 0xFF00) / 0x100); // Remplace le registre H par les 8 bits de poids fort des deux octets envoyés
                L = (byte)(value & 0x00FF); // Remplace le registre L par les 8 bits de poids faible des deux octets envoyés
            }
        }


        public byte ZFlag
        {
            get { return (byte)((F & 0x80) / 0x80); } // Retourne le 8ème bit du registre F
            set { F = (byte)((F & 0x7F) | (value << 7)); } // Remplace le 8ème bit du registre F par la valeur voulue
        }
        public byte NFlag
        {
            get { return (byte)((F & 0x40) / 0x40); } // Retourne le 7ème bit du registre F
            set { F = (byte)((F & 0xBF) | (value << 6)); } // Remplace le 7ème bit du registre F par la valeur voulue
        }
        public byte HFlag
        {
            get { return (byte)((F & 0x20) / 0x20); } // Retourne le 6ème bit du registre F
            set { F = (byte)((F & 0xDF) | (value << 5)); } // Remplace le 6ème bit du registre F par la valeur voulue
        }
        public byte CFlag
        {
            get { return (byte)((F & 0x10) / 0x10); } // Retourne le 5ème bit du registre F
            set { F = (byte)((F & 0xEF) | (value << 4)); } // Remplace le 5ème bit du registre F par la valeur voulue
        }

        public long CycleCount { get; set; }

        public void Initialize()
        {
            PC = 0x0100;
            SP = 0xFFFE;
            AF = 0x01B0;
            BC = 0x0013;
            DE = 0x00D8;
            HL = 0x014D;
            IME = true;
        }
    }
}
