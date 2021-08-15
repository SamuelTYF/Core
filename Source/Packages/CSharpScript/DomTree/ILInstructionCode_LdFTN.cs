using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_LdFTN : ILInstructionCode
	{
		public MethodDefOrRef Method;
		public ILInstructionCode_LdFTN(Instruction_InlineMethod method, int offset, int length)
			: base(offset, length, ILCodeFlag.LdFTN)
		{
			Method = new MethodDefOrRef(method.MethodDef, method.MemberRef, method.MethodSpec);
		}
		public override string Print()
		{
			return $"ldftn          {Method}";
		}
	}
}
