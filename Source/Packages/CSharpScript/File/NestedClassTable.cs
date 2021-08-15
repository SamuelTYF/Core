namespace CSharpScript.File
{
	public struct NestedClassTable
	{
		public int Count;
		public NestedClass[] NestedClasses;
		public NestedClassTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			NestedClasses = new NestedClass[count];
			bool islong = Rows[2] >> 16 != 0;
			for (int i = 0; i < count; i++)
			{
				NestedClasses[i] = new NestedClass(new NestedClassRow
				{
					Index = i + 1,
					NestedClass = TildeHeap.Read(bs, ref index, islong),
					EnclosingClass = TildeHeap.Read(bs, ref index, islong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (NestedClasses != null)
			{
				NestedClass[] nestedClasses = NestedClasses;
				foreach (NestedClass nestedClass in nestedClasses)
				{
					nestedClass.ResolveSignature(metadata);
					nestedClass.EnclosingClass.NestedClasses.Add(nestedClass.Class);
				}
			}
		}
	}
}
