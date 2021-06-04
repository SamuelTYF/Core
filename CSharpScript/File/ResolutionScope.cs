using System;
namespace CSharpScript.File
{
	public class ResolutionScope
	{
		public ResolutionScopeFlag Flag;
		public Module Module;
		public ModuleRef ModuleRef;
		public AssemblyRef AssemblyRef;
		public TypeRef TypeRef;
		public ResolutionScope(int value, MetadataRoot metadata)
		{
			Flag = (ResolutionScopeFlag)(value & 3);
			switch (Flag)
			{
			case ResolutionScopeFlag.Module:
				Module = metadata.TildeHeap.ModuleTable.Modules[(value >> 2) - 1];
				break;
			case ResolutionScopeFlag.ModuleRef:
				ModuleRef = metadata.TildeHeap.ModuleRefTable.ModuleRefs[(value >> 2) - 1];
				break;
			case ResolutionScopeFlag.AssemblyRef:
				AssemblyRef = metadata.TildeHeap.AssemblyRefTable.AssemblyRefs[(value >> 2) - 1];
				break;
			case ResolutionScopeFlag.TypeRef:
				TypeRef = metadata.TildeHeap.TypeRefTable.TypeRefs[(value >> 2) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			return Flag switch
			{
				ResolutionScopeFlag.Module => Module.ToString(), 
				ResolutionScopeFlag.ModuleRef => ModuleRef.ToString(), 
				ResolutionScopeFlag.AssemblyRef => AssemblyRef.ToString(), 
				ResolutionScopeFlag.TypeRef => TypeRef.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
