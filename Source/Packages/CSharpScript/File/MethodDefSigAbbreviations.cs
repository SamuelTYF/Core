using System;
namespace CSharpScript.File
{
	[Flags]
	public enum MethodDefSigAbbreviations : ushort
	{
		DEFAULT = 0x0,
		VARARG = 0x5,
		GENERIC = 0x10,
		HASTHIS = 0x20,
		EXPLICITTHIS = 0x40
	}
}
