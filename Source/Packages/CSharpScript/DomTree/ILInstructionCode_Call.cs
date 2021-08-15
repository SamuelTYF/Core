using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Call : ILInstructionCode
	{
		public MethodDefOrRef Method;
        public ILInstructionCode_Call(Instruction_InlineMethod method, int offset, int length)
            : base(offset, length, ILCodeFlag.Call) 
			=> Method = new MethodDefOrRef(method.MethodDef, method.MemberRef, method.MethodSpec);
        public override string Print() => $"call           {Method}";
    }
}
