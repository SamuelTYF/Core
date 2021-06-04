using System;
namespace CSharpScript.File
{
	public class EventMap
	{
		public TypeDef Parent;
		public Event[] EventList;
		public EventMapRow Row;
		public int Token;
		public EventMap(EventMapRow row)
		{
			EventMapRow eventMapRow = (Row = row);
			Token = eventMapRow.Index | 0x12000000;
		}
		public void ResolveSignature(MetadataRoot metadata, int nextevent)
		{
			Parent = metadata.TildeHeap.TypeDefTable.TypeDefs[Row.Parent - 1];
			int num = nextevent - Row.EventList;
			EventList = new Event[num];
			if (num != 0)
				Array.Copy(metadata.TildeHeap.EventTable.Events, Row.EventList - 1, EventList, 0, num);
			Parent.Events = EventList;
			for (int i = 0; i < num; i++)
			{
				EventList[i].Parent = Parent;
				EventList[i].Index = i;
			}
		}
	}
}
