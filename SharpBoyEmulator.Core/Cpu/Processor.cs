using System;
using System.Collections.Generic;
using System.Text;
using SharpBoyEmulator.Models;

namespace SharpBoyEmulator.Core
{
    public class Processor : IProcessor
    {
        public IRegisters Registers { get; private set; }


        private readonly IGameBoy _device;


        public Processor(IGameBoy device)
        {
            _device = device;
            Registers = new Registers();
        }


        public void Initialize()
        {
            Registers.Initialize();
        }

        public void Step()
        {
            var currentInstruction =_device.GetInstruction(Registers.PC);
            var cycle = currentInstruction.Execute(_device);
            Registers.PC += (ushort)currentInstruction.Opcode.PC;
            Registers.CycleCount += cycle;
        }



        public byte Add(byte value)
        {
            var temp = Registers.A + value;
            Registers.NFlag = 0;
            Registers.HFlag = (byte)(((Registers.A & 0x0F) + (value & 0x0F)) > 0x0F ? 1 : 0);
            Registers.CFlag = (byte)(temp > 0xFF ? 1 : 0);
            Registers.ZFlag = (byte)(temp == 0 ? 1 : 0);
            return (byte)temp;
        }
        public ushort Add(ushort value1, ushort value2)
        {
            var temp = value1 + value2;
            Registers.NFlag = 0;
            Registers.HFlag = (byte)((value1 & 0xFFF) + (value2 & 0xFFF) > 0xFFF ? 1 : 0);
            Registers.CFlag = (byte)(temp > 0xFFFF ? 1 : 0);
            value1 = (ushort)temp;
            return value1;
        }
        public void AddSp(byte value)
        {
            var valueTemp = (ushort)(sbyte)value;
            var temp = Registers.SP + valueTemp;
            Registers.HFlag = (byte)(((Registers.SP ^ valueTemp ^ temp) & 0x10) != 0 ? 1 : 0);
            Registers.CFlag = (byte)(((Registers.SP ^ valueTemp ^ temp) & 0x100) != 0 ? 1 : 0);
            Registers.SP = (ushort)temp;
            Registers.F &= 0x30;
        }
        public byte Adc(byte value)
        {
            int temp;
            if (Registers.CFlag != 0)
            {
                Registers.HFlag = (byte)((Registers.A & 0xF) + (value & 0xF) > 0xE ? 1 : 0);
                temp = (Registers.A + value + 1);
            }
            else
            {
                Registers.HFlag = (byte)((Registers.A & 0xF) + (value & 0xF) > 0xF ? 1 : 0);
                temp = (Registers.A + value);
            }
            Registers.CFlag = (byte)(temp > 0xFF ? 1 : 0);
            Registers.ZFlag = (byte)((temp == 0) ? 1 : 0);
            Registers.NFlag = 0;
            return (byte)temp;
        }
        public byte Sub(byte value)
        {
            var temp = 0;
            Registers.NFlag = 1;
            Registers.HFlag = (byte)((Registers.A & 0xF) < (value & 0xF) ? 1 : 0);
            Registers.CFlag = (byte)(Registers.A < value ? 1 : 0);
            temp = (byte)(Registers.A - value);
            Registers.ZFlag = (byte)((temp == 0) ? 1 : 0);
            return (byte)temp;
        }
        public byte Sbc(byte value)
        {
            int temp;
            Registers.NFlag = 1;
            if (Registers.CFlag != 0)
            {
                Registers.CFlag = (byte)(Registers.A - value < 1 ? 1 : 0);
                Registers.HFlag = (byte)((Registers.A & 0xF) - (value & 0xF) < 1 ? 1 : 0);
                temp = (byte)(Registers.A - value - 1);
            }
            else
            {
                Registers.CFlag = (byte)(Registers.A < value ? 1 : 0);
                Registers.HFlag = (byte)((Registers.A & 0xF) < (value & 0xF) ? 1 : 0);
                temp = (byte)(Registers.A - value);
            }
            Registers.ZFlag = (byte)(temp == 0 ? 1 : 0);

            return (byte)temp;
        }
        public byte And(byte value)
        {
            var temp = Registers.A & value;
            Registers.ZFlag = (byte)(temp == 0 ? 1 : 0);
            Registers.NFlag = 0;
            Registers.HFlag = 1;
            Registers.CFlag = 0;
            return (byte)temp;
        }
        public byte Xor(byte value)
        {
            var temp = Registers.A ^ value;
            Registers.ZFlag = (byte)(temp == 0 ? 1 : 0);
            Registers.NFlag = 0;
            Registers.HFlag = 0;
            Registers.CFlag = 0;
            return (byte)temp;
        }
        public byte Or(byte value)
        {
            var temp = Registers.A | value;
            Registers.ZFlag = (byte)(temp == 0 ? 1 : 0);
            Registers.NFlag = 0;
            Registers.HFlag = 0;
            Registers.CFlag = 0;
            return (byte)temp;
        }
        public void Cp(byte value)
        {
            Registers.ZFlag = (byte)(Registers.A == value ? 1 : 0);
            Registers.NFlag = 1;
            Registers.HFlag = (byte)((Registers.A & 0xF) < (value & 0xF) ? 1 : 0);
            Registers.CFlag = (byte)((Registers.A < value) ? 1 : 0);
        }
        public byte Inc(byte value)
        {
            value = (byte)(++value & 0xFF);
            Registers.ZFlag = (byte)(value == 0 ? 1 : 0);
            Registers.HFlag = (byte)((value & 0xF) == 0 ? 1 : 0);
            Registers.NFlag = 0;

            return value;
        }
        public byte Dec(byte value)
        {
            byte save = value;
            value = (byte)(--value & 0xFF);
            Registers.ZFlag = (byte)(value == 0 ? 1 : 0);
            Registers.HFlag = (byte)((save & 0xF) == 0 ? 1 : 0);
            Registers.NFlag = 1;

            return value;
        }


