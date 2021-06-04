namespace CSharpScript.File
{
	public struct PropertyTable
	{
		public int Count;
		public Property[] Properties;
		public PropertyTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Count = count;
			Properties = new Property[count];
			for (int i = 0; i < count; i++)
			{
				Properties[i] = new Property(new PropertyRow
				{
					Index = i + 1,
					Flags = TildeHeap.Read(bs, ref index, islong: false),
					Name = TildeHeap.Read(bs, ref index, isStringLong),
					Type = TildeHeap.Read(bs, ref index, isBlobLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (Properties != null)
			{
				Property[] properties = Properties;
				for (int i = 0; i < properties.Length; i++)
				{
					properties[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
