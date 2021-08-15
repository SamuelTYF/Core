namespace CSharpScript.File
{
	public struct FieldTable
	{
		public Field[] Fields;
		public int Count;
		public FieldTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Count = count;
			Fields = new Field[count];
			for (int i = 0; i < count; i++)
			{
				Fields[i] = new Field(new FieldRow
				{
					Index = i + 1,
					Flags = TildeHeap.Read(bs, ref index, islong: false),
					Name = TildeHeap.Read(bs, ref index, isStringLong),
					Signature = TildeHeap.Read(bs, ref index, isBlobLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (Fields != null)
			{
				Field[] fields = Fields;
				for (int i = 0; i < fields.Length; i++)
				{
					fields[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
