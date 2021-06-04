using System;
namespace CSharpScript.File
{
	public class FieldSig : MemberRefSig
	{
		public CustomMod[] CustomMods;
		public TypeSig Type;
		public FieldSig(CustomMod[] customMods, TypeSig type)
		{
			CustomMods = customMods;
			Type = type;
		}
		public static FieldSig AnalyzeFieldSig(byte[] bs, ref int index, MetadataRoot metadata)
		{
			if (bs[index++] != 6)
				throw new Exception();
			return new FieldSig(CustomMod.TryAnaylzeCustomModes(bs, ref index, metadata), TypeSig.AnalyzeType(bs, ref index, metadata));
		}
	}
}
