namespace CSharpScript.File
{
	public struct FileTable
	{
		public int Count;
		public File[] Files;
		public FileTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Count = count;
			Files = new File[count];
			for (int i = 0; i < count; i++)
			{
				Files[i] = new File(new FileRow
				{
					Index = i + 1,
					Flags = TildeHeap.Read(bs, ref index, islong: true),
					Name = TildeHeap.Read(bs, ref index, isStringLong),
					HashValue = TildeHeap.Read(bs, ref index, isBlobLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (Files != null)
			{
				File[] files = Files;
				for (int i = 0; i < files.Length; i++)
				{
					files[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
