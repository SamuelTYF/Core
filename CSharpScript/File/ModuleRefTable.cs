namespace CSharpScript.File
{
	public struct ModuleRefTable
	{
		public int Count;
		public ModuleRef[] ModuleRefs;
		public ModuleRefTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Count = count;
			ModuleRefs = new ModuleRef[count];
			for (int i = 0; i < count; i++)
			{
				ModuleRefs[i] = new ModuleRef(i + 1, TildeHeap.Read(bs, ref index, isStringLong));
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (ModuleRefs != null)
			{
				ModuleRef[] moduleRefs = ModuleRefs;
				for (int i = 0; i < moduleRefs.Length; i++)
				{
					moduleRefs[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
