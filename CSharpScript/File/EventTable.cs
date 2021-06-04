namespace CSharpScript.File
{
	public struct EventTable
	{
		public int Count;
		public Event[] Events;
		public EventTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong, int[] Rows)
		{
			Count = count;
			Events = new Event[count];
			bool islong = Rows[1] >> 14 != 0 || Rows[2] >> 14 != 0 || Rows[27] >> 14 != 0;
			for (int i = 0; i < count; i++)
			{
				Events[i] = new Event(new EventRow
				{
					EventFlags = TildeHeap.Read(bs, ref index, islong: false),
					Name = TildeHeap.Read(bs, ref index, isStringLong),
					EventType = TildeHeap.Read(bs, ref index, islong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (Events != null)
			{
				Event[] events = Events;
				for (int i = 0; i < events.Length; i++)
				{
					events[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
