using System;
namespace CSharpScript.File
{
	[Flags]
	public enum Abbreviations : ushort
	{
		DEFAULT = 0x0,
		C = 0x1,
		STDCALL = 0x2,
		THISCALL = 0x3,
		FASTCALL = 0x4,
		VARARG = 0x5,
		GENERIC = 0x10,
		HASTHIS = 0x20,
		EXPLICITTHIS = 0x40,
		SENTINEL = 0x41,
		FIELD = 0x6,
		PROPERTY = 0x8
	}
}
