using System;
using Collection;
namespace CSharpScript.File
{
	public sealed class CSharpCodeNewObject : CSharpCode
	{
		public TypeDefOrRef Type;
		public MethodDefOrRef Method;
		public CSharpCode[] Params;
		public List<CSharpCode> Dups;
		public CSharpCodeNewObject(TypeDefOrRef type, MethodDefOrRef method, CodeList list)
		{
			Type = type;
			Method = method;
			int num = method.Flag switch
			{
				MethodDefOrRefFlag.MethodDef => method.MethodDef.ParamList.Length, 
				MethodDefOrRefFlag.MemberRef => (method.MemberRef.Signature as MethodDefSig).ParamCount, 
				_ => throw new Exception(), 
			};
			Params = new CSharpCode[num];
			for (int num2 = num - 1; num2 >= 0; num2--)
			{
				Params[num2] = list.Pop();
			}
			Dups = new List<CSharpCode>();
		}
		public override string Print(int tabs = 0)
		=> GetTabs(tabs) + $"new {Type}({string.Join(",", (System.Collections.Generic.IEnumerable<CSharpCode>)Params)})" + PrintDup(tabs);
		public string PrintDup(int tabs)
		{
			if (Dups.Length == 0)
				return "";
			string[] array = new string[Dups.Length];
			for (int i = 0; i < Dups.Length; i++)
			{
				array[i] = Dups[i].Print(tabs + 1);
			}
			return "\n" + GetTabs(tabs) + "{\n" + string.Join(",\n", array) + "\n" + GetTabs(tabs) + "}";
		}
	}
}
