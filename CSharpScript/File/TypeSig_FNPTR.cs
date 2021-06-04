namespace CSharpScript.File
{
	public class TypeSig_FNPTR : TypeSig
	{
		public MethodDefSig Method;
		public TypeSig_FNPTR(MethodDefSig method)
			: base(ElementType.ELEMENT_TYPE_FNPTR)
		{
			Method = method;
		}
		public static TypeSig_FNPTR AnalyzeType_FNPTR(byte[] bs, ref int index, MetadataRoot metadata)
		{
			return new TypeSig_FNPTR(new MethodDefSig(bs, ref index, metadata));
		}
		public override string ToString()
		{
			return $"{Method}*";
		}
	}
}
