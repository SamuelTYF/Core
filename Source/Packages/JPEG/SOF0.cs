using System;
using System.IO;
namespace JPEG
{
    public sealed class SOF0:Section
    {
        public byte Precision;
        public int Y;
        public int X;
        public Comp[] Comps;
        public override void Update(Stream stream)
        {
            byte[] bs = new byte[2];
            stream.Read(bs, 0, 2);
            int length = (bs[0] << 8 | bs[1]) - 2;
            bs = new byte[length];
            stream.Read(bs, 0, length);
            Precision = bs[0];
            Y = BitConverter.ToInt16(bs, 1);
            X = BitConverter.ToInt16(bs, 3);
            int count = bs[5];
            Comps=new Comp[count];
            for (int i = 0; i < count; i++)
                Comps[i] = new(bs[6 + i * 3], bs[7 + i * 3] >> 4, bs[7 + i * 3] & 0xF, bs[8 + i * 3]);
        }
    }
}
