namespace CSharpScript.File
{
	public struct MethodSemanticsTable
	{
		public int Count;
		public MethodSemantic[] MethodSemantics;
		public MethodSemanticsTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			MethodSemantics = new MethodSemantic[count];
			bool islong = Rows[6] >> 16 != 0;
			bool islong2 = Rows[20] >> 15 != 0 || Rows[23] >> 15 != 0;
			for (int i = 0; i < count; i++)
			{
				MethodSemantics[i] = new MethodSemantic(new MethodSemanticsRow
				{
					Index = i + 1,
					Semantics = TildeHeap.Read(bs, ref index, islong: false),
					Method = TildeHeap.Read(bs, ref index, islong),
					Association = TildeHeap.Read(bs, ref index, islong2)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (MethodSemantics != null)
			{
				MethodSemantic[] methodSemantics = MethodSemantics;
				for (int i = 0; i < methodSemantics.Length; i++)
				{
					methodSemantics[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
