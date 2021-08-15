namespace CSharpScript.File
{
	public class TypeSig_PTR : TypeSig
	{
		public TypeSig Type;
		public TypeSig_PTR(TypeSig type)
			: base(ElementType.ELEMENT_TYPE_PTR)
		{
			Type = type;
		}
		public static TypeSig_PTR AnalyzeType_PTR(byte[] bs, ref int index, MetadataRoot metadata)
		{
			return new TypeSig_PTR(TypeSig.AnalyzeType(bs, ref index, metadata));
		}
		public override string ToString()
		{
			return $"{Type}*";
		}
	}
}
