using System;
namespace CSharpScript.File
{
	public class MemberRefParent
	{
		public MemberRefParentFlag Flag;
		public TypeDef TypeDef;
		public TypeRef TypeRef;
		public ModuleRef ModuleRef;
		public MethodDef MethodDef;
		public TypeSpec TypeSpec;
		public MemberRefParent(int value, MetadataRoot metadata)
		{
			Flag = (MemberRefParentFlag)(value & 7);
			switch (Flag)
			{
			case MemberRefParentFlag.TypeDef:
				TypeDef = metadata.TildeHeap.TypeDefTable.TypeDefs[(value >> 3) - 1];
				break;
			case MemberRefParentFlag.TypeRef:
				TypeRef = metadata.TildeHeap.TypeRefTable.TypeRefs[(value >> 3) - 1];
				break;
			case MemberRefParentFlag.ModuleRef:
				ModuleRef = metadata.TildeHeap.ModuleRefTable.ModuleRefs[(value >> 3) - 1];
				break;
			case MemberRefParentFlag.MethodDef:
				MethodDef = metadata.TildeHeap.MethodDefTable.MethodDefs[(value >> 3) - 1];
				break;
			case MemberRefParentFlag.TypeSpec:
				TypeSpec = metadata.TildeHeap.TypeSpecTable.TypeSpecs[(value >> 3) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public MemberRefParent(TypeDef type)
		{
			Flag = MemberRefParentFlag.TypeDef;
			TypeDef = type;
		}
		public override string ToString()
		{
			return Flag switch
			{
				MemberRefParentFlag.TypeDef => TypeDef.ToString(), 
				MemberRefParentFlag.TypeRef => TypeRef.ToString(), 
				MemberRefParentFlag.ModuleRef => ModuleRef.ToString(), 
				MemberRefParentFlag.MethodDef => MethodDef.ToString(), 
				MemberRefParentFlag.TypeSpec => TypeSpec.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
