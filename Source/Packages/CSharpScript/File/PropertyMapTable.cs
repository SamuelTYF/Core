namespace CSharpScript.File
{
	public struct PropertyMapTable
	{
		public int Count;
		public PropertyMap[] PropertyMaps;
		public PropertyMapTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			PropertyMaps = new PropertyMap[count];
			bool islong = Rows[2] >> 16 != 0;
			bool islong2 = Rows[23] >> 16 != 0;
			for (int i = 0; i < count; i++)
			{
				PropertyMaps[i] = new PropertyMap(new PropertyMapRow
				{
					Index = i + 1,
					Parent = TildeHeap.Read(bs, ref index, islong),
					PropertyList = TildeHeap.Read(bs, ref index, islong2)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (PropertyMaps != null)
			{
				for (int i = 0; i < Count - 1; i++)
				{
					PropertyMaps[i].ResolveSignature(metadata, PropertyMaps[i + 1].Row.PropertyList);
				}
				PropertyMaps[Count - 1].ResolveSignature(metadata, metadata.TildeHeap.PropertyTable.Count + 1);
			}
		}
	}
}
