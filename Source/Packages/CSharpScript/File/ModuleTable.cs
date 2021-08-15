namespace CSharpScript.File
{
	public struct ModuleTable
	{
		public Module[] Modules;
		public ModuleTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Modules = new Module[count];
			for (int i = 0; i < count; i++)
			{
				Modules[i] = new Module(new ModuleRow
				{
					Index = i + 1,
					Generation = TildeHeap.Read(bs, ref index, islong: false),
					Name = TildeHeap.Read(bs, ref index, isStringLong),
					Mvid = TildeHeap.Read(bs, ref index, isGuidLong),
					EncId = TildeHeap.Read(bs, ref index, isGuidLong),
					EncBaseId = TildeHeap.Read(bs, ref index, isGuidLong)
				});
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (Modules != null)
			{
				Module[] modules = Modules;
				for (int i = 0; i < modules.Length; i++)
				{
					modules[i].ResolveSignature(metadata);
				}
			}
		}
	}
}
