using System.IO;

namespace JPEG
{
    public class DQT : Section
    {
        public int Accuracy;
        public int Bit;
        public int ID;
        public int[] Value;
        public override void Update(Stream stream)
        {
            byte[] bs = new byte[2];
            stream.Read(bs, 0, 2);
            int length = (bs[0] << 8 | bs[1]) - 2;
            bs = new byte[length];
            stream.Read(bs, 0, length);
            Accuracy = bs[0] >> 4;
            Bit = Accuracy + 1;
            ID = bs[0] & 0xF;
            Value = new int[64];
            for (int i = 0; i < 64; i++)
                Value[i] = GetInt(bs, 1+i*Bit, Bit);
        }
        public static int GetInt(byte[] bs,int index,int count)
        {
            int r = 0;
            for (int i = count - 1; i >= 0; i--)
                r = (r << 8) | bs[index + i];
            return r;
        }
    }
}
