using System;
namespace CSharpScript.File
{
	public sealed class CSharpCodeInitObj : CSharpCode
	{
		public CSharpCode Value;
		public CSharpCodeInitObj(CSharpCode code)
		{
			Value = code;
			if (code is CSharpCodeLocalVar)
			{
				(code as CSharpCodeLocalVar).IsRef = false;
				return;
			}
			throw new Exception();
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + $"{Value}=null";
		}
	}
}
