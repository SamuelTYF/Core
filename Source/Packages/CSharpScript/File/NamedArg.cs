namespace CSharpScript.File
{
	public class NamedArg
	{
		public ElementType ElementType;
		public TypeSig Type;
		public string Name;
		public FixedArg Value;
		public NamedArg(byte[] bs, ref int index, MetadataRoot metadata, ref bool Success)
		{
			ElementType = (ElementType)bs[index++];
			Type = TypeSig.AnalyzeType(bs, ref index, metadata);
			Name = BlobHeap.ReadSerString(bs, ref index);
			Value = new FixedArg(bs, ref index, Type, metadata, ref Success);
		}
		public override string ToString()
		{
			return $"{Name}={Value}";
		}
	}
}
