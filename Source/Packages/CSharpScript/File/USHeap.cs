using System;
using System.Text;
using Collection;
namespace CSharpScript.File
{
	public class USHeap
	{
		public AVL<int, string> Strings;
		public USHeap(byte[] bs, int length)
		{
			Strings = new AVL<int, string>();
			Strings[0] = "";
			int num;
			for (num = 1; num < length; num++)
			{
				byte b = bs[num];
				if (b == 0)
					break;
				if (b < 128)
				{
					Strings[num] = Encoding.Unicode.GetString(bs, num + 1, b - 1);
					num += b;
				}
				else
				{
					if (b >= 192)
						throw new Exception();
					int num2 = ((b & 0x3F) << 8) | bs[++num];
					Strings[num - 1] = Encoding.Unicode.GetString(bs, num + 1, num2 - 1);
					num += num2;
				}
			}
		}
        public override string ToString() => string.Join("\n", Strings);
    }
}
