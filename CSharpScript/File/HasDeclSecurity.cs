using System;
namespace CSharpScript.File
{
	public class HasDeclSecurity
	{
		public HasDeclSecurityFlag Flag;
		public TypeDef TypeDef;
		public MethodDef MethodDef;
		public Assembly Assembly;
		public HasDeclSecurity(int value, MetadataRoot metadata)
		{
			Flag = (HasDeclSecurityFlag)(value & 3);
			switch (Flag)
			{
			case HasDeclSecurityFlag.TypeDef:
				TypeDef = metadata.TildeHeap.TypeDefTable.TypeDefs[(value >> 2) - 1];
				break;
			case HasDeclSecurityFlag.MethodDef:
				MethodDef = metadata.TildeHeap.MethodDefTable.MethodDefs[(value >> 2) - 1];
				break;
			case HasDeclSecurityFlag.Assembly:
				Assembly = metadata.TildeHeap.AssemblyTable.Assemblys[(value >> 2) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			return Flag switch
			{
				HasDeclSecurityFlag.TypeDef => TypeDef.ToString(), 
				HasDeclSecurityFlag.MethodDef => MethodDef.ToString(), 
				HasDeclSecurityFlag.Assembly => Assembly.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
