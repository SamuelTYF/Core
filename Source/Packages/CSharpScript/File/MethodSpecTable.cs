namespace CSharpScript.File
{
	public struct MethodSpecTable
	{
		public int Count;
		public MethodSpec[] MethodSpecs;
		public MethodSpecTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			MethodSpecs = new MethodSpec[count];
			bool islong = Rows[6] >> 15 != 0 || Rows[10] >> 15 != 0;
			for (int i = 0; i < count; i++)
			{
				MethodSpecs[i] = new MethodSpec(new MethodSpecRow
				{
					Index = i + 1,
					Method = TildeHeap.Read(bs, ref index, islong),
					Instantiation = TildeHeap.Read(bs, ref index, isBlobLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (MethodSpecs != null)
			{
				MethodSpec[] methodSpecs = MethodSpecs;
				for (int i = 0; i < methodSpecs.Length; i++)
				{
					methodSpecs[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
