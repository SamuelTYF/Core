namespace CSharpScript.File
{
	public struct MethodImplTable
	{
		public int Count;
		public MethodImpl[] MethodImpls;
		public MethodImplTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			MethodImpls = new MethodImpl[count];
			bool islong = Rows[2] >> 16 != 0;
			bool islong2 = Rows[6] >> 15 != 0 || Rows[10] >> 15 != 0;
			for (int i = 0; i < count; i++)
			{
				MethodImpls[i] = new MethodImpl(new MethodImplRow
				{
					Index = i + 1,
					Class = TildeHeap.Read(bs, ref index, islong),
					MethodBody = TildeHeap.Read(bs, ref index, islong2),
					MethodDeclaration = TildeHeap.Read(bs, ref index, islong2)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (MethodImpls != null)
			{
				MethodImpl[] methodImpls = MethodImpls;
				for (int i = 0; i < methodImpls.Length; i++)
				{
					methodImpls[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
