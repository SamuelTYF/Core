namespace CSharpScript.File
{
	public sealed class CSharpCodeReturn : CSharpCode
	{
		public CSharpCode Value;
		public CSharpCodeReturn(CSharpCode value)
		{
			Value = value;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + "return" + ((Value == null) ? "" : $" {Value}");
		}
	}
}
