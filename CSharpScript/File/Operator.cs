using System.Reflection.Emit;
namespace CSharpScript.File
{
	public class Operator
	{
		public string Name;
		public string FieldName;
		public int Size;
		public byte First;
		public byte Second;
		public int Value;
		public OperandType OperandType;
		public OpCode OpCode;
		public Operator(string name, string fieldName, int size, byte first, byte second, OperandType type, OpCode opcode)
		{
			Name = name;
			FieldName = fieldName;
			Size = size;
			First = first;
			Second = second;
			OperandType = type;
			OpCode = opcode;
			Value = (First << 8) | Second;
		}
	}
}
