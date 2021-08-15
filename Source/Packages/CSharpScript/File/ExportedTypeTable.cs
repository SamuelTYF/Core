namespace CSharpScript.File
{
	public struct ExportedTypeTable
	{
		public int Count;
		public ExportedType[] ExportedTypes;
		public ExportedTypeTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			ExportedTypes = new ExportedType[count];
			bool islong = Rows[38] >> 14 != 0 || Rows[35] >> 14 != 0 || Rows[39] >> 14 != 0;
			for (int i = 0; i < count; i++)
			{
				ExportedTypes[i] = new ExportedType(new ExportedTypeRow
				{
					Index = i + 1,
					Flags = TildeHeap.Read(bs, ref index, islong: true),
					TypeDefId = TildeHeap.Read(bs, ref index, islong: true),
					TypeName = TildeHeap.Read(bs, ref index, isStringLong),
					TypeNamespace = TildeHeap.Read(bs, ref index, isStringLong),
					Implementation = TildeHeap.Read(bs, ref index, islong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (ExportedTypes != null)
			{
				ExportedType[] exportedTypes = ExportedTypes;
				for (int i = 0; i < exportedTypes.Length; i++)
				{
					exportedTypes[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
