using Collection;
namespace CSharpScript.File
{
	public class TypeSpec
	{
		public int Row;
		public TypeSig Signature;
		public int Token;
		public TypeDefOrRef Parent;
		public List<MemberRef> Methods = new();
		public List<MemberRef> Fields = new();
		public TypeSpec(int index, int row)
		{
			Row = row;
			Token = index | 0x1B000000;
		}
		public TypeSpec(TypeSpecTable table, TypeSig sig)
		{
			table.TypeSpecs.Add(this);
			Row = -1;
			Token = table.TypeSpecs.Length | 0x1B000000;
			Signature = sig;
			if (Signature is TypeSig_Generic)
				Parent = (Signature as TypeSig_Generic).Encoded.Type;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			int index = 0;
			Signature = TypeSig.AnalyzeType(metadata.BlobHeap.Values[Row], ref index, metadata);
			metadata.BlobHeap.ParsedValues[Row] = this;
			if (Signature is TypeSig_Generic)
				Parent = (Signature as TypeSig_Generic).Encoded.Type;
		}
        public override string ToString() => Signature.ToString();
    }
}
