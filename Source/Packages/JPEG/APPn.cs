using System;
using System.IO;
namespace JPEG
{
    public sealed class APPn:Section
    {
        public AlignType AlignType;
        public short Mark;
        public IFD MainImage;
        public IFD ThumbnailImage;
        public override void Update(Stream stream)
        {
            byte[] bs = new byte[2];
            stream.Read(bs, 0, 2);
            int length = (bs[0] << 8 | bs[1]) - 2;
            bs = new byte[length];
            stream.Read(bs,0,length);
            if (bs[0] != 0x45 || bs[1] != 0x78 || bs[2] != 0x69 || bs[3] != 0x66 || bs[4] != 0 || bs[5] != 0) throw new System.Exception();
            AlignType = (AlignType)((bs[6] << 8) | bs[7]);
            if (AlignType != AlignType.II) throw new Exception();
            Mark = BitConverter.ToInt16(bs, 8);
            MainImage = new(bs, BitConverter.ToInt32(bs, 10));
            ThumbnailImage = new(bs,MainImage.NextIFDOffset);
        }
    }
}
