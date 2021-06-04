using System;
namespace CSharpScript.File
{
	public class MethodSpec
	{
		public MethodDefOrRef Method;
		public MethodSpecSig Instantiation;
		public MethodSpecRow Row;
		public int Token;
		public MethodSpec(MethodSpecRow row)
		{
			MethodSpecRow methodSpecRow = (Row = row);
			Token = methodSpecRow.Index | 0x2B000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Method = new MethodDefOrRef(Row.Method, metadata);
			int index = 0;
			Instantiation = new MethodSpecSig(metadata.BlobHeap.Values[Row.Instantiation], ref index, metadata);
			metadata.BlobHeap.ParsedValues[Row.Instantiation] = this;
		}
		public string GetTypeSig(TypeSig type, TypeSig[] mvars, TypeSig[] vars = null)
		{
			if (type == null)
				return "Void";
			switch (type.ElementType)
			{
			case ElementType.ELEMENT_TYPE_MVAR:
				return mvars[(type as TypeSig_MVAR).Number].ToString();
			case ElementType.ELEMENT_TYPE_VAR:
				return vars[(type as TypeSig_VAR).Number].ToString();
			case ElementType.ELEMENT_TYPE_SZARRAY:
				return GetTypeSig((type as TypeSig_SZARRAY).Type, mvars, vars) + "[]";
			case ElementType.ELEMENT_TYPE_GENERICINST:
			{
				TypeSig_Generic typeSig_Generic = type as TypeSig_Generic;
				return string.Format("{0}<{1}>", typeSig_Generic.Encoded, string.Join(",", Array.ConvertAll(typeSig_Generic.Types, (TypeSig t) => GetTypeSig(t, mvars, vars))));
			}
			case ElementType.ELEMENT_TYPE_BYREF:
				return (type as TypeSig_ByRef).Type.ToString();
			case ElementType.ELEMENT_TYPE_CLASS:
				return (type as TypeSig_Class).Encoded.ToString();
			case ElementType.ELEMENT_TYPE_VALUETYPE:
				return (type as TypeSig_ValueType).Encoded.ToString();
			case ElementType.ELEMENT_TYPE_I8:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_I4:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_I2:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_I1:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_U8:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_U4:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_U2:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_U1:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_STRING:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_OBJECT:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_BOOLEAN:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_I:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_U:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_PTR:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_FNPTR:
				return type.ToString();
			case ElementType.ELEMENT_TYPE_VOID:
				return type.ToString();
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			TypeSig[] types = Instantiation.GenericTypes;
			TypeSig[] vars = null;
			switch (Method.Flag)
			{
			case MethodDefOrRefFlag.MethodDef:
			{
				MethodDef methodDef = Method.MethodDef;
				if (methodDef.GenericParams.Length != types.Length)
					throw new Exception();
				string text = ((methodDef.ReturnParam != null) ? GetTypeSig(methodDef.ReturnParam.Type, types, vars) : "Void");
				return text + " " + methodDef.Name + "(" + string.Join(",", Array.ConvertAll(methodDef.ParamList, (Param p) => GetTypeSig(p.Type, types, vars))) + ")";
			}
			case MethodDefOrRefFlag.MemberRef:
			{
				MemberRef memberRef = Method.MemberRef;
				if (memberRef.Class.Flag == MemberRefParentFlag.TypeSpec)
					vars = (memberRef.Class.TypeSpec.Signature as TypeSig_Generic).Types;
				MethodDefSig methodDefSig = memberRef.Signature as MethodDefSig;
				if (methodDefSig.GenericCount != types.Length)
					throw new Exception();
				return GetTypeSig(methodDefSig.ReturnType, types, vars) + " " + memberRef.Name + "(" + string.Join(",", Array.ConvertAll(methodDefSig.ParamTypes, (TypeSig pt) => GetTypeSig(pt, types, vars))) + ")";
			}
			default:
				throw new Exception();
			}
		}
	}
}
