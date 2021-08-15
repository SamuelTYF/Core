namespace CSharpScript.File
{
	public class TypeSig_Class : TypeSig
	{
		public TypeDefOrRefOrSpecEncoded Encoded;
		public TypeSig_Class(TypeDefOrRefOrSpecEncoded encoded)
			: base(ElementType.ELEMENT_TYPE_CLASS)
		{
			Encoded = encoded;
		}
		public static TypeSig_Class AnalyzeType_Class(byte[] bs, ref int index, MetadataRoot metadata)
		{
			return new TypeSig_Class(new TypeDefOrRefOrSpecEncoded(bs, ref index, metadata));
		}
		public override string ToString()
		{
			return Encoded.ToString();
		}
	}
}
