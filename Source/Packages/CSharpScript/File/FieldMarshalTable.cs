namespace CSharpScript.File
{
	public struct FieldMarshalTable
	{
		public int Count;
		public FieldMarshal[] FieldMarshals;
		public FieldMarshalTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			FieldMarshals = new FieldMarshal[count];
			bool islong = Rows[8] >> 15 != 0 || Rows[4] >> 15 != 0;
			for (int i = 0; i < count; i++)
			{
				FieldMarshals[i] = new FieldMarshal(new FieldMarshalRow
				{
					Index = i + 1,
					Parent = TildeHeap.Read(bs, ref index, islong),
					NativeType = TildeHeap.Read(bs, ref index, isBlobLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (FieldMarshals != null)
			{
				FieldMarshal[] fieldMarshals = FieldMarshals;
				for (int i = 0; i < fieldMarshals.Length; i++)
				{
					fieldMarshals[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
