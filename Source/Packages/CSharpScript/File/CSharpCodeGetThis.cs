namespace CSharpScript.File
{
	public sealed class CSharpCodeGetThis : CSharpCode
	{
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + "this";
		}
	}
}
