namespace PE
{
	public struct RVASize
	{
		public int RVA;
		public int Size;
		public RVASize(int rva, int size)
		{
			RVA = rva;
			Size = size;
		}
		public static implicit operator RVASize(long value)
		{
			return new RVASize((int)value, (int)(value >> 32));
		}
		public override string ToString()
		{
			return $"{RVA:X}[{Size:X}]";
		}
		public void WriteToStream(byte[] bs, int index)
		{
			bs[index++] = (byte)((uint)RVA & 0xFu);
			bs[index++] = (byte)((RVA & 0xF0) >> 8);
			bs[index++] = (byte)((RVA & 0xF) >> 16);
			bs[index++] = (byte)((RVA & 0xF) >> 24);
			bs[index++] = (byte)((uint)Size & 0xFu);
			bs[index++] = (byte)((Size & 0xF0) >> 8);
			bs[index++] = (byte)((Size & 0xF) >> 16);
			bs[index] = (byte)((Size & 0xF) >> 24);
		}
	}
}
