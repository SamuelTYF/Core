namespace CSharpScript.File
{
	public class AssemblyRefProcessor
	{
		public int Processor;
		public AssemblyRef AssemblyRef;
		public AssemblyRefProcessorRow Row;
		public int Token;
		public AssemblyRefProcessor(AssemblyRefProcessorRow row)
		{
			AssemblyRefProcessorRow assemblyRefProcessorRow = (Row = row);
			Token = assemblyRefProcessorRow.Index | 0x24000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Processor = Row.Processor;
			AssemblyRef = metadata.TildeHeap.AssemblyRefTable.AssemblyRefs[Row.AssemblyRef - 1];
		}
	}
}
