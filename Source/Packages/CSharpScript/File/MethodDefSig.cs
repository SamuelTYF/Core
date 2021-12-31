using System.Collections.Generic;
namespace CSharpScript.File
{
	public class MethodDefSig : MemberRefSig
	{
		public MethodDefSigAbbreviations Flags;
		public int GenericCount;
		public int ParamCount;
		public TypeSig ReturnType;
		public TypeSig[] ParamTypes;
		public MethodDefSig(byte[] bs, ref int index, MetadataRoot metadata)
		{
			Flags = (MethodDefSigAbbreviations)bs[index++];
			if (Flags.HasFlag(MethodDefSigAbbreviations.GENERIC))
				GenericCount = BlobHeap.ReadUnsigned(bs, ref index);
			else
				GenericCount = 0;
			ParamCount = BlobHeap.ReadUnsigned(bs, ref index);
			ReturnType = TypeSig.AnalyzeType(bs, ref index, metadata);
			ParamTypes = TypeSig.AnalyzeTypes(bs, ref index, ParamCount, metadata);
		}
        public override string ToString() 
			=> $"{ReturnType}({string.Join(",", (IEnumerable<TypeSig>)ParamTypes)})";
    }
}
