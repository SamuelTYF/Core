namespace CSharpScript.File
{
	public struct GenericParamTable
	{
		public int Count;
		public GenericParam[] GenericParams;
		public GenericParamTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			GenericParams = new GenericParam[count];
			bool islong = Rows[2] >> 15 != 0 || Rows[6] >> 15 != 0;
			for (int i = 0; i < count; i++)
			{
				GenericParams[i] = new GenericParam(new GenericParamRow
				{
					Index = i + 1,
					Number = TildeHeap.Read(bs, ref index, islong: false),
					Flags = TildeHeap.Read(bs, ref index, islong: false),
					Owner = TildeHeap.Read(bs, ref index, islong),
					Name = TildeHeap.Read(bs, ref index, isStringLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (GenericParams != null)
			{
				GenericParam[] genericParams = GenericParams;
				for (int i = 0; i < genericParams.Length; i++)
				{
					genericParams[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
