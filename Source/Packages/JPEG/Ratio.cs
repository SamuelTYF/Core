namespace JPEG
{
    public struct Ratio
    {
        public int X;
        public int Y;
        public Ratio(int x,int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString() => $"{X}/{Y}";
    }
}
