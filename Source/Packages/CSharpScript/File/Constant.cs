using System;
using System.Text;
namespace CSharpScript.File
{
	public class Constant
	{
		public ElementType Type;
		public HasConstant Parent;
		public byte[] Value;
		public ConstantRow Row;
		public int Token;
		public Constant(ConstantRow row)
			=>Token = (Row = row).Index | 0xB000000;
		public void ResolveSignature(MetadataRoot metadata)
		{
			Type = (ElementType)Row.Type;
			Parent = new HasConstant(Row.Parent, metadata);
			Value = metadata.BlobHeap.Values[Row.Value];
			metadata.BlobHeap.ParsedValues[Row.Value] = this;
			switch (Parent.Flag)
			{
			case HasConstantFlag.Field:
				Parent.Field.Constant = this;
				break;
			case HasConstantFlag.Param:
				Parent.Param.Constant = this;
				break;
			case HasConstantFlag.Property:
				throw new Exception();
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			switch (Type)
			{
			case ElementType.ELEMENT_TYPE_BOOLEAN:
				return (Value[0] == 1) ? "true" : "false";
			case ElementType.ELEMENT_TYPE_I8:
				return $"0x{BitConverter.ToInt64(Value, 0):X}";
			case ElementType.ELEMENT_TYPE_U8:
				return $"0x{BitConverter.ToInt64(Value, 0):X}u";
			case ElementType.ELEMENT_TYPE_I4:
				return $"0x{BitConverter.ToInt32(Value, 0):X}";
			case ElementType.ELEMENT_TYPE_U4:
				return $"0x{BitConverter.ToInt32(Value, 0):X}u";
			case ElementType.ELEMENT_TYPE_I2:
				return $"0x{BitConverter.ToInt16(Value, 0):X}";
			case ElementType.ELEMENT_TYPE_U2:
				return $"0x{BitConverter.ToInt16(Value, 0):X}u";
			case ElementType.ELEMENT_TYPE_I1:
				return $"0x{Value[0]:X}";
			case ElementType.ELEMENT_TYPE_U1:
				return $"0x{Value[0]:X}u";
			case ElementType.ELEMENT_TYPE_CLASS:
				if (Value.Length != 4 || BitConverter.ToInt32(Value, 0) != 0)
					throw new Exception();
				return "null";
			case ElementType.ELEMENT_TYPE_STRING:
				return "\"" + Encoding.Unicode.GetString(Value) + "\"";
			case ElementType.ELEMENT_TYPE_R8:
				return BitConverter.ToDouble(Value, 0).ToString();
			case ElementType.ELEMENT_TYPE_R4:
				return $"{BitConverter.ToSingle(Value, 0)}f";
			case ElementType.ELEMENT_TYPE_CHAR:
				return "'\\u" + Convert.ToString(BitConverter.ToChar(Value, 0), 16).PadLeft(4, '0') + "'";
			default:
				throw new Exception();
			}
		}
	}
}
