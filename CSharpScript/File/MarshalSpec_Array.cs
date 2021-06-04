using System;
namespace CSharpScript.File
{
	public class MarshalSpec_Array : MarshalSpec
	{
		public NativeType ArrayElemType;
		public int ParamNum;
		public int NumElem;
		public MarshalSpec_Array(byte[] bs, ref int index)
		{
			NativeType = (NativeType)bs[index++];
			if (NativeType != NativeType.NATIVE_TYPE_ARRAY)
				throw new Exception();
			ArrayElemType = (NativeType)bs[index++];
			ParamNum = ((bs.Length != index) ? BlobHeap.ReadUnsigned(bs, ref index) : 0);
			NumElem = ((bs.Length != index) ? BlobHeap.ReadUnsigned(bs, ref index) : 0);
		}
		public override string ToString()
		{
			string text = PrintConfig._UnmanagedType + ".LPArray";
			if (NumElem != 0)
				return text += $" , SizeConst = 0x{NumElem:X}";
			if (ParamNum != 0)
				return text += $" , SizeParamIndex = 0x{ParamNum:X}";
			return PrintConfig._UnmanagedType + ".LPArray";
		}
	}
}
