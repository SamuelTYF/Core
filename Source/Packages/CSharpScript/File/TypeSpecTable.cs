using Collection;
namespace CSharpScript.File
{
	public struct TypeSpecTable
	{
		public int Count;
		public List<TypeSpec> TypeSpecs;
		public TypeSpecTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Count = count;
			TypeSpecs = new();
			for (int i = 0; i < count; i++)
				TypeSpecs.Add(new TypeSpec(i + 1, TildeHeap.Read(bs, ref index, isBlobLong)));
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (TypeSpecs == null)return;
			foreach (TypeSpec typeSpec in TypeSpecs)
				typeSpec.ResolveSignature(metadata);
		}
	}
}
