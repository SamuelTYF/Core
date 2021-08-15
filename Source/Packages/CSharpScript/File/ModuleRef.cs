namespace CSharpScript.File
{
	public class ModuleRef
	{
		public int Row;
		public string Name;
		public int Token;
		public ModuleRef(int index, int row)
		{
			Token = index | 0x1A000000;
			Row = row;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Name = metadata.StringsHeap[Row];
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
