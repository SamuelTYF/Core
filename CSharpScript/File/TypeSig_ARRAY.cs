namespace CSharpScript.File
{
	public class TypeSig_ARRAY : TypeSig
	{
		public TypeSig Type;
		public ArrayShape Array;
		public TypeSig_ARRAY(TypeSig type, ArrayShape array)
			: base(ElementType.ELEMENT_TYPE_ARRAY)
		{
			Type = type;
			Array = array;
		}
		public static TypeSig_ARRAY AnalyzeType_ARRAY(byte[] bs, ref int index, MetadataRoot metadata)
		{
			return new TypeSig_ARRAY(TypeSig.AnalyzeType(bs, ref index, metadata), new ArrayShape(bs, ref index));
		}
		public override string ToString()
		{
			return $"{Type}{Array}";
		}
	}
}
