using System;
namespace CSharpScript.File
{
	[Flags]
	public enum MethodHeaderTypeValues
	{
		CorILMethod_TinyFormat = 0x2,
		CorILMethod_FatFormat = 0x3,
		CorILMethod_MoreSects = 0x8,
		CorILMethod_InitLocals = 0x10
	}
}
