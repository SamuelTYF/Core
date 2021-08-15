namespace CSharpScript.File
{
	public sealed class CSharpCodeTry : CSharpCode
	{
		public CSharpCodeBlock Codes;
		public CSharpCodeCatch[] Catches;
		public CSharpCodeFinally Finally;
		public override string Print(int tabs = 0)
		{
			return "";
		}
	}
}
