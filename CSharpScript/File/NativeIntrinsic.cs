using System;
namespace CSharpScript.File
{
	public class NativeIntrinsic : MarshalSpec
	{
		public UnmanagedType UnmanagedType;
		public NativeIntrinsic(byte[] bs, ref int index)
		{
			NativeType = (NativeType)bs[index++];
			UnmanagedType = (UnmanagedType)NativeType;
			if (NativeType == NativeType.NATIVE_TYPE_ARRAY)
				throw new Exception();
		}
		public override string ToString()
		{
			return $"{PrintConfig._UnmanagedType}.{UnmanagedType}";
		}
	}
}
