namespace CSharpScript.File
{
	public struct AssemblyRefProcessorTable
	{
		public int Count;
		public AssemblyRefProcessor[] AssemblyRefProcessors;
		public AssemblyRefProcessorTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			AssemblyRefProcessors = new AssemblyRefProcessor[count];
			bool islong = Rows[35] >> 16 != 0;
			for (int i = 0; i < count; i++)
				AssemblyRefProcessors[i] = new AssemblyRefProcessor(new AssemblyRefProcessorRow
				{
					Index = i + 1,
					Processor = TildeHeap.Read(bs, ref index, islong: true),
					AssemblyRef = TildeHeap.Read(bs, ref index, islong)
				});
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (AssemblyRefProcessors != null)
				for (int i = 0; i < AssemblyRefProcessors.Length; i++)
					AssemblyRefProcessors[i].ResolveSignature(metadata);
		}
	}
}
