namespace CSharpScript.File
{
	public struct AssemblyTable
	{
		public int Count;
		public Assembly[] Assemblys;
		public AssemblyTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Count = count;
			Assemblys = new Assembly[count];
			for (int i = 0; i < count; i++)
				Assemblys[i] = new Assembly(new AssemblyRow
				{
					Index = i + 1,
					HashAlgId = TildeHeap.Read(bs, ref index, islong: true),
					MajorVersion = TildeHeap.Read(bs, ref index, islong: false),
					MinorVersion = TildeHeap.Read(bs, ref index, islong: false),
					BuildNumber = TildeHeap.Read(bs, ref index, islong: false),
					RevisionNumber = TildeHeap.Read(bs, ref index, islong: false),
					Flags = TildeHeap.Read(bs, ref index, islong: true),
					PublicKey = TildeHeap.Read(bs, ref index, isBlobLong),
					Name = TildeHeap.Read(bs, ref index, isStringLong),
					Culture = TildeHeap.Read(bs, ref index, isStringLong)
				});
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (Assemblys != null)
				for (int i = 0; i < Assemblys.Length; i++)
					Assemblys[i].ResolveSignature(metadata);
		}
	}
}
