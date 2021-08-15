using System.IO;
namespace CSharpScript.File
{
	public struct MethodHeader
	{
		public MethodHeaderTypeValues MethodHeaderTypeValues;
		public byte Size;
		public short MaxStack;
		public int CodeSize;
		public int LocalVarSigTok;
		public MethodHeader(int b, Stream stream)
		{
			if ((b & 3) == 2)
			{
				MethodHeaderTypeValues = MethodHeaderTypeValues.CorILMethod_TinyFormat;
				CodeSize = b >> 2;
				Size = 1;
				MaxStack = 8;
				LocalVarSigTok = 0;
			}
			else
			{
				byte[] array = new byte[12];
				stream.Read(array, 1, 11);
				array[0] = (byte)b;
				MethodHeaderTypeValues = (MethodHeaderTypeValues)(array[0] | ((array[1] & 0xF) << 8));
				Size = (byte)(array[1] >> 4);
				MaxStack = (short)(array[2] | (array[3] << 8));
				CodeSize = array[4] | (array[5] << 8) | (array[6] << 16) | (array[7] << 24);
				LocalVarSigTok = array[8] | (array[9] << 8) | (array[10] << 16) | (array[11] << 24);
			}
		}
		public override string ToString()
		{
			return $"MethodHeaderTypeValues:\t{MethodHeaderTypeValues}\n" + $"Size:\t{Size}\n" + $"MaxStack:\t{MaxStack}\n" + $"CodeSize:\t{CodeSize}\n" + $"LocalVarSigTok:\t{LocalVarSigTok}\n";
		}
	}
}
