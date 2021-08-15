namespace CSharpScript.File
{
	public struct ConstantTable
	{
		public int Count;
		public Constant[] Constants;
		public ConstantTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			Constants = new Constant[count];
			bool islong = Rows[4] >> 14 != 0 || Rows[8] >> 14 != 0 || Rows[23] >> 14 != 0;
			for (int i = 0; i < count; i++)
				Constants[i] = new Constant(new ConstantRow
				{
					Index = i + 1,
					Type = TildeHeap.Read(bs, ref index, islong: false),
					Parent = TildeHeap.Read(bs, ref index, islong),
					Value = TildeHeap.Read(bs, ref index, isBlobLong)
				});
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (Constants != null)
				for (int i = 0; i < Constants.Length; i++)
					Constants[i].ResolveSignature(metadata);
		}
	}
}
