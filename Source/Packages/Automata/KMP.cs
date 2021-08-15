namespace Automata
{
    public class KMP
    {
        public char[] Source;
        public int[] KMPs;
        public KMP(string source)
        {
            Source = source.ToCharArray();
            KMPs = new int[Source.Length];
            for(int i=1,j=0;i<Source.Length;i++)
            {
                while (j != 0 && Source[i] != Source[j]) j = KMPs[j-1];
                if (Source[i] == Source[j]) KMPs[i] = ++j;
            }
        }
        public int Run(string destination)
        {
            char[] dest = destination.ToCharArray();
            int sum = 0;
            for (int i = 0, j = 0; i < dest.Length; i++)
            {
                while (j != 0 && dest[i] != Source[j]) j = KMPs[j-1];
                if (dest[i] == Source[j])++j;
                if (j == Source.Length)
                {
                    sum++;
                    j = KMPs[^1];
                }
            }
            return sum;
        }
    }
}
