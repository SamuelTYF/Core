using System;
using Collection;
namespace CSharpScript.File
{
	public class CustomAttribute
	{
		public bool Resolved = false;
		public HasCustomAttribute Parent;
		public CustomAttributeType Type;
		public CustomAttributeSig Value;
		public CustomAttributeRow Row;
		public int Token;
		public CustomAttribute(CustomAttributeRow row)
		{
			CustomAttributeRow customAttributeRow = (Row = row);
			Token = customAttributeRow.Index | 0xC000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (Resolved)
				return;
			Parent = new HasCustomAttribute(Row.Parent, metadata);
			Type = new CustomAttributeType(Row.Type, metadata);
			int index = 0;
			bool Success = true;
			Value = new CustomAttributeSig(metadata.BlobHeap.Values[Row.Value], ref index, Type.MethodDefSig, metadata, ref Success);
			metadata.BlobHeap.ParsedValues[Row.Value] = this;
			if (Success)
			{
				Resolved = true;
				switch (Parent.Flag)
				{
				case HasCustomAttributeFlag.MethodDef:
					Parent.MethodDef.CustomAttributes.Add(this);
					break;
				case HasCustomAttributeFlag.Field:
					Parent.Field.CustomAttributes.Add(this);
					break;
				case HasCustomAttributeFlag.TypeRef:
					Parent.TypeRef.CustomAttributes.Add(this);
					break;
				case HasCustomAttributeFlag.TypeDef:
					Parent.TypeDef.CustomAttributes.Add(this);
					break;
				case HasCustomAttributeFlag.Param:
					Parent.Param.ParamArray = this;
					break;
				case HasCustomAttributeFlag.Module:
					Parent.Module.CustomAttributes.Add(this);
					break;
				case HasCustomAttributeFlag.Property:
					Parent.Property.CustomAttributes.Add(this);
					break;
				case HasCustomAttributeFlag.Assembly:
					Parent.Assembly.CustomAttributes.Add(this);
					break;
				case HasCustomAttributeFlag.Event:
					Parent.Event.CustomAttributes.Add(this);
					break;
				case HasCustomAttributeFlag.InterfaceImpl:
					Parent.InterfaceImpl.CustomAttributes.Add(this);
					break;
				default:
					throw new Exception();
				}
			}
		}
		public override string ToString()
		{
			if (!Resolved)
				return "Not Resolved";
			List<string> list = new();
			string text = Type.Flag switch
			{
				CustomAttributeTypeFlag.MethodDef => Type.MethodDef.Parent.FullName, 
				CustomAttributeTypeFlag.MemberRef => Type.MemberRef.Class.TypeRef.FullName, 
				_ => throw new Exception(), 
			};
			if (text is "" or null)
				throw new Exception();
			FixedArg[] fixedArgs = Value.FixedArgs;
			for (int i = 0; i < fixedArgs.Length; i++)
				list.Add(GetString(fixedArgs[i]));
			NamedArg[] namedArgs = Value.NamedArgs;
			foreach (NamedArg namedArg in namedArgs)
				list.Add(namedArg.Name + " = " + GetString(namedArg.Value));
			return "[" + text + "(" + string.Join(",", list) + ")]";
		}
		public static bool HasFlag(byte[] A, byte[] B)
		{
			int num = 0;
			while (num < B.Length)
			{
				if ((A[num] & B[num]) != B[num++])
					return false;
			}
			while (num < A.Length)
			{
				if (A[num++] != 0)
					return false;
			}
			return true;
		}
		public static string GetString(FixedArg arg)
		{
			switch (arg.Type)
			{
			case ElementType.ELEMENT_TYPE_BOOLEAN:
				return arg.Value.ToString();
			case ElementType.ELEMENT_TYPE_R8:
				return $"{arg.Value}";
			case ElementType.ELEMENT_TYPE_R4:
				return $"{arg.Value}f";
			case ElementType.ELEMENT_TYPE_I8:
				return $"0x{arg.Value:X}";
			case ElementType.ELEMENT_TYPE_U8:
				return $"0x{arg.Value:X}u";
			case ElementType.ELEMENT_TYPE_I4:
				return $"0x{arg.Value:X}";
			case ElementType.ELEMENT_TYPE_U4:
				return $"0x{arg.Value:X}u";
			case ElementType.ELEMENT_TYPE_I2:
				return $"0x{arg.Value:X}";
			case ElementType.ELEMENT_TYPE_U2:
				return $"0x{arg.Value:X}u";
			case ElementType.ELEMENT_TYPE_I1:
				return $"0x{arg.Value:X}";
			case ElementType.ELEMENT_TYPE_U1:
				return $"0x{arg.Value:X}u";
			case ElementType.ELEMENT_TYPE_CLASS:
				return $"typeof({arg.Value})";
			case ElementType.ELEMENT_TYPE_VALUETYPE:
			{
				(TypeDef, byte[]) obj2 = ((TypeDef, byte[]))arg.Value;
				TypeDef item3 = obj2.Item1;
				byte[] item4 = obj2.Item2;
				List<string> list2 = new();
				for (int j = 1; j < item3.FieldList.Length; j++)
				{
					if (HasFlag(item4, item3.FieldList[j].Constant.Value))
						list2.Add($"{item3}.{item3.FieldList[j].Name}");
				}
                return list2.Length == 0 ? $"({item3})0x{BitConverter.ToInt64(item4, 0):X}" : string.Join("|", list2);
            }
            case ElementType.ELEMENT_TYPE_STRING:
				return (arg.Value == null) ? "null" : $"\"{arg.Value}\"";
			case ElementType.ELEMENT_TYPE_ENUM:
			{
				(TypeDef, byte[]) obj = ((TypeDef, byte[]))arg.Value;
				TypeDef item = obj.Item1;
				byte[] item2 = obj.Item2;
				List<string> list = new();
				for (int i = 1; i < item.FieldList.Length; i++)
					if (HasFlag(item2, item.FieldList[i].Constant.Value))
						list.Add($"{item}.{item.FieldList[i].Name}");
				if (list.Length == 0)
					return $"({item})0x{BitConverter.ToInt64(item2, 0):X}";
				return string.Join("|", list);
			}
			case ElementType.ELEMENT_TYPE_BOXED:
				return GetString(arg.Value as FixedArg);
			case ElementType.ELEMENT_TYPE_OBJECT:
				return GetString(arg.Value as FixedArg);
			case ElementType.ELEMENT_TYPE_SZARRAY:
				List<string> sz = new();
				foreach (FixedArg fa in arg.Value as FixedArg[])
					sz.Add(GetString(fa));
				return $"[{string.Join(",", sz)}]";
			default:
				throw new Exception();
			}
		}
	}
}
