
namespace SharpBoyEmulator.Models
{
    public interface IGameBoy
    {
        IProcessor Processor { get; }
        IMemory Memory { get; }
        ICartridge Cartridge { get; }


        void LoadCartridge(byte[] data);
        IRomHeader GetRomHeader();
        void ResetEmulator();
        IMemoryCell[] GetMemoryCells(int startIndex, int endIndex);
        void Start();


        IInstruction GetInstruction(ushort address);
        IRegisters GetRegisters();
        void Step();
        void Initialize();
    }
}
