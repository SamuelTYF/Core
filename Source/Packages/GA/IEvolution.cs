namespace GA
{
    public interface IEvolution<TCode, TValue, TEntityCollection> where TEntityCollection : IEntityCollection<TCode, TValue> where TCode : ICode
    {
        public TEntityCollection Evolve(TEntityCollection current);
    }
}
