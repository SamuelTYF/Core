using System;
namespace CSharpScript.File
{
	public class MethodImpl
	{
		public TypeDef Class;
		public MethodDefOrRef MethodBody;
		public MethodDefOrRef MethodDeclaration;
		public MethodImplRow Row;
		public int Token;
		public MethodImpl(MethodImplRow row)
		{
			MethodImplRow methodImplRow = (Row = row);
			Token = methodImplRow.Index | 0x19000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Class = metadata.TildeHeap.TypeDefTable.TypeDefs[Row.Class - 1];
			MethodBody = new MethodDefOrRef(Row.MethodBody, metadata);
			if (MethodBody.Flag == MethodDefOrRefFlag.MethodDef)
			{
				if (MethodBody.MethodDef.Base != null)
					throw new Exception();
				MethodBody.MethodDef.Base = this;
				MethodDeclaration = new MethodDefOrRef(Row.MethodDeclaration, metadata);
				return;
			}
			throw new Exception();
		}
	}
}
