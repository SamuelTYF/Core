namespace SAA
{
    public interface IPointGenerator<TPoint>where TPoint : IPoint
    {
        public TPoint Next(TPoint now,double T);
    }
}
