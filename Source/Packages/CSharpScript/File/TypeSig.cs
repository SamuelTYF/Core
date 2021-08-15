using System;
namespace CSharpScript.File
{
	public class TypeSig
	{
		public CustomMod[] CustomMods;
		public ElementType ElementType;
		public static readonly TypeSig Boolean = new TypeSig(ElementType.ELEMENT_TYPE_BOOLEAN);
		public static readonly TypeSig CHAR = new TypeSig(ElementType.ELEMENT_TYPE_CHAR);
		public static readonly TypeSig I = new TypeSig(ElementType.ELEMENT_TYPE_I);
		public static readonly TypeSig U = new TypeSig(ElementType.ELEMENT_TYPE_U);
		public static readonly TypeSig I1 = new TypeSig(ElementType.ELEMENT_TYPE_I1);
		public static readonly TypeSig U1 = new TypeSig(ElementType.ELEMENT_TYPE_U1);
		public static readonly TypeSig I2 = new TypeSig(ElementType.ELEMENT_TYPE_I2);
		public static readonly TypeSig U2 = new TypeSig(ElementType.ELEMENT_TYPE_U2);
		public static readonly TypeSig I4 = new TypeSig(ElementType.ELEMENT_TYPE_I4);
		public static readonly TypeSig U4 = new TypeSig(ElementType.ELEMENT_TYPE_U4);
		public static readonly TypeSig I8 = new TypeSig(ElementType.ELEMENT_TYPE_I8);
		public static readonly TypeSig U8 = new TypeSig(ElementType.ELEMENT_TYPE_U8);
		public static readonly TypeSig STRING = new TypeSig(ElementType.ELEMENT_TYPE_STRING);
		public static readonly TypeSig OBJECT = new TypeSig(ElementType.ELEMENT_TYPE_OBJECT);
		public static readonly TypeSig VOID = new TypeSig(ElementType.ELEMENT_TYPE_VOID);
		public static readonly TypeSig R4 = new TypeSig(ElementType.ELEMENT_TYPE_R4);
		public static readonly TypeSig R8 = new TypeSig(ElementType.ELEMENT_TYPE_R8);
		public static readonly TypeSig TYPEDBYREF = new TypeSig(ElementType.ELEMENT_TYPE_TYPEDBYREF);
		public static readonly TypeSig BOXED = new TypeSig(ElementType.ELEMENT_TYPE_BOXED);
		public static readonly TypeSig TYPE = new TypeSig(ElementType.ELEMENT_TYPE_TYPE);
		public TypeSig(ElementType type)
		{
			ElementType = type;
		}
		private static TypeSig _AnanlyzeTypeSig(byte[] bs, ref int index, MetadataRoot metadata, ElementType type)
		{
			return type switch
			{
				ElementType.ELEMENT_TYPE_GENERICINST => TypeSig_Generic.AnalyzeType_Generic(bs, ref index, metadata), 
				ElementType.ELEMENT_TYPE_VALUETYPE => TypeSig_ValueType.AnalyzeType_ValueType(bs, ref index, metadata), 
				ElementType.ELEMENT_TYPE_VAR => TypeSig_VAR.AnalyzeType_VAR(bs, ref index), 
				ElementType.ELEMENT_TYPE_MVAR => TypeSig_MVAR.AnalyzeType_MVAR(bs, ref index), 
				ElementType.ELEMENT_TYPE_SZARRAY => TypeSig_SZARRAY.AnalyzeType_SZARRAY(bs, ref index, metadata), 
				ElementType.ELEMENT_TYPE_CLASS => TypeSig_Class.AnalyzeType_Class(bs, ref index, metadata), 
				ElementType.ELEMENT_TYPE_PTR => TypeSig_PTR.AnalyzeType_PTR(bs, ref index, metadata), 
				ElementType.ELEMENT_TYPE_FNPTR => TypeSig_FNPTR.AnalyzeType_FNPTR(bs, ref index, metadata), 
				ElementType.ELEMENT_TYPE_ENUM => TypeSig_Enum.AnalyzeType_Enum(bs, ref index), 
				ElementType.ELEMENT_TYPE_BYREF => TypeSig_ByRef.AnaylzeType_ByRef(bs, ref index, metadata), 
				ElementType.ELEMENT_TYPE_ARRAY => TypeSig_ARRAY.AnalyzeType_ARRAY(bs, ref index, metadata), 
				ElementType.ELEMENT_TYPE_BOOLEAN => Boolean, 
				ElementType.ELEMENT_TYPE_CHAR => CHAR, 
				ElementType.ELEMENT_TYPE_I => I, 
				ElementType.ELEMENT_TYPE_U => U, 
				ElementType.ELEMENT_TYPE_I1 => I1, 
				ElementType.ELEMENT_TYPE_U1 => U1, 
				ElementType.ELEMENT_TYPE_I2 => I2, 
				ElementType.ELEMENT_TYPE_U2 => U2, 
				ElementType.ELEMENT_TYPE_I4 => I4, 
				ElementType.ELEMENT_TYPE_U4 => U4, 
				ElementType.ELEMENT_TYPE_I8 => I8, 
				ElementType.ELEMENT_TYPE_U8 => U8, 
				ElementType.ELEMENT_TYPE_STRING => STRING, 
				ElementType.ELEMENT_TYPE_OBJECT => OBJECT, 
				ElementType.ELEMENT_TYPE_VOID => VOID, 
				ElementType.ELEMENT_TYPE_R4 => R4, 
				ElementType.ELEMENT_TYPE_R8 => R8, 
				ElementType.ELEMENT_TYPE_TYPEDBYREF => TYPEDBYREF, 
				ElementType.ELEMENT_TYPE_BOXED => BOXED, 
				ElementType.ELEMENT_TYPE_TYPE => TYPE, 
				_ => throw new Exception(), 
			};
		}
		public static TypeSig AnalyzeType(byte[] bs, ref int index, MetadataRoot metadata)
		{
			CustomMod[] customMods = CustomMod.TryAnaylzeCustomModes(bs, ref index, metadata);
			TypeSig typeSig = _AnanlyzeTypeSig(bs, ref index, metadata, (ElementType)bs[index++]);
			typeSig.CustomMods = customMods;
			return typeSig;
		}
		public static TypeSig[] AnalyzeTypes(byte[] bs, ref int index, int count, MetadataRoot metadata)
		{
			TypeSig[] array = new TypeSig[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = AnalyzeType(bs, ref index, metadata);
			}
			return array;
		}
		public override string ToString()
		{
			return ElementType switch
			{
				ElementType.ELEMENT_TYPE_BOOLEAN => "bool", 
				ElementType.ELEMENT_TYPE_CHAR => "char", 
				ElementType.ELEMENT_TYPE_I => "IntPtr", 
				ElementType.ELEMENT_TYPE_U => "UIntPtr", 
				ElementType.ELEMENT_TYPE_I1 => "byte", 
				ElementType.ELEMENT_TYPE_U1 => "sbyte", 
				ElementType.ELEMENT_TYPE_I2 => "short", 
				ElementType.ELEMENT_TYPE_U2 => "ushort", 
				ElementType.ELEMENT_TYPE_I4 => "int", 
				ElementType.ELEMENT_TYPE_U4 => "uint", 
				ElementType.ELEMENT_TYPE_I8 => "long", 
				ElementType.ELEMENT_TYPE_U8 => "ulong", 
				ElementType.ELEMENT_TYPE_STRING => "string", 
				ElementType.ELEMENT_TYPE_OBJECT => "object", 
				ElementType.ELEMENT_TYPE_VOID => "void", 
				ElementType.ELEMENT_TYPE_R4 => "float", 
				ElementType.ELEMENT_TYPE_R8 => "double", 
				ElementType.ELEMENT_TYPE_TYPEDBYREF => "TypedReference", 
				_ => throw new Exception(), 
			};
		}
	}
}
