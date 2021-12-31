namespace ISO
{
    public class VolumeDescriptor
    {
        public VolumeDescriptorType Type;
        public byte[] Identifier;
        public byte Version;
        public byte[] Data;
        public static byte[] StandardIndentifier = new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 };
        public VolumeDescriptor(ISOStreamReader sr)
        {
            Type = (VolumeDescriptorType)sr.ReadByte();
            Identifier = sr.ReadBytes(5);
            if (
                Identifier[0]!=StandardIndentifier[0]||
                Identifier[1] != StandardIndentifier[1]||
                Identifier[2] != StandardIndentifier[2]||
                Identifier[3] != StandardIndentifier[3]||
                Identifier[4] != StandardIndentifier[4]
                )throw new Exception("Identifier Error");
            Version = sr.ReadByte();
            if (Version != 1) throw new Exception("Version Error");
            Data = sr.ReadBytes(2041);
        }
        public bool IsTerminator() => Type == VolumeDescriptorType.VolumeDescriptorSetTerminator;
    }
}
