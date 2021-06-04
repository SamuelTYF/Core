using System;
namespace CSharpScript.File
{
	public class MethodSemantic
	{
		public MethodSemanticsAttributes Semantics;
		public MethodDef Method;
		public HasSemantic Association;
		public MethodSemanticsRow Row;
		public int Token;
		public MethodSemantic(MethodSemanticsRow row)
		{
			MethodSemanticsRow methodSemanticsRow = (Row = row);
			Token = methodSemanticsRow.Index | 0x18000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Semantics = (MethodSemanticsAttributes)Row.Semantics;
			Method = metadata.TildeHeap.MethodDefTable.MethodDefs[Row.Method - 1];
			Association = new HasSemantic(Row.Association, metadata);
			switch (Semantics)
			{
			case MethodSemanticsAttributes.Setter:
				Association.Property.Setter = this;
				break;
			case MethodSemanticsAttributes.Getter:
				Association.Property.Getter = this;
				break;
			case MethodSemanticsAttributes.Other:
				throw new Exception();
			case MethodSemanticsAttributes.Setter | MethodSemanticsAttributes.Getter:
				break;
			}
		}
		public override string ToString()
		{
			return $"{Association}.{Semantics}\n{Method}";
		}
	}
}
