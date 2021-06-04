namespace CSharpScript.File
{
	public class File
	{
		public FileAttributes Flags;
		public string Name;
		public byte[] HashValue;
		public FileRow Row;
		public int Token;
		public ManifestResource ManifestResource;
		public File(FileRow row)
		{
			FileRow fileRow = (Row = row);
			Token = fileRow.Index | 0x26000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Flags = (FileAttributes)Row.Flags;
			Name = metadata.StringsHeap[Row.Name];
			HashValue = metadata.BlobHeap.Values[Row.HashValue];
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
