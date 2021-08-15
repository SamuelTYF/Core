using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    public sealed class SOS : Section
    {
        public CompSOS[] Comps;
        public int Ss;
        public int Se;
        public int Ah;
        public int Al;
        public List<byte> Data;
        public override void Update(Stream stream)
        {
            byte[] bs = new byte[2];
            stream.Read(bs, 0, 2);
            int length = (bs[0] << 8 | bs[1]) - 2;
            bs = new byte[length];
            stream.Read(bs, 0, length);
            int count = bs[0];
            Comps = new CompSOS[count];
            for (int i = 0; i < count; i++)
                Comps[i] = new(bs[1 + i * 2], bs[2 + i * 2] >> 4, bs[2 + i * 2] & 0xF);
            Ss = bs[count * 2 + 1];
            Se = bs[count * 2 + 2];
            Ah = bs[count * 2 + 3] >> 4;
            Al = bs[count * 2 + 3] & 0xF;
            Data = new();
            bool run = true;
            while (run)
            {
                int t = stream.ReadByte();
                if (t == 0xFF)
                {
                    t = stream.ReadByte();
                    switch (t)
                    {
                        case 0x00: Data.Add(255);break;
                        case 0xD9: run = false; break;
                        case >= 0xD0 and <= 0xD7:
                            throw new Exception();
                        case 0xFF: throw new Exception();
                        default: Data.Add((byte)t);break;
                    }
                }
                else Data.Add((byte)t);
            }
        }
    }
}
