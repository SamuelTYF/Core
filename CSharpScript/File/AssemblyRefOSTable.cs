namespace CSharpScript.File
{
	public struct AssemblyRefOSTable
	{
		public int Count;
		public AssemblyRefOS[] AssemblyRefOSs;
		public AssemblyRefOSTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			AssemblyRefOSs = new AssemblyRefOS[count];
			bool islong = Rows[35] >> 16 != 0;
			for (int i = 0; i < count; i++)
				AssemblyRefOSs[i] = new AssemblyRefOS(new AssemblyRefOSRow
				{
					Index = i + 1,
					OSPlatformId = TildeHeap.Read(bs, ref index, islong: true),
					OSMajorVersion = TildeHeap.Read(bs, ref index, islong: true),
					OSMinorVersion = TildeHeap.Read(bs, ref index, islong: true),
					AssemblyRef = TildeHeap.Read(bs, ref index, islong)
				});
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (AssemblyRefOSs != null)	
				for (int i = 0; i < AssemblyRefOSs.Length; i++)
					AssemblyRefOSs[i].ResolveSignature(metadata);
		}
	}
}
