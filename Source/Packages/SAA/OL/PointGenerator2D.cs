using System;

namespace SAA.OL
{
    public class PointGeneratorOL : IPointGenerator<PointOL>
    {
        public PointOL Next(PointOL now, double T)
        {
            int[] values = new int[now.Indexs.Length];
            Array.Copy(now.Indexs, 0, values, 0, values.Length);
            int x = RandomHelper.Next(now.Indexs.Length);
            int y;
            do y = RandomHelper.Next(now.Indexs.Length); while (x == y);
            values[x] = now.Indexs[y];
            values[y] = now.Indexs[x];
            return new(values);
        }
    }
}
