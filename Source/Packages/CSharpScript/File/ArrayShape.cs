namespace CSharpScript.File
{
	public class ArrayShape
	{
		public int Rank;
		public int NumberSizes;
		public int[] Sizes;
		public int NumerLowBound;
		public int[] LowBounds;
		public ArrayShape(byte[] bs, ref int index)
		{
			Rank = BlobHeap.ReadUnsigned(bs, ref index);
			NumberSizes = BlobHeap.ReadUnsigned(bs, ref index);
			Sizes = new int[NumberSizes];
			for (int i = 0; i < NumberSizes; i++)
				Sizes[i] = BlobHeap.ReadUnsigned(bs, ref index);
			NumerLowBound = BlobHeap.ReadUnsigned(bs, ref index);
			LowBounds = new int[NumerLowBound];
			for (int j = 0; j < NumerLowBound; j++)
				LowBounds[j] = BlobHeap.ReadUnsigned(bs, ref index);
		}
		public override string ToString() => "[".PadRight(Rank, ',') + "]";
    }
}
