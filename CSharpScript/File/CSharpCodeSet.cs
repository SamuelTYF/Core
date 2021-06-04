namespace CSharpScript.File
{
	public sealed class CSharpCodeSet : CSharpCode
	{
		public CSharpCode Destination;
		public CSharpCode Value;
		public CSharpCodeSet(CSharpCode des, CSharpCode value)
		{
			Destination = des;
			Value = value;
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + Destination.ToString() + "=" + Value.ToString();
		}
	}
}
