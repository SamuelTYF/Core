using System.Collections.Generic;
using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public sealed class ILMethod : ILCode
	{
		public Param[] Params;
		public LocalVar[] LocalVars;
		public ILBody Body;
		public ILMethod(Param[] @params, LocalVar[] locals, ILBody body)
			: base(body.Info.Offset, body.Info.Length, ILCodeFlag.ILMethod)
		{
			Parent = null;
			Params = @params;
			LocalVars = locals;
			Body = body;
			Body.Method = this;
		}
		public override string Print(int tabs = 0)
		{
			return ".params\n\t" + string.Join(",\n\t", (IEnumerable<Param>)Params) + "\n.locals\n\t" + string.Join(",\n\t", (IEnumerable<LocalVar>)LocalVars) + "\n.code\n" + Body.Print();
		}
	}
}
