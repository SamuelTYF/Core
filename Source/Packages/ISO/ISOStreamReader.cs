namespace ISO
{
    public class ISOStreamReader
    {
        public Stream Stream;
        public ISOStreamReader(Stream stream) => Stream = stream;
        public byte ReadByte() => (byte)Stream.ReadByte();
        public byte[] ReadBytes(int length)
        {
            byte[] r = new byte[length];
            Stream.Read(r, 0, length);
            return r;
        }
        public DigitsZonedDateTime ReadDigitsZonedDateTime() => new(ReadBytes(17));
        public LB16 ReadLB16() => new(ReadBytes(4));
        public LB32 ReadLB32() => new(ReadBytes(8));
        public int ReadInt32() => BitConverter.ToInt32(ReadBytes(4), 0);
        public DateAndTime ReadDateAndTime() => new(ReadBytes(7));
    }
}
