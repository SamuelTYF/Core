using System;
using System.Collections.Generic;
namespace CSharpScript.File
{
	public sealed class CSharpCodeInvoke : CSharpCode
	{
		public MethodDefSig mds;
		public MethodDefOrRef Method;
		public CSharpCode Object;
		public CSharpCode[] Params;
		public bool IsStatic;
		public CSharpCodeInvoke(MethodDefOrRef method, CodeList codes)
		{
			Method = method;
			switch (method.Flag)
			{
			case MethodDefOrRefFlag.MethodDef:
				mds = method.MethodDef.Signature;
				break;
			case MethodDefOrRefFlag.MemberRef:
				mds = method.MemberRef.Signature as MethodDefSig;
				break;
			default:
				throw new Exception();
			}
			Params = new CSharpCode[mds.ParamCount];
			for (int num = mds.ParamCount - 1; num >= 0; num--)
			{
				Params[num] = codes.Pop();
			}
			IsStatic = !mds.Flags.HasFlag(MethodDefSigAbbreviations.HASTHIS);
			if (!IsStatic)
				Object = codes.Pop();
		}
		public override string Print(int tabs = 0)
		{
			string tabs2 = GetTabs(tabs);
			return string.Concat(((!IsStatic) ? (tabs2 + Object.ToString()) : (tabs2 + Method.Parent.ToString())) + "." + Method.Name(), "(", string.Join(",", (IEnumerable<CSharpCode>)Params), ")");
		}
	}
}
