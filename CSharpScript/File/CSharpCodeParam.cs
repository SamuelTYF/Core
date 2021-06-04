namespace CSharpScript.File
{
	public sealed class CSharpCodeParam : CSharpCode
	{
		public Param Param;
		public bool IsRef;
		public CSharpCodeParam(Param param, bool isref = false)
		{
			Param = param;
			IsRef = isref;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + Param.Name.ToString();
		}
	}
}
