using System;
namespace CSharpScript.File
{
	public class TypeOrMethodDef
	{
		public TypeOrMethodDefFlag Flag;
		public TypeDef TypeDef;
		public MethodDef MethodDef;
		public TypeOrMethodDef(int value, MetadataRoot metadata)
		{
			Flag = (TypeOrMethodDefFlag)(value & 1);
			switch (Flag)
			{
			case TypeOrMethodDefFlag.TypeDef:
				TypeDef = metadata.TildeHeap.TypeDefTable.TypeDefs[(value >> 1) - 1];
				break;
			case TypeOrMethodDefFlag.MethodDef:
				MethodDef = metadata.TildeHeap.MethodDefTable.MethodDefs[(value >> 1) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			return Flag switch
			{
				TypeOrMethodDefFlag.TypeDef => TypeDef.ToString(), 
				TypeOrMethodDefFlag.MethodDef => MethodDef.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
