namespace CSharpScript.File
{
	public struct ClassLayoutTable
	{
		public int Count;
		public ClassLayout[] ClassLayouts;
		public ClassLayoutTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			ClassLayouts = new ClassLayout[count];
			bool islong = Rows[2] >> 16 != 0;
			for (int i = 0; i < count; i++)
				ClassLayouts[i] = new ClassLayout(new ClassLayoutRow
				{
					Index = i + 1,
					PackingSize = TildeHeap.Read(bs, ref index, islong: false),
					ClassSize = TildeHeap.Read(bs, ref index, islong: true),
					Parent = TildeHeap.Read(bs, ref index, islong)
				});
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (ClassLayouts != null)
				for (int i = 0; i < ClassLayouts.Length; i++)
					ClassLayouts[i].ResolveSignature(metadata);
		}
	}
}
