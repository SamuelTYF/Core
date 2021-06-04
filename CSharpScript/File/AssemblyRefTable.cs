namespace CSharpScript.File
{
	public struct AssemblyRefTable
	{
		public int Count;
		public AssemblyRef[] AssemblyRefs;
		public AssemblyRefTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Count = count;
			AssemblyRefs = new AssemblyRef[count];
			for (int i = 0; i < count; i++)
				AssemblyRefs[i] = new AssemblyRef(new AssemblyRefRow
				{
					Index = i + 1,
					MajorVersion = TildeHeap.Read(bs, ref index, islong: false),
					MinorVersion = TildeHeap.Read(bs, ref index, islong: false),
					BuildNumber = TildeHeap.Read(bs, ref index, islong: false),
					RevisionNumber = TildeHeap.Read(bs, ref index, islong: false),
					Flags = TildeHeap.Read(bs, ref index, islong: true),
					PublicKeyToken = TildeHeap.Read(bs, ref index, isBlobLong),
					Name = TildeHeap.Read(bs, ref index, isStringLong),
					Culture = TildeHeap.Read(bs, ref index, isStringLong),
					HashValue = TildeHeap.Read(bs, ref index, isBlobLong)
				});
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (AssemblyRefs != null)
				for (int i = 0; i < AssemblyRefs.Length; i++)
					AssemblyRefs[i].ResolveSignature(metadata);
		}
	}
}