        public void Push(ushort value)
        {
            _device.Memory.WriteByte(--Registers.SP, (byte)(value >> 8)); // Ecriture du byte de poids faible dans la pile
            _device.Memory.WriteByte(--Registers.SP, (byte)value); // Ecriture du byte de poids fort dans la pile
        }
        public ushort Pop()
        {
            var octetFaible = _device.Memory.ReadByte(Registers.SP++);
            var octetFort = _device.Memory.ReadByte(Registers.SP++);
            return (ushort)(octetFort << 8 | octetFaible);
        }


        public byte Rlc(byte value)
        {
            Registers.CFlag = (byte)((value & 0x80) != 0 ? 1 : 0);
            value = (byte)((value & 0x80) != 0 ? (value << 1) | 0x01 : value << 1);
            Registers.ZFlag = (byte)((value == 0) ? 1 : 0);
            Registers.F &= 0x90;
            return value;
        }
        public void Rlca()
        {
            if ((Registers.A & 0x80) != 0)
            {
                Registers.CFlag = 1;
                Registers.A = (byte)((Registers.A << 1) | 0x01);
            }
            else
            {
                Registers.F = 0;
                Registers.A <<= 1;
            }
            Registers.F &= 0x10;
        }
        public byte Rl(byte value)
        {
            var bit = (byte)((value & 128) >> 7);
            var result = (byte)((value << 1) | Registers.CFlag);
            Registers.ZFlag = (byte)((result == 0) ? 1 : 0);
            Registers.NFlag = 0;
            Registers.HFlag = 0;
            Registers.CFlag = bit;
            return result;
        }
        public void Rla()
        {
            if (Registers.CFlag != 0)
            {
                Registers.CFlag = (byte)((Registers.A & 0x80) != 0 ? 1 : 0);
                Registers.A = (byte)((Registers.A << 1) | 0x01);
            }
            else
            {
                Registers.CFlag = (byte)((Registers.A & 0x80) != 0 ? 1 : 0);
                Registers.A <<= 1;
            }
            Registers.F &= 0x10;
        }
        public byte Rrc(byte value)
        {
            Registers.CFlag = (byte)((value & 0x01) != 0 ? 1 : 0);
            value = (byte)((value & 0x01) != 0 ? (value >> 1) | 0x80 : value >> 1);
            Registers.ZFlag = (byte)(value == 0 ? 1 : 0);
            Registers.F &= 0x90;
            return value;
        }
        public void Rrca()
        {
            if ((Registers.A & 0x01) != 0)
            {
                Registers.CFlag = 1;
                Registers.A = (byte)((Registers.A >> 1) | 0x80);
            }
            else
            {
                Registers.F = 0;
                Registers.A >>= 1;
            }
            Registers.F &= 0x10;
        }
        public byte Rr(byte value)
        {
            var bit = (byte)(value & 1);
            var result = (byte)((value >> 1) | (Registers.CFlag << 7));
            Registers.ZFlag = (byte)((result == 0) ? 1 : 0);
            Registers.NFlag = 0;
            Registers.HFlag = 0;
            Registers.CFlag = bit;
            return result;
        }
        public void Rra()
        {
            if (Registers.CFlag != 0)
            {
                Registers.CFlag = (byte)((Registers.A & 0x01) != 0 ? 1 : 0);
                Registers.A = (byte)((Registers.A >> 1) | 0x80);
            }
            else
            {
                Registers.CFlag = (byte)((Registers.A & 0x01) != 0 ? 1 : 0);
                Registers.A >>= 1;
            }
            Registers.F &= 0x10;
        }
        public byte Swap(byte value)
        {
            var b = (byte)((value << 4) | (value >> 4));
            Registers.ZFlag = (byte)((value == 0) ? 1 : 0);
            Registers.NFlag = 0;
            Registers.HFlag = 0;
            Registers.CFlag = 0;
            return b;
        }
        public byte Set(byte value, byte pos)
        {
            var bit = (byte)(0x01 << pos);
            return (byte)(value | bit);
        }
        public byte Res(byte value, byte pos)
        {
            var mask = (byte)(1 << pos);
            value &= (byte)~mask;
            return value;
        }
        public void Bit(byte value, byte pos)
        {
            Registers.ZFlag = (byte)(((0x01 << pos) & value) == 0 ? 1 : 0);
            Registers.NFlag = 0;
            Registers.HFlag = 1;
        }
        public byte Sla(byte value)
        {
            Registers.NFlag = 0;
            Registers.HFlag = 0;
            Registers.CFlag = (byte)((value & 0x80) != 0 ? 1 : 0);
            value <<= 1;
            Registers.ZFlag = (byte)(value == 0 ? 1 : 0);

            return value;
        }
        public byte Sra(byte value)
        {
            Registers.CFlag = (byte)((value & 0x01) != 0 ? 1 : 0);
            Registers.NFlag = 0;
            Registers.HFlag = 0;
            value = (byte)((value >> 1) | (value & 0x80));
            Registers.ZFlag = (byte)(value == 0 ? 1 : 0);
            return value;
        }
        public byte Srl(byte value)
        {
            Registers.CFlag = (byte)((value & 0x01) != 0 ? 1 : 0);
            Registers.NFlag = 0;
            Registers.HFlag = 0;
            value >>= 1;
            Registers.ZFlag = (byte)(value == 0 ? 1 : 0);
            return value;
        }


