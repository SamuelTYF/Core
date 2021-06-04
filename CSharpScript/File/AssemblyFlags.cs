using System;
namespace CSharpScript.File
{
	[Flags]
	public enum AssemblyFlags
	{
		PublicKey = 0x1,
		Retargetable = 0x100,
		DisableJITcompileOptimizer = 0x4000,
		EnableJITcompileTracking = 0x8000
	}
}
