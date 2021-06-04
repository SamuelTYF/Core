using System;
using Collection;
namespace CSharpScript.File
{
	public class Module
	{
		public int Generation;
		public string Name;
		public Guid Mvid;
		public Guid EncId;
		public Guid EncBaseId;
		public ModuleRow Row;
		public int Token;
		public List<CustomAttribute> CustomAttributes = new List<CustomAttribute>();
		public Module(ModuleRow row)
		{
			ModuleRow moduleRow = (Row = row);
			Token = moduleRow.Index;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Generation = Row.Generation;
			Name = metadata.StringsHeap[Row.Name];
			Mvid = metadata.GUIDHeap[Row.Mvid];
			EncId = metadata.GUIDHeap[Row.EncId];
			EncBaseId = metadata.GUIDHeap[Row.EncBaseId];
		}
		public override string ToString()
		{
			return Name;
		}
		public string GetInformation()
		{
			return (CustomAttributes.Length == 0) ? (".module " + Name) : (string.Join("\n", CustomAttributes) + "\n.module " + Name);
		}
	}
}
