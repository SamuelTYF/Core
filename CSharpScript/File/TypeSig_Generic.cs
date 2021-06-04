using System.Collections.Generic;
namespace CSharpScript.File
{
	public class TypeSig_Generic : TypeSig
	{
		public ElementType ClassOrValue;
		public TypeDefOrRefOrSpecEncoded Encoded;
		public TypeSig[] Types;
		public TypeSig_Generic(ElementType classOrValue, TypeDefOrRefOrSpecEncoded encoded, TypeSig[] types)
			: base(ElementType.ELEMENT_TYPE_GENERICINST)
		{
			ClassOrValue = classOrValue;
			Encoded = encoded;
			Types = types;
		}
		public static TypeSig_Generic AnalyzeType_Generic(byte[] bs, ref int index, MetadataRoot metadata)
		{
			return new TypeSig_Generic((ElementType)bs[index++], new TypeDefOrRefOrSpecEncoded(bs, ref index, metadata), TypeSig.AnalyzeTypes(bs, ref index, BlobHeap.ReadUnsigned(bs, ref index), metadata));
		}
		public override string ToString()
		{
			return string.Format("{0}<{1}>", Encoded, string.Join(",", (IEnumerable<TypeSig>)Types));
		}
	}
}
