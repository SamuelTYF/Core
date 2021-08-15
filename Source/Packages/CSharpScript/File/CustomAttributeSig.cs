using System;
namespace CSharpScript.File
{
	public class CustomAttributeSig
	{
		public FixedArg[] FixedArgs;
		public NamedArg[] NamedArgs;
		public CustomAttributeSig(byte[] bs, ref int index, MethodDefSig method, MetadataRoot metadata, ref bool Success)
		{
			if (method == null)
				throw new Exception();
			if (bs[index++] != 1 || bs[index++] != 0)
				throw new Exception();
			FixedArgs = new FixedArg[method.ParamCount];
			int num = 0;
			while (Success && num < method.ParamCount)
			{
				FixedArgs[num] = new FixedArg(bs, ref index, method.ParamTypes[num], metadata, ref Success);
				num++;
			}
			if (Success)
			{
				int num2 = bs[index++] | (bs[index++] << 8);
				NamedArgs = new NamedArg[num2];
				int num3 = 0;
				while (Success && num3 < num2)
				{
					NamedArgs[num3] = new NamedArg(bs, ref index, metadata, ref Success);
					num3++;
				}
				if (Success && bs.Length != index)
					throw new Exception();
			}
		}
	}
}
