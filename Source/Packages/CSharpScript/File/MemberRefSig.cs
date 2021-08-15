namespace CSharpScript.File
{
	public abstract class MemberRefSig
	{
        public static MemberRefSig AnalyzeMemberRef(byte[] bs, ref int index, MetadataRoot metadata) => bs[index] == 6 ? FieldSig.AnalyzeFieldSig(bs, ref index, metadata) : new MethodDefSig(bs, ref index, metadata);
    }
}
