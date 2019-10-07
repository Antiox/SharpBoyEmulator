using SharpBoyEmulator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoyEmulator.Core
{
    public class Memory : IMemory
    {
        private readonly IGameBoy _device;
        private readonly byte[] _internalRam;
        private readonly byte[] _internalVideoRam;
        private readonly byte[] _oamMemory;
        private readonly byte[] _ioMemory;
        private readonly byte[] _highInternalRam;


        public Memory(IGameBoy device)
        {
            _device = device;
            _internalRam = new byte[0x2000];
            _internalVideoRam = new byte[0x2000];
            _oamMemory = new byte[0x00A0];
            _ioMemory = new byte[0x004C];
            _highInternalRam = new byte[0x007F];
        }


        public void Initialize()
        {
            WriteByte(0xFF00, 0x3F); // Joypad
            WriteByte(0xFF10, 0x80); // NR10
            WriteByte(0xFF11, 0xBF); // NR11
            WriteByte(0xFF12, 0xF3); // NR12
            WriteByte(0xFF14, 0xBF); // NR14
            WriteByte(0xFF16, 0x3F); // NR21
            WriteByte(0xFF19, 0xBF); // NR24
            WriteByte(0xFF1A, 0x7F); // NR30
            WriteByte(0xFF1B, 0xFF); // NR31
            WriteByte(0xFF1C, 0x9F); // NR32
            WriteByte(0xFF1E, 0xBF); // NR33
            WriteByte(0xFF20, 0xFF); // NR41
            WriteByte(0xFF23, 0xBF); // NR30
            WriteByte(0xFF24, 0x77); // NR50
            WriteByte(0xFF25, 0xF3); // NR51
            WriteByte(0xFF26, 0xF1); // NR52
            WriteByte(0xFF40, 0x91); // LCDC
            WriteByte(0xFF41, 0x06); // STAT
            WriteByte(0xFF47, 0xFC); // BPG
            WriteByte(0xFF48, 0xFF); // OBP0
            WriteByte(0xFF49, 0xFF); // OBP1
        }


        public void WriteByte(ushort address, byte value)
        {
            switch (address)
            {
                case ushort n when n < 0x8000: _device.Cartridge.WriteByte(address, value); break;
                case ushort n when n >= 0x8000 && n < 0xA000 : _internalVideoRam[n - 0x8000] = value; break;
                case ushort n when n >= 0xA000 && n < 0xC000 : _device.Cartridge.WriteByte(address, value); break;
                case ushort n when n >= 0xC000 && n < 0xE000 : _internalRam[n - 0xC000] = value; break;
                case ushort n when n >= 0xE000 && n < 0xFE00 : _internalRam[n - 0xE000] = value; break;
                case ushort n when n >= 0xFE00 && n < 0xFEA0 : _oamMemory[n - 0xFE00] = value; break;
                case ushort n when n >= 0xFEA0 && n < 0xFF00 : return;
                case ushort n when n >= 0xFF00 && n < 0xFF4C: _ioMemory[n - 0xFF00] = value; break;
                case ushort n when n >= 0xFF4C && n < 0xFFFF: _highInternalRam[n - 0xFF4C] = value; break;
                case ushort n when n == 0xFFFF: _device.Processor.Registers.EnabledInterrupts = value; break;
            }
        }
        public void WriteUShort(ushort address, ushort value)
        {
            WriteBytes(address, BitConverter.GetBytes(value));
        }
        private void WriteBytes(ushort address, byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
                WriteByte((ushort)(address + i), bytes[i]);
        }

        public byte ReadByte(ushort address)
        {
            return address switch
            {
                ushort n when n < 0x8000 => _device.Cartridge.ReadByte(address),
                ushort n when n >= 0x8000 && n < 0xA000 => _internalVideoRam[n - 0x8000],
                ushort n when n >= 0xA000 && n < 0xC000 => _device.Cartridge.ReadByte(address),
                ushort n when n >= 0xC000 && n < 0xE000 => _internalRam[n - 0xC000],
                ushort n when n >= 0xE000 && n < 0xFE00 => _internalRam[n - 0xE000],
                ushort n when n >= 0xFE00 && n < 0xFEA0 => _oamMemory[n - 0xFE00],
                ushort n when n >= 0xFEA0 && n < 0xFF00 => 0x00,
                ushort n when n >= 0xFF00 && n < 0xFF4C => _ioMemory[n - 0xFF00],
                ushort n when n >= 0xFF80 && n < 0xFFFF => _highInternalRam[n - 0xFF80],
                ushort n when n == 0xFFFF => _device.Processor.Registers.EnabledInterrupts,
                _ => 0x00,
            };
        }
        public ushort ReadUShort(ushort address)
        {
            return BitConverter.ToUInt16(ReadBytes(address, 2), 0);
        }
        public byte[] ReadBytes(ushort address, int length)
        {
            var result = new byte[length];
            for (int i = 0; i < length; i++)
                result[i] = ReadByte((ushort)(address + i));

            return result;
        }

    }
}
