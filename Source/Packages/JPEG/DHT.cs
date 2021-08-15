using System;
using System.IO;

namespace JPEG
{
    public sealed class DHT : Section
    {
        public int Info;
        public int[] Length;
        public int[] HTV;
        public DHTNode[] Nodes;
        public override void Update(Stream stream)
        {
            byte[] bs = new byte[2];
            stream.Read(bs, 0, 2);
            int length = (bs[0] << 8 | bs[1]) - 2;
            bs = new byte[length];
            stream.Read(bs, 0, length);
            Info = bs[0];
            Length = new int[16];
            int sum = 0;
            for (int i = 0; i < 16; i++)
                sum+=Length[i] = bs[i + 1];
            HTV = new int[sum];
            for (int i = 0; i < sum; i++)
                HTV[i] = bs[i + 17];
            Nodes = new DHTNode[sum];
            int value = 0;
            int index = 0;
            for(int i=0;i<16;i++)
            {
                value <<= 1;
                for(int j=0;j<Length[i];j++,index++)
                    Nodes[index] = new(i + 1, index, value++,HTV[index]);
            }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            for (int i = 0; i < sum; i++)
                Console.WriteLine($"{Nodes[i].Index}\t{Nodes[i]}\t{Nodes[i].Weight}");
        }
    }
}
