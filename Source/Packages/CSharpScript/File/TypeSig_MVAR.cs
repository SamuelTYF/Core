namespace CSharpScript.File
{
	public class TypeSig_MVAR : TypeSig
	{
		public int Number;
		public TypeSig_MVAR(int number)
			: base(ElementType.ELEMENT_TYPE_MVAR)
		{
			Number = number;
		}
		public static TypeSig_MVAR AnalyzeType_MVAR(byte[] bs, ref int index)
		{
			return new TypeSig_MVAR(BlobHeap.ReadUnsigned(bs, ref index));
		}
		public override string ToString()
		{
			return (Number == 0) ? "T" : $"T{Number + 1}";
		}
	}
}
