using System;
namespace CSharpScript.File
{
	public class FieldMarshal
	{
		public HasFieldMarshal Parent;
		public MarshalSpec NativeType;
		public FieldMarshalRow Row;
		public int Token;
		public FieldMarshal(FieldMarshalRow row)
		{
			FieldMarshalRow fieldMarshalRow = (Row = row);
			Token = fieldMarshalRow.Index | 0xD000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Parent = new HasFieldMarshal(Row.Parent, metadata);
			int index = 0;
			NativeType = MarshalSpec.AnalyzeMarshalSpec(metadata.BlobHeap.Values[Row.NativeType], ref index);
			switch (Parent.Flag)
			{
			case HasFieldMarshalFlag.Param:
				Parent.Param.NativeType = NativeType;
				break;
			case HasFieldMarshalFlag.Field:
				Parent.Field.NativeType = NativeType;
				break;
			default:
				throw new Exception();
			}
		}
	}
}