        public void LdHlSpr8(byte value)
        {
            // Chargement de (SP + value) dans le registre HL
            var tempValue = (ushort)(sbyte)value;
            var temp = Registers.SP + tempValue;
            Registers.HFlag = (byte)(((Registers.SP ^ tempValue ^ temp) & 0x10) != 0 ? 1 : 0);
            Registers.CFlag = (byte)(((Registers.SP ^ tempValue ^ temp) & 0x100) != 0 ? 1 : 0);
            Registers.ZFlag = 0;
            Registers.NFlag = 0;
            Registers.HL = (ushort)temp;
        }
        public void Daa()
        {
            if (Registers.NFlag != 0)
            {
                if (Registers.HFlag != 0) Registers.A -= 0x06;
                if (Registers.CFlag != 0) Registers.A -= 0x60;
            }
            else
            {
                if (Registers.CFlag != 0 || Registers.A > 0x99)
                {
                    Registers.A += Registers.HFlag != 0 || (Registers.A & 0x0F) > 0x09 ? (byte)0x66 : (byte)0x60;
                    Registers.CFlag = 1;
                }
                else if (Registers.HFlag != 0 || (Registers.A & 0x0F) > 0x09) Registers.A += 0x06;
            }
            Registers.ZFlag = (byte)(Registers.A == 0 ? 1 : 0);
            Registers.F &= 0xD0;
        }
        public void Cpl()
        {
            // Complément du registre A
            Registers.A = (byte)~Registers.A;
            Registers.NFlag = 1;
            Registers.HFlag = 1;
        }
        public void Ccf()
        {
            // Complément du Carry Flag
            Registers.NFlag = 0;
            Registers.HFlag = 0;
            Registers.CFlag ^= 0x01;
        }
        public void Scf()
        {
            // Mise à 1 du Carry Flag
            Registers.NFlag = 0;
            Registers.HFlag = 0;
            Registers.CFlag = 1;
        }
        public void Halt()
        {
            throw new NotImplementedException();
        }
        public void Stop()
        {
            throw new NotImplementedException();
        }


        public int Jr(byte flag, byte condition, byte jumpTo, IOpcode opcode)
        {
            if (flag == condition)
            {
                Registers.PC = (ushort)(Registers.PC + (sbyte)jumpTo + 2);
                return opcode.Cycle;
            }
            else
            {
                Registers.PC += 2;
                return opcode.VariableCycle;
            }
        }
        public int Jp(byte flag, byte condition, ushort jumpTo, IOpcode opcode)
        {
            if (flag == condition)
            {
                Registers.PC = jumpTo;
                return opcode.Cycle;
            }
            else
            {
                Registers.PC += 3;
                return opcode.VariableCycle;
            }
        }
        public int Ret(byte flag, byte condition, IOpcode opcode)
        {
            if (flag == condition)
            {
                Registers.PC = Pop();
                return opcode.Cycle;
            }
            else
            {
                Registers.PC++;
                return opcode.VariableCycle;
            }
        }
        public void Rst(byte address)
        {
            Push((ushort)(Registers.PC + 1));
            Registers.PC = address;
        }
        public int Call(byte flag, byte condition, ushort jumpTo, IOpcode opcode)
        {
            if (flag == condition)
            {
                Push((ushort)(Registers.PC + 3));
                Registers.PC = jumpTo;
                return opcode.Cycle ;
            }
            else
            {
                Registers.PC += 3;
                return opcode.VariableCycle;
            }
        }
    }
}
