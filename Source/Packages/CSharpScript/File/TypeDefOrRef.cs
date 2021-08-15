using System;
namespace CSharpScript.File
{
	public class TypeDefOrRef
	{
		public TypeDefOrRefFlag Flag;
		public TypeDef TypeDef;
		public TypeRef TypeRef;
		public TypeSpec TypeSpec;
		public TypeDefOrRef(int value, MetadataRoot metadata)
		{
			Flag = (TypeDefOrRefFlag)(value & 3);
			switch (Flag)
			{
			case TypeDefOrRefFlag.TypeDef:
				TypeDef = metadata.TildeHeap.TypeDefTable.TypeDefs[(value >> 2) - 1];
				break;
			case TypeDefOrRefFlag.TypeRef:
				TypeRef = metadata.TildeHeap.TypeRefTable.TypeRefs[(value >> 2) - 1];
				break;
			case TypeDefOrRefFlag.TypeSpec:
				TypeSpec = metadata.TildeHeap.TypeSpecTable.TypeSpecs[(value >> 2) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public TypeDefOrRef(TypeDef typedef, TypeRef typeref, TypeSpec typespec)
		{
			if ((TypeDef = typedef) != null)
			{
				Flag = TypeDefOrRefFlag.TypeDef;
				return;
			}
			if ((TypeRef = typeref) != null)
			{
				Flag = TypeDefOrRefFlag.TypeRef;
				return;
			}
			if ((TypeSpec = typespec) != null)
			{
				Flag = TypeDefOrRefFlag.TypeSpec;
				return;
			}
			throw new Exception();
		}
		public TypeDefOrRef(TypeDef typedef)
		{
			TypeDef = typedef;
			Flag = TypeDefOrRefFlag.TypeDef;
		}
		public TypeDefOrRef(TypeRef typeref)
		{
			TypeRef = typeref;
			Flag = TypeDefOrRefFlag.TypeRef;
		}
		public TypeDefOrRef(TypeSpec typespec)
		{
			TypeSpec = typespec;
			Flag = TypeDefOrRefFlag.TypeSpec;
		}
		public override string ToString()
		{
			return Flag switch
			{
				TypeDefOrRefFlag.TypeDef => TypeDef.ToString(), 
				TypeDefOrRefFlag.TypeRef => TypeRef.ToString(), 
				TypeDefOrRefFlag.TypeSpec => TypeSpec.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
