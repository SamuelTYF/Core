using System;
using System.IO;

namespace BMP
{
    public class Header
    {
        public int Size;
        public int Offset;
        public Header(Stream stream)
        {
            byte[] bs = new byte[14];
            stream.Read(bs, 0, 14);
            if (bs[0] != 'B' || bs[1] != 'm') throw new Exception();
            Size = BitConverter.ToInt32(bs, 2);
            if(BitConverter.ToInt32(bs,6)!=0)throw new Exception();
            Offset = BitConverter.ToInt32(bs, 10);
        }
    }
}
