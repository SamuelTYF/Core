using System;

namespace SAA.OL
{
    public class PointOL:IPoint
    {
        public int[] Indexs;
        public PointOL(int[] indexs) => Indexs = indexs;
        public static PointOL GetRandom(int length)
        {
            double[] r = new double[length];
            int[] indexs=new int[length];
            for (int i = 0; i < length; i++)
            {
                r[i] = RandomHelper.NextDouble();
                indexs[i] = i;
            }
            Array.Sort(indexs, (a, b) => indexs[a].CompareTo(indexs[b]));
            return new(indexs);

        }
    }
}
