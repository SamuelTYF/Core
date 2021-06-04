namespace CSharpScript.File
{
	public struct AssemblyOSTable
	{
		public int Count;
		public AssemblyOSRow[] AssemblyOSRows;
		public AssemblyOSTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Count = count;
			AssemblyOSRows = new AssemblyOSRow[count];
			for (int i = 0; i < count; i++)
				AssemblyOSRows[i] = new AssemblyOSRow
				{
					OSPlatformID = TildeHeap.Read(bs, ref index, islong: true),
					OSMajorVersion = TildeHeap.Read(bs, ref index, islong: true),
					OSMinorVersion = TildeHeap.Read(bs, ref index, islong: true)
				};
		}
	}
}
