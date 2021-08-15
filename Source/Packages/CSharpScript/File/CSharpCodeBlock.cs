using System.Collections.Generic;
namespace CSharpScript.File
{
	public sealed class CSharpCodeBlock : CSharpCode
	{
		public CSharpCode[] Codes;
		public CSharpCodeBlock(CSharpCode[] codes)
		{
			Codes = codes;
		}
		public override string Print(int tabs = 0)
		{
			return string.Join("\n", (IEnumerable<CSharpCode>)Codes);
		}
	}
}
