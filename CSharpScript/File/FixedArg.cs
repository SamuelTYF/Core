using System;
namespace CSharpScript.File
{
	public class FixedArg
	{
		public ElementType Type;
		public object Value;
		public FixedArg(byte[] bs, ref int index, TypeSig type, MetadataRoot metadata, ref bool Success)
		{
			switch (Type = type.ElementType)
			{
			case ElementType.ELEMENT_TYPE_BOOLEAN:
				Value = bs[index++] == 1;
				break;
			case ElementType.ELEMENT_TYPE_CHAR:
				Value = (char)(bs[index++] | (bs[index++] << 8));
				break;
			case ElementType.ELEMENT_TYPE_I1:
				Value = bs[index++];
				break;
			case ElementType.ELEMENT_TYPE_U1:
				Value = (sbyte)bs[index++];
				break;
			case ElementType.ELEMENT_TYPE_I2:
				Value = (short)(bs[index++] | (bs[index++] << 8));
				break;
			case ElementType.ELEMENT_TYPE_U2:
				Value = (ushort)(bs[index++] | (bs[index++] << 8));
				break;
			case ElementType.ELEMENT_TYPE_I4:
				Value = bs[index++] | (bs[index++] << 8) | (bs[index++] << 16) | (bs[index++] << 24);
				break;
			case ElementType.ELEMENT_TYPE_U4:
				Value = (uint)(bs[index++] | (bs[index++] << 8) | (bs[index++] << 16) | (bs[index++] << 24));
				break;
			case ElementType.ELEMENT_TYPE_I8:
				Value = (long)(bs[index++] | ((ulong)bs[index++] << 8) | ((ulong)bs[index++] << 16) | ((ulong)bs[index++] << 24) | ((ulong)bs[index++] << 32) | ((ulong)bs[index++] << 40) | ((ulong)bs[index++] << 48) | ((ulong)bs[index++] << 56));
				break;
			case ElementType.ELEMENT_TYPE_U8:
				Value = bs[index++] | ((ulong)bs[index++] << 8) | ((ulong)bs[index++] << 16) | ((ulong)bs[index++] << 24) | ((ulong)bs[index++] << 32) | ((ulong)bs[index++] << 40) | ((ulong)bs[index++] << 48) | ((ulong)bs[index++] << 56);
				break;
			case ElementType.ELEMENT_TYPE_R8:
				Value = BitConverter.ToDouble(bs, index);
				index += 8;
				break;
			case ElementType.ELEMENT_TYPE_VALUETYPE:
			{
				if (type.ElementType != ElementType.ELEMENT_TYPE_VALUETYPE)
					throw new Exception();
				TypeDefOrRefOrSpecEncoded encoded = (type as TypeSig_ValueType).Encoded;
				byte[] array2 = new byte[8];
				TypeDef typeDef2;
				switch (encoded.Flag)
				{
				case TypeDefOrRefOrSpecFlag.TypeRef:
					typeDef2 = encoded.Type.TypeRef.TypeDef;
					if (typeDef2 == null)
					{
						Success = false;
						return;
					}
					break;
				case TypeDefOrRefOrSpecFlag.TypeDef:
					typeDef2 = encoded.Type.TypeDef;
					break;
				default:
					throw new Exception();
				}
				if (typeDef2.FieldList.Length == 0)
					throw new Exception();
				Field field2 = typeDef2.FieldList[0];
				if (field2.Name != "value__")
					throw new Exception();
				int num2 = field2.Signature.Type.ElementType switch
				{
					ElementType.ELEMENT_TYPE_BOOLEAN => 1, 
					ElementType.ELEMENT_TYPE_CHAR => 2, 
					ElementType.ELEMENT_TYPE_I1 => 1, 
					ElementType.ELEMENT_TYPE_U1 => 1, 
					ElementType.ELEMENT_TYPE_I2 => 2, 
					ElementType.ELEMENT_TYPE_U2 => 2, 
					ElementType.ELEMENT_TYPE_I4 => 4, 
					ElementType.ELEMENT_TYPE_U4 => 4, 
					ElementType.ELEMENT_TYPE_I8 => 8, 
					ElementType.ELEMENT_TYPE_U8 => 8, 
					_ => throw new Exception(), 
				};
				Array.Copy(bs, index, array2, 0, num2);
				index += num2;
				Value = (typeDef2, array2);
				break;
			}
			case ElementType.ELEMENT_TYPE_STRING:
				Value = BlobHeap.ReadSerString(bs, ref index);
				break;
			case ElementType.ELEMENT_TYPE_ENUM:
			{
				string name = (type as TypeSig_Enum).Name;
				byte[] array = new byte[8];
				TypeDef typeDef = metadata.FindTypeDef(name);
				if (typeDef == null)
				{
					Success = false;
					break;
				}
				if (typeDef.FieldList.Length == 0)
					throw new Exception();
				Field field = typeDef.FieldList[0];
				if (field.Name != "value__")
					throw new Exception();
				int num = field.Signature.Type.ElementType switch
				{
					ElementType.ELEMENT_TYPE_BOOLEAN => 1, 
					ElementType.ELEMENT_TYPE_CHAR => 2, 
					ElementType.ELEMENT_TYPE_I1 => 1, 
					ElementType.ELEMENT_TYPE_U1 => 1, 
					ElementType.ELEMENT_TYPE_I2 => 2, 
					ElementType.ELEMENT_TYPE_U2 => 2, 
					ElementType.ELEMENT_TYPE_I4 => 4, 
					ElementType.ELEMENT_TYPE_U4 => 4, 
					ElementType.ELEMENT_TYPE_I8 => 8, 
					ElementType.ELEMENT_TYPE_U8 => 8, 
					_ => throw new Exception(), 
				};
				Array.Copy(bs, index, array, 0, num);
				index += num;
				Value = (typeDef, array);
				break;
			}
			case ElementType.ELEMENT_TYPE_CLASS:
				Value = metadata.FindTypeDef(BlobHeap.ReadSerString(bs, ref index));
				break;
			case ElementType.ELEMENT_TYPE_BOXED:
				Value = new FixedArg(bs, ref index, TypeSig.AnalyzeType(bs, ref index, metadata), metadata, ref Success);
				break;
			case ElementType.ELEMENT_TYPE_OBJECT:
				Value = new FixedArg(bs, ref index, TypeSig.AnalyzeType(bs, ref index, metadata), metadata, ref Success);
				break;
			case ElementType.ELEMENT_TYPE_TYPE:
				Value = metadata.FindTypeDef(BlobHeap.ReadSerString(bs, ref index));
				Success = Value != null;
				break;
			case ElementType.ELEMENT_TYPE_SZARRAY:
				TypeSig valuetype = (type as TypeSig_SZARRAY).Type;
				FixedArg[] values = new FixedArg[BitConverter.ToInt32(bs, index)];
				index += 4;
				for (int i = 0; i < values.Length; i++)
					values[i] = new(bs, ref index, valuetype, metadata, ref Success);
				Value = values;
				break;
			default:
				throw new Exception();
			}
		}
        public override string ToString() => Value.ToString();
    }
}
