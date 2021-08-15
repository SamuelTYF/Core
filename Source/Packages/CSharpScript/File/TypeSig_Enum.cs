namespace CSharpScript.File
{
	public class TypeSig_Enum : TypeSig
	{
		public string Name;
		public TypeSig_Enum(string name)
			: base(ElementType.ELEMENT_TYPE_ENUM)
		{
			Name = name;
		}
		public static TypeSig_Enum AnalyzeType_Enum(byte[] bs, ref int index)
		{
			return new TypeSig_Enum(BlobHeap.ReadSerString(bs, ref index));
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
