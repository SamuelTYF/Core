namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Operator : ILInstructionCode
	{
		public BaseOperator Operator;
		public bool Checked;
		public ILInstructionCode_Operator(BaseOperator op, int offset, int length, bool @checked = false)
			: base(offset, length, ILCodeFlag.Operator)
		{
			Operator = op;
			Checked = @checked;
		}
		public override string Print()
		{
			return Operator.ToString();
		}
	}
}
