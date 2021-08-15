namespace CSharpScript.File
{
	public class TypeSig_VAR : TypeSig
	{
		public int Number;
        public TypeSig_VAR(int number)
            : base(ElementType.ELEMENT_TYPE_VAR) => Number = number;
        public static TypeSig_VAR AnalyzeType_VAR(byte[] bs, ref int index) => new TypeSig_VAR(BlobHeap.ReadUnsigned(bs, ref index));
        public override string ToString() => (Number == 0) ? "T" : $"T{Number}";
    }
}
