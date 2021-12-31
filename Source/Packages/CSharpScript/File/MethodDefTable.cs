namespace CSharpScript.File
{
	public struct MethodDefTable
	{
		public MethodDef[] MethodDefs;
		public int Count;
		public MethodDefTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			MethodDefs = new MethodDef[count];
			bool islong = Rows[8] >> 16 != 0;
			for (int i = 0; i < count; i++)
			{
				MethodDefs[i] = new MethodDef(new MethodDefRow
				{
					Index = i + 1,
					RVA = TildeHeap.Read(bs, ref index, islong: true),
					ImplFlags = TildeHeap.Read(bs, ref index, islong: false),
					Flags = TildeHeap.Read(bs, ref index, islong: false),
					Name = TildeHeap.Read(bs, ref index, isStringLong),
					Signature = TildeHeap.Read(bs, ref index, isBlobLong),
					ParamList = TildeHeap.Read(bs, ref index, islong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (MethodDefs != null)
			{
				for (int i = 0; i < MethodDefs.Length - 1; i++)
					MethodDefs[i].ResolveSignature(metadata, MethodDefs[i + 1].Row.ParamList);
				MethodDefs[^1].ResolveSignature(metadata, metadata.TildeHeap.ParamTable.Count + 1);
			}
		}
	}
}
