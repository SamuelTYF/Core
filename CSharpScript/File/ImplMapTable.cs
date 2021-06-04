namespace CSharpScript.File
{
	public struct ImplMapTable
	{
		public int Count;
		public ImplMap[] ImplMaps;
		public ImplMapTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			ImplMaps = new ImplMap[count];
			bool islong = Rows[4] >> 15 != 0 || Rows[6] >> 15 != 0;
			for (int i = 0; i < count; i++)
			{
				ImplMaps[i] = new ImplMap(new ImplMapRow
				{
					Index = i + 1,
					MappingFlags = TildeHeap.Read(bs, ref index, islong: false),
					MemberForwarded = TildeHeap.Read(bs, ref index, islong),
					ImportName = TildeHeap.Read(bs, ref index, isStringLong),
					ImportScope = TildeHeap.Read(bs, ref index, islong: false)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (ImplMaps != null)
			{
				ImplMap[] implMaps = ImplMaps;
				for (int i = 0; i < implMaps.Length; i++)
				{
					implMaps[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
