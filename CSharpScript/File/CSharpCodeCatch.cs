namespace CSharpScript.File
{
	public sealed class CSharpCodeCatch : CSharpCode
	{
		public TypeDefOrRef Exception;
		public CSharpCodeBlock Codes;
		public override string Print(int tabs = 0)
		{
			return "";
		}
	}
}
