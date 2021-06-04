using System;
namespace CSharpScript.File
{
	public class GUIDHeap
	{
		public Guid[] Guids;
		public Guid this[int index] => (index == 0) ? Guid.Empty : Guids[index - 1];
		public GUIDHeap(byte[] bs)
		{
			int num = bs.Length >> 4;
			Guids = new Guid[num];
			for (int i = 0; i < num; i++)
			{
				byte[] array = new byte[16];
				Array.Copy(bs, i << 4, array, 0, 16);
				Guids[i] = new Guid(array);
			}
		}
		public override string ToString()
		{
			return string.Join("\n", Guids);
		}
	}
}
