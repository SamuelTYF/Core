namespace CSharpScript.File
{
	public sealed class CSharpCodeNormalConstructor : CSharpCode
	{
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + "//base.ctor()";
		}
	}
}
