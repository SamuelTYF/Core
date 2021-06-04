using System;
namespace CSharpScript.File
{
	public class LocalVarSig : StandAloneSig
	{
		public int Count;
		public bool[] Pinned;
		public TypeSig[] Types;
		public LocalVarSig(byte[] bs, ref int index, MetadataRoot metadata)
		{
			if (bs[index++] != 7)
				throw new Exception();
			Count = BlobHeap.ReadUnsigned(bs, ref index);
			Pinned = new bool[Count];
			Types = new TypeSig[Count];
			for (int i = 0; i < Count; i++)
			{
				CustomMod[] customMods = CustomMod.TryAnaylzeCustomModes(bs, ref index, metadata);
				if (bs[index] == 69)
				{
					Pinned[i] = true;
					index++;
				}
				Types[i] = TypeSig.AnalyzeType(bs, ref index, metadata);
				Types[i].CustomMods = customMods;
			}
			if (index != bs.Length)
				throw new Exception();
		}
        public override string ToString()
        {
			string s = "LocalVarSig\n";
			for (int i = 0; i < Count; i++)
				s += $"{Pinned[i]}\t{Types[i]}\n";
			return s;
        }
    }
}
