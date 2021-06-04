using System;
namespace CSharpScript.File
{
	[Flags]
	public enum MethodImplAttributes
	{
		IL = 0x0,
		Native = 0x1,
		OPTIL = 0x2,
		Runtime = 0x3,
		Unmanaged = 0x4,
		Managed = 0x0,
		ForwardRef = 0x10,
		PreserveSig = 0x80,
		InternalCall = 0x1000,
		Synchronized = 0x20,
		NoInlining = 0x8,
		MaxMethodImplVal = 0xFFFF,
		NoOptimization = 0x40
	}
}
