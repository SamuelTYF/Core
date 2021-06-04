namespace CSharpScript.File
{
	public sealed class CSharpCodeAs : CSharpCode
	{
		public TypeDefOrRef Type;
		public CSharpCode Value;
		public CSharpCodeAs(CSharpCode value, TypeDefOrRef type)
		{
			Value = value;
			Type = type;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + $"{Value} as {Type}";
		}
	}
}
