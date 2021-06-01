using System;
namespace Net.Json
{
	public class StringArg
	{
		public char[] str;
		public int index;
		public int end;
		public bool NotOver;
		public StringArg(string s)
		{
			str = s.ToCharArray();
			index = 0;
			end = str.Length;
			NotOver = true;
		}
        public char Top() => str[index];
        public void Pop()
		{
			index++;
			if (index == end)
				NotOver = false;
		}
		public string EndWith(params char[] cs)
		{
			string text = "";
			while (NotOver)
			{
				for (int i = 0; i < cs.Length; i++)
					if (cs[i] == str[index])
						return text;
				text += str[index];
				Pop();
			}
			throw new Exception();
		}
		public string GetString()
		{
			string text = "";
			while (NotOver && str[index] != '"')
				Pop();
			Pop();
			if (!NotOver)
				throw new Exception();
			while (NotOver)
			{
				if (str[index] == '\\')
				{
					Pop();
					if (!NotOver)throw new Exception();
					text = str[index] == '"' ? text + "\"" : text + "\\"+str[index];
                }
				else
				{
					if (str[index] == '"')break;
					text += str[index];
				}
				Pop();
			}
			if (!NotOver)throw new Exception();
			Pop();
			return text;
		}
		public void GoTo(char c)
		{
			while (NotOver)
			{
				if (c == str[index])
					return;
				Pop();
			}
			throw new Exception();
		}
	}
}
