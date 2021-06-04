using System;
namespace CSharpScript.File
{
	public class MethodSpecSig
	{
		public TypeSig[] GenericTypes;
		public MethodSpecSig(byte[] bs, ref int index, MetadataRoot metadata)
		{
			if (bs[index++] != 10)
				throw new Exception();
			GenericTypes = TypeSig.AnalyzeTypes(bs, ref index, BlobHeap.ReadUnsigned(bs, ref index), metadata);
			if (index != bs.Length)
				throw new Exception();
		}
	}
}
