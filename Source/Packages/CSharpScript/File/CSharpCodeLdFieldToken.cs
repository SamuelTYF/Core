using System;
namespace CSharpScript.File
{
	public sealed class CSharpCodeLdFieldToken : CSharpCode
	{
		public FieldRVA RVA;
		public CSharpCodeLdFieldToken(FieldRVA rva)
		{
			RVA = rva;
		}
		public override string Print(int tabs = 0)
		{
			throw new Exception();
		}
	}
}
