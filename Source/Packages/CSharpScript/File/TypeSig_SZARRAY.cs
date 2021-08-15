namespace CSharpScript.File
{
	public class TypeSig_SZARRAY : TypeSig
	{
		public TypeSig Type;
        public TypeSig_SZARRAY(TypeSig type)
            : base(ElementType.ELEMENT_TYPE_SZARRAY) => Type = type;
        public static TypeSig_SZARRAY AnalyzeType_SZARRAY(byte[] bs, ref int index, MetadataRoot metadata)
            => new(AnalyzeType(bs, ref index, metadata));
        public override string ToString() => $"{Type}[]";
    }
}
