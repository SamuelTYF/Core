namespace CSharpScript.File
{
	public struct EventMapTable
	{
		public int Count;
		public EventMap[] EventMaps;
		public EventMapTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			EventMaps = new EventMap[count];
			bool islong = Rows[2] >> 16 != 0;
			bool islong2 = Rows[20] >> 16 != 0;
			for (int i = 0; i < count; i++)
			{
				EventMaps[i] = new EventMap(new EventMapRow
				{
					Index = i + 1,
					Parent = TildeHeap.Read(bs, ref index, islong),
					EventList = TildeHeap.Read(bs, ref index, islong2)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (EventMaps != null)
			{
				for (int i = 0; i < Count - 1; i++)
				{
					EventMaps[i].ResolveSignature(metadata, EventMaps[i + 1].Row.EventList);
				}
				EventMaps[Count - 1].ResolveSignature(metadata, metadata.TildeHeap.EventTable.Count + 1);
			}
		}
	}
}
