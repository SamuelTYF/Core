namespace CSharpScript.File
{
	public struct AssemblyProcessorTable
	{
		public int[] Processors;
		public AssemblyProcessorTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Processors = new int[count];
			for (int i = 0; i < count; i++)
				Processors[i] = TildeHeap.Read(bs, ref index, islong: true);
		}
	}
}
