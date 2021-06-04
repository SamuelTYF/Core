using System.Text;
using Collection;
namespace CSharpScript.File
{
	public class StringsHeap
	{
		public byte[] BS;
		public AVL<int, string> Strings;
		public string this[int index]
		{
			get
			{
				for (int i = index; i < BS.Length; i++)
				{
					if (BS[i] == 0)
						return Encoding.UTF8.GetString(BS, index, i - index);
				}
				return Encoding.UTF8.GetString(BS, index, BS.Length - index);
			}
		}
		public StringsHeap(byte[] bs, int length)
		{
			BS = bs;
			Strings = new AVL<int, string>();
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				if (bs[i] == 0)
				{
					Strings[num] = Encoding.UTF8.GetString(bs, num, i - num);
					num = i + 1;
				}
			}
			if (num != length)
				Strings[num] = Encoding.UTF8.GetString(bs, num, length - num);
		}
		public override string ToString()
		{
			string s = "";
			Strings.LDR(delegate(int index, string value)
			{
				s += $"{index:X}\t{value}\n";
			});
			return s;
		}
	}
}
