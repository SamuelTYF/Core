using System;
namespace CSharpScript.File
{
	public class HasFieldMarshal
	{
		public HasFieldMarshalFlag Flag;
		public Field Field;
		public Param Param;
		public HasFieldMarshal(int value, MetadataRoot metadata)
		{
			Flag = (HasFieldMarshalFlag)(value & 1);
			switch (Flag)
			{
			case HasFieldMarshalFlag.Field:
				Field = metadata.TildeHeap.FieldTable.Fields[(value >> 1) - 1];
				break;
			case HasFieldMarshalFlag.Param:
				Param = metadata.TildeHeap.ParamTable.Params[(value >> 1) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			return Flag switch
			{
				HasFieldMarshalFlag.Field => Field.ToString(), 
				HasFieldMarshalFlag.Param => Param.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
