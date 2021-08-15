namespace CSharpScript.File
{
	public class TypeSig_ByRef : TypeSig
	{
		public TypeSig Type;
		public TypeSig_ByRef(TypeSig type)
			: base(ElementType.ELEMENT_TYPE_BYREF)
		{
			Type = type;
		}
		public static TypeSig_ByRef AnaylzeType_ByRef(byte[] bs, ref int index, MetadataRoot metadata)
		{
			return new TypeSig_ByRef(TypeSig.AnalyzeType(bs, ref index, metadata));
		}
		public override string ToString()
		{
			return $"{Type}";
		}
	}
}
