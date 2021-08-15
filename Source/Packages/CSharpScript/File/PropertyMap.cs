using System;
namespace CSharpScript.File
{
	public class PropertyMap
	{
		public TypeDef Parent;
		public Property[] PropertyList;
		public PropertyMapRow Row;
		public int Token;
		public PropertyMap(PropertyMapRow row)
		{
			PropertyMapRow propertyMapRow = (Row = row);
			Token = propertyMapRow.Index | 0x15000000;
		}
		public void ResolveSignature(MetadataRoot metadata, int nextproperty)
		{
			Parent = metadata.TildeHeap.TypeDefTable.TypeDefs[Row.Parent - 1];
			int num = nextproperty - Row.PropertyList;
			PropertyList = new Property[num];
			if (num != 0)
				Array.Copy(metadata.TildeHeap.PropertyTable.Properties, Row.PropertyList - 1, PropertyList, 0, num);
			Parent.Properties = PropertyList;
			for (int i = 0; i < num; i++)
			{
				PropertyList[i].Parent = Parent;
				PropertyList[i].Index = i;
			}
		}
	}
}
