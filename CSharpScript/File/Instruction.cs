namespace CSharpScript.File
{
	public abstract class Instruction
	{
		public int ILIndex;
		public int Size;
		public Operator Operator;
		public Instruction(int ilIndex, int size, Operator op)
		{
			ILIndex = ilIndex;
			Operator = op;
			Size = size + op.Size;
		}
		public string GetPrefix()
		{
			return ("IL_" + $"{ILIndex}".PadLeft(4, '0') + "\t" + Operator.Name).PadRight(20, ' ');
		}
	}
}
