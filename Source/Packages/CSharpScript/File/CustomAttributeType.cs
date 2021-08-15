using System;
namespace CSharpScript.File
{
	public class CustomAttributeType
	{
		public CustomAttributeTypeFlag Flag;
		public MethodDef MethodDef;
		public MemberRef MemberRef;
		public MethodDefSig MethodDefSig;
		public CustomAttributeType(int value, MetadataRoot metadata)
		{
			Flag = (CustomAttributeTypeFlag)(value & 7);
			switch (Flag)
			{
			case CustomAttributeTypeFlag.MethodDef:
				MethodDef = metadata.TildeHeap.MethodDefTable.MethodDefs[(value >> 3) - 1];
				MethodDefSig = MethodDef.Signature;
				break;
			case CustomAttributeTypeFlag.MemberRef:
				MemberRef = metadata.TildeHeap.MemberRefTable.MemberRefs[(value >> 3) - 1];
				MethodDefSig = MemberRef.Signature as MethodDefSig;
				break;
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			return Flag switch
			{
				CustomAttributeTypeFlag.MethodDef => MethodDef.ToString(), 
				CustomAttributeTypeFlag.MemberRef => MemberRef.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
