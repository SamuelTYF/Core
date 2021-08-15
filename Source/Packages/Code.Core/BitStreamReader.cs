using System.IO;
namespace Code.Core
{
	public class BitStreamReader
	{
		public long Value;
		public byte[] Buffer;
		public int BitPosition;
		public int Position;
		public int Length;
		public Stream Stream;
		public long StreamLength;
		public BitStreamReader(Stream stream)
		{
			Value = 0L;
			Buffer = new byte[65536];
			BitPosition = 0;
			Position = 0;
			Length = 0;
			Stream = stream;
			StreamLength = stream.Length;
		}
		public bool Read(int length, out int value)
		{
			value = 0;
			if ((StreamLength - Position) * 8 + BitPosition < length)
			{
				return false;
			}
			while (BitPosition < length)
			{
				if (Position == Length)
				{
					StreamLength -= Length;
					Length = Stream.Read(Buffer, 0, 65536);
					Position = 0;
				}
				Value |= (long)((ulong)Buffer[Position++] << BitPosition);
				BitPosition += 8;
			}
			value = (int)(Value & ((1 << length) - 1));
			BitPosition -= length;
			Value >>= length;
			return true;
		}
	}
}
