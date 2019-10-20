
namespace SharpBoyEmulator.Models
{
    public interface IProcessor
    {
        IRegisters Registers { get; }


        void Step();
        void Initialize();



        byte Add(byte value);
        ushort Add(ushort value1, ushort value2);
        void AddSp(byte value);
        byte Adc(byte value);
        byte Sub(byte value);
        byte Sbc(byte value);
        byte And(byte value);
        byte Xor(byte value);
        byte Or(byte value);
        void Cp(byte value);
        byte Inc(byte value);
        byte Dec(byte value);


        void Push(ushort value);
        ushort Pop();
        
        
        byte Rlc(byte value);
        void Rlca();
        byte Rl(byte value);
        void Rla();
        byte Rrc(byte value);
        void Rrca();
        byte Rr(byte value);
        void Rra();
        byte Swap(byte value);
        byte Set(byte value, byte pos);
        byte Res(byte value, byte pos);
        void Bit(byte value, byte pos);
        byte Sla(byte value);
        byte Sra(byte value);
        byte Srl(byte value);

        void LdHlSpr8(byte value);
        void Daa();
        void Cpl();
        void Ccf();
        void Scf();
        void Halt();
        void Stop();

        int Jr(byte flag, byte condition, byte jumpTo, IOpcode opcode);
        int Jp(byte flag, byte condition, ushort jumpTo, IOpcode opcode);
        int Ret(byte flag, byte condition, IOpcode opcode);
        void Rst(byte address);
        int Call(byte flag, byte condition, ushort jumpTo, IOpcode opcode);
    }
}
