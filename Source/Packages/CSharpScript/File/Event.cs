using Collection;
namespace CSharpScript.File
{
	public class Event
	{
		public EventAttributes EventFlags;
		public string Name;
		public TypeDefOrRef EventType;
		public EventRow Row;
		public int Token;
		public TypeDef Parent;
		public int Index;
		public List<CustomAttribute> CustomAttributes = new();
		public Event(EventRow row)
		{
			EventRow eventRow = (Row = row);
			Token = eventRow.Index | 0x14000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			EventFlags = (EventAttributes)Row.EventFlags;
			Name = metadata.StringsHeap[Row.Name];
			EventType = new TypeDefOrRef(Row.EventType, metadata);
		}
        public override string ToString() => $".event {EventType} {Parent}.{Name}";
        public string GetInformation()
		{
			if (CustomAttributes.Length == 0)
				return ToString();
			return string.Join("\n", CustomAttributes) + "\n" + ToString();
		}
	}
}
