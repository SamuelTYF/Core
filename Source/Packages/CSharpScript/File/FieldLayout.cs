namespace CSharpScript.File
{
	public class FieldLayout
	{
		public int Offset;
		public Field Field;
		public FieldLayoutRow Row;
		public int Token;
		public FieldLayout(FieldLayoutRow row)
		{
			FieldLayoutRow fieldLayoutRow = (Row = row);
			Token = fieldLayoutRow.Index | 0x10000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Offset = Row.Offset;
			Field = metadata.TildeHeap.FieldTable.Fields[Row.Field - 1];
		}
		public override string ToString()
		{
			return $"[System.Runtime.InteropServices.FieldOffsetAttribute(0x{Offset:X})]";
		}
	}
}
