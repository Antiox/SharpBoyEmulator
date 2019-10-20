using System.Text;

namespace SharpBoyEmulator.Models
{
    public class RomHeader : IRomHeader
    {
        public string Title { get; set; }
        public string ModeGBC { get; set; }
        public string Editor { get; set; }
        public string ModeSGB { get; set; }
        public string TypeCartouche { get; set; }
        public int ROMSize { get; set; }
        public int RAMSize { get; set; }
        public string Destination { get; set; }
        public int Version { get; set; }
        public byte HeaderChecksum { get; set; }
        public short GlobalChecksum { get; set; }


        public RomHeader(byte[] headerData)
        {
            Version = headerData[0x14C];
            HeaderChecksum = headerData[0x14D];
            TypeCartouche = GetCartridgeType(headerData[0x147]);
            ROMSize = GetROMSize(headerData[0x148]);
            RAMSize = GetRAMSize(headerData[0x149]);
            ModeGBC = GetGBCMode(headerData[0x143]);
            Editor = GetGameEditor(headerData[0x144], headerData[0x145]);
            ModeSGB = headerData[0x146] == 0x03 ? "Compatible" : "Incompatible";
            Destination = headerData[0x14A] == 0x00 ? "Japon" : "Internationnal";
            GlobalChecksum = (short)(headerData[0x14F] + headerData[0x14E] * 0x100);

            var sb = new StringBuilder();

            for (int i = 0x134; i < 0x143; i++)
                sb.Append((char)headerData[i]);

            Title = sb.ToString();
        }

        private string GetCartridgeType(byte data) =>
            data switch
            {
                0x00 => "ROM",
                0x01 => "MBC1",
                0x02 => "MBC1+RAM",
                0x03 => "MBC1+RAM+BATTERY",
                0x05 => "MBC2",
                0x06 => "MBC2+BATTERY",
                0x08 => "ROM+RAM",
                0x09 => "ROM+RAM+BATTERY",
                0x0B => "MMM01",
                0x0C => "MMM01+RAM",
                0x0D => "MMM01+RAM+BATTERY",
                0x0F => "MBC3+TIMER+BATTERY",
                0x10 => "MBC3+TIMER+RAM+BATTERY",
                0x11 => "MBC3",
                0x12 => "MBC3+RAM",
                0x13 => "MBC3+RAM+BATTERY",
                0x14 => "MBC4",
                0x16 => "MBC4+RAM",
                0x17 => "MBC4+RAM+BATTERY",
                0x19 => "MBC5",
                0x1A => "MBC5+RAM",
                0x1B => "MBC5+RAM+BATTERY",
                0x1C => "MBC5+RUMBLE",
                0x1D => "MBC5+RUMBLE+BATTERY",
                0x1E => "MBC5+RUMBLE+RAM+BATTERY",
                0xFC => "POCKET CAMERA",
                0xFD => "Bandai TAMA5",
                0xFE => "HuC3",
                0xFF => "HuC1+RAM+BATTERY",
                _ => "UNKNOWN"
            };

        private int GetROMSize(byte data) =>
            data switch
            {
                0x00 => 32768,
                0x01 => 65536,
                0x02 => 131072,
                0x03 => 262144,
                0x04 => 524288,
                0x05 => 1048576,
                0x06 => 2097152,
                0x07 => 4194304,
                0x52 => 1153434,
                0x53 => 1258291,
                0x54 => 1572864,
                _ => int.MaxValue
            };

        private int GetRAMSize(byte data) =>
            data switch
            {
                0x00 => 0,
                0x01 => 2048,
                0x02 => 8192,
                0x03 => 32768,
                _ => int.MaxValue
            };

        private string GetGBCMode(byte data) =>
            data switch
            {
                0x00 => "Game Boy uniquement",
                0x80 => "Game Boy & Game Boy Color",
                0xC0 => "Game Boy Color uniquement",
                _ => "UNKNOWN"
            };

