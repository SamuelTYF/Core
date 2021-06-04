namespace CSharpScript.File
{
	public sealed class CSharpCodeStElem : CSharpCode
	{
		public CSharpCode Index;
		public CSharpCode Object;
		public CSharpCode Value;
		public CSharpCodeStElem(CSharpCode v, CSharpCode i, CSharpCode o)
		{
			Value = v;
			Object = o;
			Index = i;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + $"{Object}[{Index}]={Value}";
		}
	}
}
