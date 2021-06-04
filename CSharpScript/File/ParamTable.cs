namespace CSharpScript.File
{
	public struct ParamTable
	{
		public int Count;
		public Param[] Params;
		public ParamTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Count = count;
			Params = new Param[count];
			for (int i = 0; i < count; i++)
			{
				Params[i] = new Param(new ParamRow
				{
					Index = i + 1,
					Flags = TildeHeap.Read(bs, ref index, islong: false),
					Sequence = TildeHeap.Read(bs, ref index, islong: false),
					Name = TildeHeap.Read(bs, ref index, isStringLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (Params != null)
			{
				Param[] @params = Params;
				for (int i = 0; i < @params.Length; i++)
				{
					@params[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
