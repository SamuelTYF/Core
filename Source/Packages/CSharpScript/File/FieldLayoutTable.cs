namespace CSharpScript.File
{
	public struct FieldLayoutTable
	{
		public int Count;
		public FieldLayout[] FieldLayouts;
		public FieldLayoutTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			FieldLayouts = new FieldLayout[count];
			bool islong = Rows[4] >> 16 != 0;
			for (int i = 0; i < count; i++)
			{
				FieldLayouts[i] = new FieldLayout(new FieldLayoutRow
				{
					Index = i + 1,
					Offset = TildeHeap.Read(bs, ref index, islong: true),
					Field = TildeHeap.Read(bs, ref index, islong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (FieldLayouts != null)
			{
				FieldLayout[] fieldLayouts = FieldLayouts;
				for (int i = 0; i < fieldLayouts.Length; i++)
				{
					fieldLayouts[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
