namespace CSharpScript.File
{
	public sealed class CSharpCodeDupField : CSharpCode
	{
		public FieldOrRef Field;
		public CSharpCodeDupField(FieldOrRef _Field)
		{
			Field = _Field;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + Field.Name;
		}
	}
}
