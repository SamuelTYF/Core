namespace CSharpScript.File
{
	public sealed class CSharpCodeDoubleOperator : CSharpCode
	{
		public static readonly string[] Signs = new string[16]
		{
			"+", "-", "*", "/", "%", "&", "|", "^", "<<", ">>",
			"==", "!=", ">=", ">", "<=", "<"
		};
		public static readonly string Format = "{0}{1}{2}";
		public CSharpCode Left;
		public CSharpCode Right;
		public DoubleOperator Operator;
		public bool Checked;
		public CSharpCodeDoubleOperator(CSharpCode r, CSharpCode l, DoubleOperator op, bool ch = true)
		{
			Left = l;
			Right = r;
			Operator = op;
			Checked = ch;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + string.Format(Checked ? Format : ("unchecked(" + Format + ")"), Left, Signs[(int)Operator], Right);
		}
	}
}
