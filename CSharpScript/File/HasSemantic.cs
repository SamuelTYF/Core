using System;
namespace CSharpScript.File
{
	public class HasSemantic
	{
		public HasSemanticFlags Flag;
		public Event Event;
		public Property Property;
		public HasSemantic(int value, MetadataRoot metadata)
		{
			Flag = (HasSemanticFlags)(value & 1);
			switch (Flag)
			{
			case HasSemanticFlags.Event:
				Event = metadata.TildeHeap.EventTable.Events[(value >> 1) - 1];
				break;
			case HasSemanticFlags.Property:
				Property = metadata.TildeHeap.PropertyTable.Properties[(value >> 1) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			return Flag switch
			{
				HasSemanticFlags.Event => Event.ToString(), 
				HasSemanticFlags.Property => Property.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
