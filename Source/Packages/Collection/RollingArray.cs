namespace Collection
{
    public class RollingArray<TArray> where TArray : new()
    {
        public TArray ArrayOne;
        public TArray ArrayTwo;
        public bool IsOne;
        public TArray Current => IsOne ? ArrayOne : ArrayTwo;
        public TArray Next => IsOne ? ArrayTwo : ArrayOne;
        public RollingArray(TArray one, TArray two)
        {
            ArrayOne = one;
            ArrayTwo = two;
            IsOne = true;
        }
        public void Swap() => IsOne = !IsOne;
    }
}
