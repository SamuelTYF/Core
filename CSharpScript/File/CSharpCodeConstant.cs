using System;
using CSharpScript.DomTree;
namespace CSharpScript.File
{
	public sealed class CSharpCodeConstant : CSharpCode
	{
		public ElementType Type;
		public object Value;
		public CSharpCodeConstant(ILInstructionCode_LdC ins)
		{
			Type = ins.Type;
			Value = ins.Value;
		}
		public CSharpCodeConstant(ElementType type, object value)
		{
			Type = type;
			Value = value;
		}
		public int ToInt32()
		{
			return Type switch
			{
				ElementType.ELEMENT_TYPE_I1 => (byte)Value, 
				ElementType.ELEMENT_TYPE_I2 => (short)Value, 
				ElementType.ELEMENT_TYPE_I4 => (int)Value, 
				_ => throw new Exception(), 
			};
		}
		public override string Print(int tabs = 0)
		{
			return GetTabs(tabs) + ((Value == null) ? "null" : Value.ToString());
		}
	}
}
