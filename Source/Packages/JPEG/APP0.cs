using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    public class APP0 : Section
    {
        public int Major;
        public int Minor;
        public int Units;
        public int XDensity;
        public int YDensity;
        public int XThumbnail;
        public int YThumbnail;
        public override void Update(Stream stream)
        {
            byte[] bs = new byte[2];
            stream.Read(bs, 0, 2);
            int length = (bs[0] << 8 | bs[1]) - 2;
            bs = new byte[length];
            stream.Read(bs, 0, length);
            Major = bs[5];
            Minor = bs[6];
            Units = bs[7];
            XDensity = (bs[8]<<8) | bs[9];
            YDensity = (bs[10]<<8) | bs[11];
            XThumbnail = bs[12];
            YThumbnail = bs[13];
        }
    }
}
