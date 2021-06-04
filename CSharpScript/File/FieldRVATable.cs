namespace CSharpScript.File
{
	public struct FieldRVATable
	{
		public int Count;
		public FieldRVA[] FieldRVAs;
		public FieldRVATable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			FieldRVAs = new FieldRVA[count];
			bool islong = Rows[4] >> 16 != 0;
			for (int i = 0; i < count; i++)
			{
				FieldRVAs[i] = new FieldRVA(new FieldRVARow
				{
					Index = i + 1,
					RVA = TildeHeap.Read(bs, ref index, islong: true),
					Field = TildeHeap.Read(bs, ref index, islong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (FieldRVAs != null)
			{
				FieldRVA[] fieldRVAs = FieldRVAs;
				for (int i = 0; i < fieldRVAs.Length; i++)
				{
					fieldRVAs[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
