using System;
using System.Text;
using Collection;
namespace CSharpScript.File
{
	public class BlobHeap
	{
		public AVL<int, byte[]> Values;
		public AVL<int, object> ParsedValues;
		public static readonly byte[] Lower16 = new byte[16]
		{
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
			97, 98, 99, 100, 101, 102
		};
		public static readonly byte[] Upper16 = new byte[16]
		{
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
			65, 66, 67, 68, 69, 70
		};
		public BlobHeap(byte[] bs, int length)
		{
			Values = new AVL<int, byte[]>();
			ParsedValues = new();
			int num = 0;
			while (num < length)
			{
				byte b = bs[num++];
				if (b < 128)
				{
					byte[] array = new byte[b];
					Array.Copy(bs, num, array, 0, b);
					Values[num - 1] = array;
					num += b;
				}
				else if (b < 192)
				{
					byte[] array2 = new byte[((b & 0x3F) << 8) | bs[num++]];
					Array.Copy(bs, num, array2, 0, array2.Length);
					Values[num - 2] = array2;
					num += array2.Length;
				}
				else
				{
					byte[] array3 = new byte[((b & 0x1F) << 24) | (bs[num++] << 16) | (bs[num++] << 8) | bs[num++]];
					Array.Copy(bs, num, array3, 0, array3.Length);
					Values[num - 4] = array3;
					num += array3.Length;
				}
			}
		}
		public override string ToString()
		{
			string s = "";
			Values.LDR(delegate(int index, byte[] bs)
			{
				string text = s;
				object arg = index;
				string[] obj = new string[5]
				{
					string.Join(" ", bs),
					"\n",
					bs.Length != 0 ? ((Abbreviations)bs[0]).ToString() :"Empty",
					"\n",
					ParsedValues.ContainsKey(index)?ParsedValues[index].ToString():"NULL"
				};
                s = text + $"\n\n{arg:X}\n{string.Concat(obj)}";
			});
			return s;
		}
		public static int ReadUnsigned(byte[] bs, ref int index)
		{
			byte b = bs[index++];
            return b switch
            {
                > 223 => b,
                < 128 => b,
                < 192 => (b & 0x3F) << 8 | bs[index++],
                _ => (b & 0x1F) << 24 | bs[index++] << 16 | bs[index++] << 8 | bs[index++]
            };
        }
		public static string ReadString(byte[] bs, ref int index, int length)
		{
			byte[] array = new byte[length];
			Array.Copy(bs, index, array, 0, length);
			index += length;
			return Encoding.UTF8.GetString(array);
		}
		public static string GetKey(byte[] bs, bool lower = true)
		{
			byte[] array = new byte[bs.Length << 1];
			int num = 0;
			byte[] array2 = (lower ? Lower16 : Upper16);
			foreach (byte b in bs)
			{
				array[num++] = array2[b >> 4];
				array[num++] = array2[b & 0xF];
			}
			return Encoding.UTF8.GetString(array);
		}
		public static string ReadSerString(byte[] bs, ref int index)
		{
			int num = ReadUnsigned(bs, ref index);
            return num == 255 ? null : ReadString(bs, ref index, num);
        }
    }
}
