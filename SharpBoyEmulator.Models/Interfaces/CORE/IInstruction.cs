
namespace SharpBoyEmulator.Models
{
    public interface IInstruction
    {
        ushort Address { get; }
        string HexAddress { get; }
        string RawBytes { get; }
        IOpcode Opcode { get; }
        byte[] Parameters { get; }
        byte Parameter8 { get; }
        ushort Parameter16 { get; }
        string Description { get; }



        int Execute(IGameBoy device);
    }
}
