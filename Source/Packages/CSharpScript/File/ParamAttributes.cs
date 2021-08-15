using System;
namespace CSharpScript.File
{
	[Flags]
	public enum ParamAttributes
	{
		In = 0x1,
		Out = 0x2,
		Optional = 0x10,
		HasDefault = 0x1000,
		HasFieldMarshal = 0x2000
	}
}
