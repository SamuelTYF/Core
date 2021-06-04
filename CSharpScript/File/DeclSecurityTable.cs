namespace CSharpScript.File
{
	public struct DeclSecurityTable
	{
		public int Count;
		public DeclSecurity[] DeclSecuritys;
		public DeclSecurityTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			DeclSecuritys = new DeclSecurity[count];
			bool islong = Rows[2] >> 14 != 0 || Rows[6] >> 14 != 0 || Rows[32] >> 14 != 0;
			for (int i = 0; i < count; i++)
			{
				DeclSecuritys[i] = new DeclSecurity(new DeclSecurityRow
				{
					Index = i + 1,
					Action = TildeHeap.Read(bs, ref index, islong: false),
					Parent = TildeHeap.Read(bs, ref index, islong),
					PermissionSet = TildeHeap.Read(bs, ref index, isBlobLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (DeclSecuritys != null)
			{
				DeclSecurity[] declSecuritys = DeclSecuritys;
				for (int i = 0; i < declSecuritys.Length; i++)
				{
					declSecuritys[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
