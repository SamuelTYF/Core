namespace ISO
{
    public class BootRecordDescriptor
    {
        public byte[] BootSystemId;
        public byte[] BootId;
        public byte[] SystemUsed;
        public static BootRecordDescriptor Parse(byte[] value)
        {
            using MemoryStream ms = new(value);
            return new(new ISOStreamReader(ms));
        }
        public BootRecordDescriptor(ISOStreamReader sr)
        {
            BootSystemId = sr.ReadBytes(32);
            BootId = sr.ReadBytes(32);
            SystemUsed = sr.ReadBytes(1970);
        }
    }
}
