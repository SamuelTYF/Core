namespace CSharpScript.File
{
	public sealed class CSharpCodeLocalVar : CSharpCode
	{
		public LocalVar Var;
		public bool IsRef;
		public CSharpCodeLocalVar(LocalVar var, bool isref = false)
		{
			Var = var;
			IsRef = isref;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + (IsRef ? "ref " : "") + Var.Name;
		}
	}
}
