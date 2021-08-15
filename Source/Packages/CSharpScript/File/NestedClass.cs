using System;
namespace CSharpScript.File
{
	public class NestedClass
	{
		public TypeDef Class;
		public TypeDef EnclosingClass;
		public NestedClassRow Row;
		public int Token;
		public NestedClass(NestedClassRow row)
		{
			NestedClassRow nestedClassRow = (Row = row);
			Token = nestedClassRow.Index | 0x29000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Class = metadata.TildeHeap.TypeDefTable.TypeDefs[Row.NestedClass - 1];
			EnclosingClass = metadata.TildeHeap.TypeDefTable.TypeDefs[Row.EnclosingClass - 1];
			Class.EnclosingClass = EnclosingClass;
			Class.FullName = $"{EnclosingClass}+{Class.TypeName}";
			Class.Hash = Class.FullName.GetHashCode();
			if (metadata.QueryTree.ContainsKey(Class.Hash))
				throw new Exception();
			metadata.QueryTree[Class.Hash] = Class.Token;
		}
	}
}
