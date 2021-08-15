using System;
namespace CSharpScript.File
{
	[Flags]
	public enum ExceptionHandlingFlags : byte
	{
		COR_ILEXCEPTION_CLAUSE_EXCEPTION = 0x0,
		COR_ILEXCEPTION_CLAUSE_FILTER = 0x1,
		COR_ILEXCEPTION_CLAUSE_FINALLY = 0x2,
		COR_ILEXCEPTION_CLAUSE_FAULT = 0x4
	}
}
