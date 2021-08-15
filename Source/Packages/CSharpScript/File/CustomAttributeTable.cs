namespace CSharpScript.File
{
	public struct CustomAttributeTable
	{
		public int Count;
		public CustomAttribute[] CustomAttributes;
		public CustomAttributeTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			CustomAttributes = new CustomAttribute[count];
			bool islong = Rows[6] >> 11 != 0 || Rows[4] >> 11 != 0 || Rows[1] >> 11 != 0 || Rows[2] >> 11 != 0 || Rows[8] >> 11 != 0 || Rows[9] >> 11 != 0 || Rows[10] >> 11 != 0 || Rows[0] >> 11 != 0 || Rows[23] >> 11 != 0 || Rows[20] >> 11 != 0 || Rows[17] >> 11 != 0 || Rows[26] >> 11 != 0 || Rows[27] >> 11 != 0 || Rows[32] >> 11 != 0 || Rows[35] >> 11 != 0 || Rows[38] >> 11 != 0 || Rows[39] >> 11 != 0 || Rows[40] >> 11 != 0 || Rows[42] >> 11 != 0 || Rows[44] >> 11 != 0 || Rows[43] >> 11 != 0;
			bool islong2 = Rows[6] >> 13 != 0 || Rows[10] >> 13 != 0;
			for (int i = 0; i < count; i++)
			{
				CustomAttributes[i] = new CustomAttribute(new CustomAttributeRow
				{
					Index = i + 1,
					Parent = TildeHeap.Read(bs, ref index, islong),
					Type = TildeHeap.Read(bs, ref index, islong2),
					Value = TildeHeap.Read(bs, ref index, isBlobLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (CustomAttributes != null)
			{
				CustomAttribute[] customAttributes = CustomAttributes;
				for (int i = 0; i < customAttributes.Length; i++)
				{
					customAttributes[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
