namespace CSharpScript.File
{
	public class AssemblyRefOS
	{
		public int OSPlatformId;
		public int OSMajorVersion;
		public int OSMinorVersion;
		public AssemblyRef AssemblyRef;
		public AssemblyRefOSRow Row;
		public int Token;
		public AssemblyRefOS(AssemblyRefOSRow row)
			=>Token = (Row = row).Index | 0x25000000;
		public void ResolveSignature(MetadataRoot metadata)
		{
			OSPlatformId = Row.OSPlatformId;
			OSMajorVersion = Row.OSMajorVersion;
			OSMinorVersion = Row.OSMinorVersion;
			AssemblyRef = metadata.TildeHeap.AssemblyRefTable.AssemblyRefs[Row.AssemblyRef - 1];
		}
	}
}
