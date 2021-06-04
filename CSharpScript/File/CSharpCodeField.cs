namespace CSharpScript.File
{
	public sealed class CSharpCodeField : CSharpCode
	{
		public CSharpCode Object;
		public FieldOrRef Field;
		public bool IsRef;
		public CSharpCodeField(CSharpCode _Object, FieldOrRef _Field, bool _Isref)
		{
			Object = _Object;
			Field = _Field;
			IsRef = _Isref;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + ((Object == null) ? Field.Parent.ToString() : Object.Print()) + "." + Field.Name;
		}
	}
}