        private string GetGameEditor(byte firstByte, byte secondByte) =>
        ((secondByte & 0x0F) + (firstByte & 0x0F) * 0x10) switch
        {
            0x00 => "NONE",
            0x01 => "Nintendo",
            0x08 => "Capcom",
            0x13 => "Electronic Arts",
            0x18 => "Hudsonsoft",
            0x19 => "b-ai",
            0x20 => "kss",
            0x22 => "Pow",
            0x24 => "Pcm complete",
            0x25 => "San-X",
            0x28 => "Kemco Japan",
            0x29 => "Seta",
            0x30 => "Viacom",
            0x31 => "Nintendo",
            0x32 => "Bandia",
            0x33 => "Ocean / Acclaim",
            0x34 => "Konami",
            0x35 => "hector",
            0x37 => "Taito",
            0x38 => "Hudson",
            0x39 => "Banpresto",
            0x41 => "Ubisoft",
            0x42 => "Atlus",
            0x44 => "Malibu",
            0x46 => "Angel",
            0x47 => "Pullet-proof",
            0x49 => "Irem",
            0x50 => "Absolute",
            0x51 => "Acclaim",
            0x52 => "Activision",
            0x53 => "American Sammy",
            0x54 => "Konami",
            0x55 => "HiTech Entertainment",
            0x56 => "Ljn",
            0x57 => "Matchbox",
            0x58 => "Mattel",
            0x59 => "Milton Bradley",
            0x60 => "Titus",
            0x61 => "Virgin",
            0x64 => "Lucas Arts",
            0x67 => "Ocean",
            0x69 => "Electronic Arts",
            0x70 => "Infogrammes",
            0x71 => "Interplay",
            0x72 => "Broderbund",
            0x73 => "Sculptured",
            0x75 => "Sci",
            0x78 => "T*hq",
            0x79 => "Accolade",
            0x80 => "Misawa",
            0x83 => "Lozc",
            0x86 => "Tokuma Shoten i",
            0x87 => "Tsukuda Ori",
            0x91 => "Chun Soft",
            0x92 => "Video System",
            0x93 => "Ocean / Acclaim",
            0x95 => "Varie",
            0x96 => "Yonezawa/s'pal",
            0x97 => "Kaneko",
            0x99 => "Pack in Soft",
            0x9A => "nihon bussan",
            0x9C => "imagineer",
            0x9D => "banpresto",
            0xA1 => "hori electric",
            0xA2 => "bandai",
            0xA6 => "kawada",
            0xA7 => "takara",
            0xAA => "broderbund",
            0xAC => "toei animation",
            0xAF => "namco",
            0xB0 => "acclaim",
            0xB2 => "bandai",
            0xB4 => "enix",
            0xB7 => "snk",
            0xB9 => "pony canyon",
            0xBB => "sunsoft",
            0xBD => "sony imagesoft",
            0xC0 => "taito",
            0xC2 => "kemco",
            0xC4 => "*tokuma shoten i",
            0xC5 => "data east",
            0xC8 => "koei",
            0xC9 => "ufl",
            0xCB => "vap",
            0xCC => "use",
            0xCE => "*pony canyon or",
            0xCF => "angel",
            0xD1 => "sofel",
            0xD2 => "quest",
            0xD4 => "ask kodansha",
            0xD6 => "naxat soft",
            0xD9 => "banpresto",
            0xDA => "tomy",
            0xDD => "ncs",
            0xDE => "human",
            0xE0 => "jaleco",
            0xE1 => "towachiki",
            0xE3 => "varie",
            0xE5 => "epoch",
            0xE8 => "asmik",
            0xE9 => "natsume",
            0xEB => "atlus",
            0xEC => "epic/sony records",
            0xF0 => "a wave",
            0xF3 => "extreme entertaint",
            _ => "UNKNOWN",
        };
    }
}
