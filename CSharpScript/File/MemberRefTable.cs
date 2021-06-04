namespace CSharpScript.File
{
	public struct MemberRefTable
	{
		public int Count;
		public MemberRef[] MemberRefs;
		public MemberRefTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			MemberRefs = new MemberRef[count];
			bool islong = Rows[1] >> 13 != 0 || Rows[2] >> 13 != 0 || Rows[26] >> 13 != 0 || Rows[6] >> 13 != 0 || Rows[27] >> 13 != 0;
			for (int i = 0; i < count; i++)
			{
				MemberRefs[i] = new MemberRef(new MemberRefRow
				{
					Index = i + 1,
					Class = TildeHeap.Read(bs, ref index, islong),
					Name = TildeHeap.Read(bs, ref index, isStringLong),
					Signature = TildeHeap.Read(bs, ref index, isBlobLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (MemberRefs != null)
			{
				MemberRef[] memberRefs = MemberRefs;
				for (int i = 0; i < memberRefs.Length; i++)
				{
					memberRefs[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
