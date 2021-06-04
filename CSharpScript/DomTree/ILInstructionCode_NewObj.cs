using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_NewObj : ILInstructionCode
	{
		public MethodDefOrRef Method;
		public ILInstructionCode_NewObj(Instruction_InlineMethod method, int offset, int length)
			: base(offset, length, ILCodeFlag.NewObj)
		{
			Method = new MethodDefOrRef(method.MethodDef, method.MemberRef, method.MethodSpec);
		}
		public override string Print()
		{
			return $"newobj         {Method.Parent}";
		}
	}
}
