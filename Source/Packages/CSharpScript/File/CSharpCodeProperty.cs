namespace CSharpScript.File
{
	public sealed class CSharpCodeProperty : CSharpCode
	{
		public CSharpCode Object;
		public Property Property;
		public bool IsRef;
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + (IsRef ? "ref " : "") + Object.Print() + "." + Property.ToString();
		}
	}
}
