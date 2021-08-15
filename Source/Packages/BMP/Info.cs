using System;
using System.IO;

namespace BMP
{
    public class Info
    {
        public int Width;
        public int Height;
        public int Planes;
        public int BitCount;
        public int BitCompression;
        public int SizeImage;
        public int X;
        public int Y;
        public int Used;
        public int Important;
        public Info(Stream stream)
        {
            byte[] bs = new byte[40];
            stream.Read(bs, 0, 40);
            if (BitConverter.ToInt32(bs, 0) != 40) throw new Exception();
            Width = BitConverter.ToInt32(bs, 4);
            Height= BitConverter.ToInt32(bs, 8);
            Planes = BitConverter.ToInt16(bs, 12);
            BitCount = BitConverter.ToInt16(bs, 14);
            BitCompression = BitConverter.ToInt16(bs, 16);
            SizeImage = BitConverter.ToInt32(bs, 20);
            X = BitConverter.ToInt32(bs, 24);
            Y = BitConverter.ToInt32(bs, 28);
            Used = BitConverter.ToInt32(bs, 32);
            Important = BitConverter.ToInt32(bs, 36);
        }
    }
}
