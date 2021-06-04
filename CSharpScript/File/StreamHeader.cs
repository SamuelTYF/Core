using System.IO;
using System.Text;
namespace CSharpScript.File
{
	public class StreamHeader
	{
		public int Offset;
		public int Size;
		public string Name;
		public StreamHeader(Stream stream)
		{
			byte[] array = new byte[8];
			stream.Read(array, 0, 8);
			Offset = array[0] | (array[1] << 8) | (array[2] << 16) | (array[3] << 24);
			Size = array[4] | (array[5] << 8) | (array[6] << 16) | (array[7] << 24);
			StringBuilder stringBuilder = new StringBuilder(32);
			for (int i = 0; i < 32; i++)
			{
				int num = stream.ReadByte();
				if (num == 0)
				{
					array = new byte[3 - (i & 3)];
					stream.Read(array, 0, array.Length);
					break;
				}
				stringBuilder.Append((char)num);
			}
			Name = stringBuilder.ToString();
		}
		public override string ToString()
		{
			return $"Offset:\t{Offset:X}\n" + $"Size:\t{Size:X}\n" + "Name:\t" + Name + "\n";
		}
	}
}
