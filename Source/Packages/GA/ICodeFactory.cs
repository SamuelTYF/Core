namespace GA
{
    public interface ICodeFactory<TCode, TValue> where TCode : ICode
    {
        public TCode Encode(TValue value);
        public TValue Decode(TCode code);
        public bool IsLegal(TCode code);
        public bool IsLegal(TValue value);
        public void Random(out TCode code, out TValue value);
    }
}
