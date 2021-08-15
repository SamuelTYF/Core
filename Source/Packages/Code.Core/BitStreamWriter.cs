using System.IO;
namespace Code.Core
{
	public class BitStreamWriter
	{
		public long Value;
		public byte[] Buffer;
		public int Position;
		public int BitPosition;
		public Stream Stream;
		public BitStreamWriter(Stream stream)
		{
			Value = 0L;
			Buffer = new byte[65536];
			Position = 0;
			BitPosition = 0;
			Stream = stream;
		}
		public void Write(int value, int length)
		{
			Value |= (long)value << BitPosition;
			BitPosition += length;
			while (BitPosition >= 8)
			{
				Buffer[Position++] = (byte)(Value & 0xFF);
				if (Position == 65536)
				{
					Stream.Write(Buffer, 0, Position);
					Position = 0;
				}
				Value >>= 8;
				BitPosition -= 8;
			}
		}
		public void EndWrite()
		{
			Buffer[Position++] = (byte)Value;
			Stream.Write(Buffer, 0, Position);
			Position = 0;
			Buffer = null;
		}
	}
}
