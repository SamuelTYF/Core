using System;
using System.IO;
using Collection;
namespace Code.Core
{
	public class DLZW
	{
		public class Node
		{
			public int Value;
			public AVL<int, Node> Nodes;
            public Node() => Nodes = new();
        }
		public BitStreamWriter Writer;
		public BitStreamReader Reader;
		public List<byte[]> Indexes;
		public Node Dictionary;
		public Node Top;
		public int Count;
		public int Max;
		public int BitLength;
		public void StartEncode(Stream stream)
		{
			Dictionary = new Node();
			for (int i = 0; i < 256; i++)
				Dictionary.Nodes[i]=new Node
				{
					Value = i
				};
			Top = Dictionary;
			Count = 256;
			Writer = new BitStreamWriter(stream);
			Max = 512;
			BitLength = 9;
		}
		public void Encode(byte[] bs, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (Top.Nodes.ContainsKey(bs[i]))
				{
					Top = Top.Nodes[bs[i]];
					continue;
				}
				Writer.Write(Top.Value, BitLength);
				Top.Nodes[bs[i]]=new Node
				{
					Value = Count++
				};
				Top = Dictionary.Nodes[bs[i]];
				if (Count >= Max)
				{
					Max <<= 1;
					BitLength++;
				}
			}
		}
		public void EndEncode()
		{
			Writer.Write(Top.Value, BitLength);
			Top = Dictionary;
			Writer.EndWrite();
		}
		public void StartDecode(Stream stream)
		{
			Indexes = new List<byte[]>(16);
			for (int i = 0; i < 256; i++)
				Indexes.Add(new byte[1] { (byte)i });
			Reader = new BitStreamReader(stream);
			Count = 256;
			Max = 512;
			BitLength = 9;
		}
		public void Decode(Stream stream)
		{
			byte[] last = new byte[0];
			while (Reader.Read(BitLength, out int code))
			{
				byte[] r = new byte[last.Length + 1];
				Array.Copy(last, 0, r, 0, last.Length);
				if (r.Length > 1)Indexes.Add(r);
				if (Indexes.Length + 1 >= Max)
				{
					Max <<= 1;
					BitLength++;
				}
				byte[] now = Indexes[code];
				r[last.Length] = now[0];
				stream.Write(now, 0, now.Length);
				last = now;
			}
		}
		public static void Encode(Stream input, Stream output)
		{
			DLZW lzw = new();
			lzw.StartEncode(output);
			while (input.Position < input.Length)
			{
				byte[] bs = new byte[65536];
				int i = input.Read(bs, 0, bs.Length);
				lzw.Encode(bs, i);
			}
			lzw.EndEncode();
		}
		public static void Decode(Stream input, Stream output)
		{
			DLZW dLZW = new();
			dLZW.StartDecode(input);
			dLZW.Decode(output);
		}
	}
}
