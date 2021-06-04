using System;
namespace CSharpScript.File
{
	public class HasConstant
	{
		public HasConstantFlag Flag;
		public Field Field;
		public Param Param;
		public Property Property;
		public HasConstant(int value, MetadataRoot metadata)
		{
			Flag = (HasConstantFlag)(value & 3);
			switch (Flag)
			{
			case HasConstantFlag.Field:
				Field = metadata.TildeHeap.FieldTable.Fields[(value >> 2) - 1];
				break;
			case HasConstantFlag.Param:
				Param = metadata.TildeHeap.ParamTable.Params[(value >> 2) - 1];
				break;
			case HasConstantFlag.Property:
				Property = metadata.TildeHeap.PropertyTable.Properties[(value >> 2) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			return Flag switch
			{
				HasConstantFlag.Field => Field.ToString(), 
				HasConstantFlag.Param => Param.ToString(), 
				HasConstantFlag.Property => Property.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
