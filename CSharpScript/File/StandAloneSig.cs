using System;
namespace CSharpScript.File
{
	public class StandAloneSig
	{
		public static StandAloneSig AnalyzeStandAlone(byte[] bs, ref int index, MetadataRoot metadata)
		{
			if (bs[index] == 7)
				return new LocalVarSig(bs, ref index, metadata);
			Console.WriteLine(BlobHeap.GetKey(bs));
			Console.WriteLine(FieldSig.AnalyzeFieldSig(bs, ref index, metadata).Type);
			return null;
		}
	}
}
