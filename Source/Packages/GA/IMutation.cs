namespace GA
{
    public interface IMutation<TCode>where TCode : ICode
    {
        public abstract TCode Mutation(TCode source);
    }
}
