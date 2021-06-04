namespace CSharpScript.File
{
	public sealed class CSharpCodeConvert : CSharpCode
	{
		public CSharpCode Value;
		public ElementType Type;
		public CSharpCodeConvert(CSharpCode value, ElementType type)
		{
			Value = value;
			Type = type;
		}
		public override string Print(int tabs = 0)
		{
			return Value.ToString();
		}
	}
}
