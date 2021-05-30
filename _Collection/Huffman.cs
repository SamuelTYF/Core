using System.Collections.Generic;
using System.IO;

namespace Collection
{
	public class Huffman
	{
		public HuffmanNode Top;

		public HuffmanNode[] Nodes;

		public Huffman(int[] counts)
		{
			Nodes = new HuffmanNode[counts.Length];
			Heap<HuffmanNode> heap = new Heap<HuffmanNode>(-1);
			for (int i = 0; i < counts.Length; i++)
			{
				if (counts[i] != 0)
				{
					heap.Insert(Nodes[i] = new HuffmanNode(i, counts[i]));
				}
			}
			while (heap.Length > 1)
			{
				heap.Insert(new HuffmanNode(heap.Pop(), heap.Pop()));
			}
			Top = heap.Pop();
			Top.Lock();
		}

		public static Huffman ReadFrom(Stream stream)
		{
			int[] array = new int[256];
			byte[] array2 = new byte[67108864];
			int num;
			while ((num = stream.Read(array2, 0, 67108864)) != 0)
			{
				for (int i = 0; i < num; i++)
				{
					array[array2[i]]++;
				}
			}
			return new Huffman(array);
		}

		public override string ToString()
		{
			return string.Join("\n", (IEnumerable<HuffmanNode>)Nodes);
		}
	}
}
