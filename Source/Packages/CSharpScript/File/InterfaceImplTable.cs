namespace CSharpScript.File
{
	public struct InterfaceImplTable
	{
		public int Count;
		public InterfaceImpl[] InterfaceImpls;
		public InterfaceImplTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			InterfaceImpls = new InterfaceImpl[count];
			bool islong = Rows[2] >> 16 != 0;
			bool islong2 = Rows[1] >> 14 != 0 || Rows[2] >> 14 != 0 || Rows[27] >> 14 != 0;
			for (int i = 0; i < count; i++)
			{
				InterfaceImpls[i] = new InterfaceImpl(new InterfaceImplRow
				{
					Index = i + 1,
					Class = TildeHeap.Read(bs, ref index, islong),
					Interface = TildeHeap.Read(bs, ref index, islong2)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (InterfaceImpls != null)
			{
				InterfaceImpl[] interfaceImpls = InterfaceImpls;
				for (int i = 0; i < interfaceImpls.Length; i++)
				{
					interfaceImpls[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
