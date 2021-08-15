namespace CSharpScript.File
{
	public struct TypeDefTable
	{
		public int Count;
		public TypeDef[] TypeDefs;
		public TypeDefTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			TypeDefs = new TypeDef[count];
			bool islong = Rows[1] >> 14 != 0 || Rows[2] >> 14 != 0 || Rows[27] >> 14 != 0;
			bool islong2 = Rows[4] >> 16 != 0;
			bool islong3 = Rows[6] >> 16 != 0;
			for (int i = 0; i < count; i++)
			{
				TypeDefs[i] = new TypeDef(new TypeDefRow
				{
					Index = i + 1,
					Flags = TildeHeap.Read(bs, ref index, islong: true),
					TypeName = TildeHeap.Read(bs, ref index, isStringLong),
					TypeNamespace = TildeHeap.Read(bs, ref index, isStringLong),
					Extends = TildeHeap.Read(bs, ref index, islong),
					FieldList = TildeHeap.Read(bs, ref index, islong2),
					MethodList = TildeHeap.Read(bs, ref index, islong3)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (TypeDefs != null)
			{
				for (int i = 0; i < TypeDefs.Length - 1; i++)
				{
					TypeDefs[i].ResolveSignature(metadata, TypeDefs[i + 1].Row.FieldList, TypeDefs[i + 1].Row.MethodList);
				}
				TypeDefs[TypeDefs.Length - 1].ResolveSignature(metadata, metadata.TildeHeap.FieldTable.Count + 1, metadata.TildeHeap.MethodDefTable.Count + 1);
			}
		}
	}
}
