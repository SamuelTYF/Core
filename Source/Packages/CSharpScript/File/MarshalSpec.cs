namespace CSharpScript.File
{
	public abstract class MarshalSpec
	{
		public NativeType NativeType;
		public static MarshalSpec AnalyzeMarshalSpec(byte[] bs, ref int index)
		{
			if (bs[index] == 42)
				return new MarshalSpec_Array(bs, ref index);
			return new NativeIntrinsic(bs, ref index);
		}
	}
}
