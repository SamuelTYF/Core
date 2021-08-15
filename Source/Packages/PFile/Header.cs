using System;
using System.IO;
namespace PFile
{
    public class Header
    {
        public int Magic;
        public int Version;
        public int SectionCount;
        public long SectionHeaderOffset;
        public Header(Stream stream)
        {
            byte[] bs = new byte[20];
            stream.Read(bs, 0, 16);
            Magic = BitConverter.ToInt32(bs, 0);
            Version = BitConverter.ToInt32(bs, 4);
            SectionCount=BitConverter.ToInt32(bs, 8);
            SectionHeaderOffset = BitConverter.ToInt64(bs, 12);
        }
    }
}
