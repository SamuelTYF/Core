using System;
namespace CSharpScript.File
{
	public class MemberRef
	{
		public MemberRefParent Class;
		public string Name;
		public MemberRefSig Signature;
		public MemberRefRow Row;
		public int Token;
		public MemberRef(MemberRefRow row)
		{
			MemberRefRow memberRefRow = (Row = row);
			Token = memberRefRow.Index | 0xA000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Class = new MemberRefParent(Row.Class, metadata);
			Name = metadata.StringsHeap[Row.Name];
			int index = 0;
			Signature = MemberRefSig.AnalyzeMemberRef(metadata.BlobHeap.Values[Row.Signature], ref index, metadata);
			switch (Class.Flag)
			{
			case MemberRefParentFlag.TypeRef:
				if (Signature is MethodDefSig)
					Class.TypeRef.Methods.Add(this);
				else
					Class.TypeRef.Fields.Add(this);
				break;
			case MemberRefParentFlag.TypeSpec:
				if (Signature is MethodDefSig)
					Class.TypeSpec.Methods.Add(this);
				else
					Class.TypeSpec.Fields.Add(this);
				break;
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			return $"{Class}.{Name}";
		}
	}
}
