namespace CSharpScript.File
{
	public struct ManifestResourceTable
	{
		public int Count;
		public ManifestResource[] ManifestResources;
		public ManifestResourceTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			ManifestResources = new ManifestResource[count];
			bool islong = Rows[38] >> 14 != 0 || Rows[35] >> 14 != 0 || Rows[39] >> 14 != 0;
			for (int i = 0; i < count; i++)
			{
				ManifestResources[i] = new ManifestResource(new ManifestResourceRow
				{
					Index = i + 1,
					Offset = TildeHeap.Read(bs, ref index, islong: true),
					Flags = TildeHeap.Read(bs, ref index, islong: true),
					Name = TildeHeap.Read(bs, ref index, isStringLong),
					Implementation = TildeHeap.Read(bs, ref index, islong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (ManifestResources != null)
			{
				ManifestResource[] manifestResources = ManifestResources;
				for (int i = 0; i < manifestResources.Length; i++)
				{
					manifestResources[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
