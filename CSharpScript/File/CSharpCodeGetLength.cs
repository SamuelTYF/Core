namespace CSharpScript.File
{
	public sealed class CSharpCodeGetLength : CSharpCode
	{
		public CSharpCode Value;
		public CSharpCodeGetLength(CSharpCode value)
		{
			Value = value;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + Value.ToString() + ".GetLength()";
		}
	}
}
