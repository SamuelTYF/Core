namespace CSharpScript.File
{
	public sealed class CSharpCodeIf : CSharpCode
	{
		public CSharpCode Condition;
		public CSharpCode True;
		public CSharpCode False;
		public CSharpCodeIf(CSharpCode condition, CSharpCode @true, CSharpCode @false)
		{
			Condition = condition;
			True = @true;
			False = @false;
		}
		public override string Print(int tabs = 0)
		{
			if (False == null)
				return $"{GetTabs(tabs)}if({Condition})\n{GetTabs(tabs)}{{\n{GetTabs(tabs)}{True.Print(tabs + 1)}\n{GetTabs(tabs)}}}";
			if (True == null)
				return $"{GetTabs(tabs)}if(!({Condition}))\n{GetTabs(tabs)}{{\n{GetTabs(tabs)}{False.Print(tabs + 1)}\n{GetTabs(tabs)}}}";
			if (!(True is CSharpCodeBlock) && !(False is CSharpCodeBlock))
				return $"{GetTabs(tabs)}({Condition})?{True}:{False}";
			return $"{GetTabs(tabs)}if({Condition})\n{GetTabs(tabs)}{{\n{GetTabs(tabs)}{True.Print(tabs + 1)}\n{GetTabs(tabs)}}}\n{GetTabs(tabs)}else\n{GetTabs(tabs)}{{\n{GetTabs(tabs)}{False.Print(tabs + 1)}\n{GetTabs(tabs)}}}";
		}
	}
}
