namespace CSharpScript.File
{
	public class TypeSig_ValueType : TypeSig
	{
		public TypeDefOrRefOrSpecEncoded Encoded;
        public TypeSig_ValueType(TypeDefOrRefOrSpecEncoded encoded)
            : base(ElementType.ELEMENT_TYPE_VALUETYPE) => Encoded = encoded;
        public static TypeSig_ValueType AnalyzeType_ValueType(byte[] bs, ref int index, MetadataRoot metadata)
            => new(new TypeDefOrRefOrSpecEncoded(bs, ref index, metadata));
        public override string ToString() => Encoded.ToString();
    }
}
