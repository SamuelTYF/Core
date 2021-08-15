using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_CallVirt : ILInstructionCode
	{
		public MethodDefOrRef Method;
		public ILInstructionCode_CallVirt(Instruction_InlineMethod method, int offset, int length)
			: base(offset, length, ILCodeFlag.CallVirt)
		{
			Method = new MethodDefOrRef(method.MethodDef, method.MemberRef, method.MethodSpec);
		}
		public override string Print()
		{
			return $"callvirt       {Method}";
		}
	}
}
