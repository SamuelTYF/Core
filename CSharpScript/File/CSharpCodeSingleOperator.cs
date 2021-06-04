using System;
namespace CSharpScript.File
{
	public sealed class CSharpCodeSingleOperator : CSharpCode
	{
		public static readonly string[] Signs = new string[4] { "-{0}", "~{0}", "{0}==true", "{0}==false" };
		public CSharpCode Destination;
		public SingleOperator Operator;
		public string Sign;
		public CSharpCodeSingleOperator(CSharpCode des, SingleOperator op)
		{
			Destination = des;
			Operator = op;
			Sign = Signs[(int)Operator];
			switch (op)
			{
			case SingleOperator.@true:
				if (des is CSharpCodeLocalVar)
					Sign = "{0}==" + GetSign((des as CSharpCodeLocalVar).Var.Type);
				else if (des is CSharpCodeParam)
				{
					Sign = "{0}==" + GetSign((des as CSharpCodeParam).Param.Type);
				}
				break;
			case SingleOperator.@false:
				if (des is CSharpCodeLocalVar)
					Sign = "{0}!=" + GetSign((des as CSharpCodeLocalVar).Var.Type);
				else if (des is CSharpCodeParam)
				{
					Sign = "{0}!=" + GetSign((des as CSharpCodeParam).Param.Type);
				}
				break;
			}
		}
		public string GetSign(TypeSig type)
		{
			switch (type.ElementType)
			{
			case ElementType.ELEMENT_TYPE_STRING:
			case ElementType.ELEMENT_TYPE_VAR:
			case ElementType.ELEMENT_TYPE_ARRAY:
			case ElementType.ELEMENT_TYPE_GENERICINST:
			case ElementType.ELEMENT_TYPE_MVAR:
				return "null";
			case ElementType.ELEMENT_TYPE_I1:
			case ElementType.ELEMENT_TYPE_U1:
			case ElementType.ELEMENT_TYPE_I2:
			case ElementType.ELEMENT_TYPE_U2:
			case ElementType.ELEMENT_TYPE_I4:
			case ElementType.ELEMENT_TYPE_U4:
			case ElementType.ELEMENT_TYPE_I8:
			case ElementType.ELEMENT_TYPE_U8:
				return "0";
			default:
				throw new Exception();
			}
		}
		public override string Print(int tabs = 0)
		{
			return string.Format(Sign, Destination);
		}
	}
}
