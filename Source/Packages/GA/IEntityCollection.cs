namespace GA
{
    public interface IEntityCollection<TCode, TValue> where TCode : ICode
    {
        public int Generation { get; }
        public int Size { get; }
        public Entity<TCode, TValue>[] Entities { get; }
        public Assess<TValue> Assess { get; }
        public INormalize Normalize { get; }
        public Entity<TCode, TValue> this[int index] { get;set; }
        public void Inital(ICodeFactory<TCode, TValue> factory);
        public int SelectSingle();
        public void SelectPair(out int father, out int mother);
        public void RandomSelectPair(out int father, out int mother);
    }
}
