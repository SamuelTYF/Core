using System;
namespace CSharpScript.File
{
	public class MemberForwarded
	{
		public MemberForwardedFlag Flag;
		public MethodDef MethodDef;
		public string Name;
		public MemberForwarded(int value, MetadataRoot metadata)
		{
			Flag = (MemberForwardedFlag)(value & 1);
			if (Flag == MemberForwardedFlag.MethodDef)
			{
				MethodDef = metadata.TildeHeap.MethodDefTable.MethodDefs[(value >> 1) - 1];
				Name = MethodDef.Name;
				return;
			}
			throw new Exception();
		}
		public override string ToString()
		{
			if (Flag == MemberForwardedFlag.MethodDef)
				return MethodDef.ToString();
			throw new Exception();
		}
	}
}
