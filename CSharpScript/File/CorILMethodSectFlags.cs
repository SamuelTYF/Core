using System;
namespace CSharpScript.File
{
	[Flags]
	public enum CorILMethodSectFlags : byte
	{
		CorILMethod_Sect_EHTable = 0x1,
		CorILMethod_Sect_OptILTable = 0x2,
		CorILMethod_Sect_FatFormat = 0x40,
		CorILMethod_Sect_MoreSects = 0x80
	}
}
