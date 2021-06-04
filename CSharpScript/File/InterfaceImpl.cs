using Collection;

namespace CSharpScript.File
{
	public class InterfaceImpl
	{
		public TypeDef Class;
		public TypeDefOrRef Interface;
		public InterfaceImplRow Row;
		public int Token;
		public List<CustomAttribute> CustomAttributes = new();
		public InterfaceImpl(InterfaceImplRow row)
		{
			InterfaceImplRow interfaceImplRow = (Row = row);
			Token = interfaceImplRow.Index | 0x9000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Class = metadata.TildeHeap.TypeDefTable.TypeDefs[Row.Class - 1];
			Interface = new TypeDefOrRef(Row.Interface, metadata);
			Class.Interfaces.Add(Interface);
		}
	}
}
