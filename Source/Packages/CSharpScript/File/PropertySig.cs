using System;
namespace CSharpScript.File
{
	public class PropertySig
	{
		public ElementType PropertyType;
		public TypeSig Type;
		public TypeSig[] Params;
		public PropertySig(byte[] bs, ref int index, MetadataRoot metadata)
		{
			PropertyType = (ElementType)bs[index++];
			int count = BlobHeap.ReadUnsigned(bs, ref index);
			Type = TypeSig.AnalyzeType(bs, ref index, metadata);
			Params = TypeSig.AnalyzeTypes(bs, ref index, count, metadata);
			if (index != bs.Length)
				throw new Exception();
		}
	}
}
