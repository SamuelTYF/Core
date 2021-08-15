namespace GA
{
    public interface ICrossover<TCode> where TCode : ICode
    {
        public abstract void Crossover(TCode father, TCode mother,out TCode son,out TCode daughter);
    }
}
