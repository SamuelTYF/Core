namespace CSharpScript.File
{
	public struct TypeRefTable
	{
		public TypeRef[] TypeRefs;
		public TypeRefTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			TypeRefs = new TypeRef[count];
			bool islong = Rows[0] >> 14 != 0 || Rows[26] >> 14 != 0 || Rows[35] >> 14 != 0 || Rows[1] >> 14 != 0;
			for (int i = 0; i < count; i++)
			{
				TypeRefs[i] = new TypeRef(new TypeRefRow
				{
					Index = i + 1,
					ResolutionScope = TildeHeap.Read(bs, ref index, islong),
					TypeName = TildeHeap.Read(bs, ref index, isStringLong),
					TypeNamespace = TildeHeap.Read(bs, ref index, isStringLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (TypeRefs != null)
			{
				TypeRef[] typeRefs = TypeRefs;
				for (int i = 0; i < typeRefs.Length; i++)
				{
					typeRefs[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
