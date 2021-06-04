namespace CSharpScript.File
{
	public struct StandAloneSigTable
	{
		public int[] Signatures;
		public StandAloneSig[] StandAloneSigs;
		public StandAloneSigTable(byte[] bs, ref int index, int count, bool isStringLong, bool isGuidLong, bool isBlobLong)
		{
			Signatures = new int[count];
			StandAloneSigs = new StandAloneSig[count];
			for (int i = 0; i < count; i++)
			{
				Signatures[i] = TildeHeap.Read(bs, ref index, isBlobLong);
			}
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (StandAloneSigs != null)
			{
				for (int i = 0; i < StandAloneSigs.Length; i++)
				{
					int index = 0;
					StandAloneSigs[i] = StandAloneSig.AnalyzeStandAlone(metadata.BlobHeap.Values[Signatures[i]], ref index, metadata);
					metadata.BlobHeap.ParsedValues[Signatures[i]] = StandAloneSigs[i];
				}
			}
		}
	}
}
