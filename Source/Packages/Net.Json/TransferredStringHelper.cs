namespace Net.Json
{
	public static class TransferredStringHelper
	{
		private delegate void CharTramsferredFunction(string str, ref string s, ref int index);
		private delegate void CharSourceFunction(char c, ref string s);
		private delegate int GetInt();
		private static GetInt[] GetInts;
		private static CharTramsferredFunction[] TramsferredFunctions;
		private static CharSourceFunction[] SourceFunctions;
		private static void BuildTramsferredFunctions()
		{
			if (TramsferredFunctions != null)return;
			GetInts = new GetInt[256];
			GetInts[48] = () => 0;
			GetInts[49] = () => 1;
			GetInts[50] = () => 2;
			GetInts[51] = () => 3;
			GetInts[52] = () => 4;
			GetInts[53] = () => 5;
			GetInts[54] = () => 6;
			GetInts[55] = () => 7;
			GetInts[56] = () => 8;
			GetInts[57] = () => 9;
			GetInts[97] = () => 10;
			GetInts[98] = () => 11;
			GetInts[99] = () => 12;
			GetInts[100] = () => 13;
			GetInts[101] = () => 14;
			GetInts[102] = () => 15;
			TramsferredFunctions = new CharTramsferredFunction[256];
			TramsferredFunctions[97] = delegate(string str, ref string s, ref int index)
			{
				s += "\a";
			};
			TramsferredFunctions[98] = delegate(string str, ref string s, ref int index)
			{
				s += "\b";
			};
			TramsferredFunctions[102] = delegate(string str, ref string s, ref int index)
			{
				s += "\f";
			};
			TramsferredFunctions[110] = delegate(string str, ref string s, ref int index)
			{
				s += "\n";
			};
			TramsferredFunctions[114] = delegate(string str, ref string s, ref int index)
			{
				s += "\r";
			};
			TramsferredFunctions[116] = delegate(string str, ref string s, ref int index)
			{
				s += "\t";
			};
			TramsferredFunctions[118] = delegate(string str, ref string s, ref int index)
			{
				s += "\v";
			};
			TramsferredFunctions[92] = delegate(string str, ref string s, ref int index)
			{
				s += "\\";
			};
			TramsferredFunctions[47] = delegate(string str, ref string s, ref int index)
			{
				s += "/";
			};
			TramsferredFunctions[39] = delegate(string str, ref string s, ref int index)
			{
				s += "'";
			};
			TramsferredFunctions[34] = delegate(string str, ref string s, ref int index)
			{
				s += "\"";
			};
			TramsferredFunctions[48] = delegate(string str, ref string s, ref int index)
			{
				s += "\0";
			};
			TramsferredFunctions[117] = delegate(string str, ref string s, ref int index)
			{
				int num = 0;
				for (int i = 0; i < 4; i++)
				{
					if (index >= str.Length)
						break;
					num = (num << 4) | GetInts[str[++index]]();
				}
				s += (char)num;
			};
		}
		public static string ToTransferredString(this string str)
		{
			BuildTramsferredFunctions();
			string s = "";
			for (int i = 0; i < str.Length; i++)
				if (str[i] == '\\' && ++i < str.Length)
					TramsferredFunctions[str[i]](str, ref s, ref i);
				else s += str[i];
			return s;
		}
		public static void BuildSourceFunctions()
        {
			if (SourceFunctions != null) return;
			SourceFunctions = new CharSourceFunction[256];
			for (int i = 0; i < 256; i++)
				SourceFunctions[i] = delegate (char c, ref string s)
				  {
					  s += c;
				  };
			SourceFunctions['\\'] = delegate (char c, ref string s)
			  {
				  s += "\\\\";
			  };
		}
		public static string ToSourceString(this string str)
        {
			BuildSourceFunctions();
			string s = "";
			for (int i = 0; i < str.Length; i++)
				if (str[i]<256)
					SourceFunctions[str[i]](str[i], ref s);
				else s += str[i];
			return s;
		}
	}
}
