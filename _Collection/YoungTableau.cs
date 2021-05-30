using System;

namespace Collection
{
	public class YoungTableau
	{
		public List<List<int>> Values;

		public YoungTableau()
		{
			Values = new List<List<int>>();
		}

		public int Get(ref List<int> array, int value)
		{
			if (array.Length == 0)
			{
				return -1;
			}
			if (array[array.Length - 1] == value)
			{
				throw new Exception();
			}
			if (array[array.Length - 1] < value)
			{
				return -1;
			}
			if (array[0] == value)
			{
				throw new Exception();
			}
			if (array[0] > value)
			{
				return 0;
			}
			int num = 0;
			int num2 = array.Length - 1;
			while (num + 1 < num2)
			{
				int num3 = num + num2 >> 1;
				if (array[num3] == value)
				{
					throw new Exception();
				}
				if (array[num3] < value)
				{
					num = num3;
				}
				else
				{
					num2 = num3;
				}
			}
			return num2;
		}

		public void Bump(int value, int level = 0)
		{
			if (Values.Length == level)
			{
				Values.Add(new List<int>());
			}
			List<int> array = Values[level];
			int num = Get(ref array, value);
			if (num == -1)
			{
				array.Add(value);
				return;
			}
			int value2 = array[num];
			array[num] = value;
			Bump(value2, level + 1);
		}

		public void Bump(params int[] values)
		{
			foreach (int value in values)
			{
				Bump(value);
			}
		}

		public YoungTableau Transpose()
		{
			YoungTableau youngTableau = new YoungTableau();
			if (Values.Length == 0)
			{
				return youngTableau;
			}
			for (int i = 0; i < Values[0].Length; i++)
			{
				youngTableau.Values.Add(new List<int>());
			}
			for (int j = 0; j < Values.Length; j++)
			{
				List<int> list = Values[j];
				for (int k = 0; k < list.Length; k++)
				{
					youngTableau.Values[k].Add(list[k]);
				}
			}
			return youngTableau;
		}

		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < Values.Length; i++)
			{
				List<int> list = Values[i];
				for (int j = 0; j < list.Length; j++)
				{
					text = text + list[j] + "\t";
				}
				text += "\n";
			}
			return text;
		}
	}
}
