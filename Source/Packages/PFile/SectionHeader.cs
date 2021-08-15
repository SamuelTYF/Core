using System;
using System.IO;
namespace PFile
{
    public class SectionHeader
    {
        public long Offset;
        public long Length;
        public int Magic;
        public SectionHeader(Stream stream)
        {
            byte[] bs = new byte[20];
            stream.Read(bs, 0, 20);
            Offset = BitConverter.ToInt64(bs, 0);
            Length = BitConverter.ToInt64(bs, 8);
            Magic = BitConverter.ToInt32(bs, 16);
        }
        public SectionHeader(long offset,long length,int magic)
        {
            Offset = offset;
            Length = length;
            Magic = magic;
        }
    }
}
