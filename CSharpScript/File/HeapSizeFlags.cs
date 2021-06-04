using System;
namespace CSharpScript.File
{
	[Flags]
	public enum HeapSizeFlags : byte
	{
		String = 0x1,
		GUID = 0x2,
		Blob = 0x4
	}
}
