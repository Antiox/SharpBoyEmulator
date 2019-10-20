namespace SharpBoyEmulator.Models
{
    public interface IRomHeader
    {
        string Title { get; set; }
        string ModeGBC { get; set; }
        string Editor { get; set; }
        string ModeSGB { get; set; }
        string TypeCartouche { get; set; }
        int ROMSize { get; set; }
        int RAMSize { get; set; }
        string Destination { get; set; }
        int Version { get; set; }
        byte HeaderChecksum { get; set; }
        short GlobalChecksum { get; set; }
    }
}
