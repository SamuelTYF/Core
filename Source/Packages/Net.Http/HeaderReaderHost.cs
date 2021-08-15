using System.Text;
using Automata;
using Collection;
namespace Net.Http
{
	public sealed class HeaderReaderHost : AutomataHost
	{
		public TrieTree<byte> EscapeTable;
		public List<byte> TempBytes;
		public List<byte> EscapeBytes;
		public string Key;
		public Pre_Header Pre_Header;
		public Header_URL URL;
		public HeaderReaderHost()
		{
			EscapeTable = new TrieTree<byte>();
			"0123456789ABCDEF".ToCharArray();
			byte b = 0;
			int[] system = AutomataKernel.System16;
			for (int i = 0; i < system.Length; i++)
			{
				char c = (char)system[i];
				int[] system2 = AutomataKernel.System16;
				for (int j = 0; j < system2.Length; j++)
				{
					char c2 = (char)system2[j];
					EscapeTable[c.ToString() + c2, 0] = b++;
				}
			}
		}
		public void Init()
		{
			TempBytes = new List<byte>();
			EscapeBytes = new List<byte>();
			Pre_Header = new Pre_Header();
			URL = (Pre_Header.URL = new Header_URL());
		}
        public void Push() => TempBytes.Add((byte)Input);
        public void PushEscape() => EscapeBytes.Add((byte)Input);
        public void StoreEscape()
		{
			PushEscape();
			TempBytes.Add(EscapeTable[TempBytes.ToArray(), 0]);
			TempBytes.Clear();
		}
		public void StoreMode()
		{
			Pre_Header.Mode = Encoding.UTF8.GetString(TempBytes.ToArray());
			TempBytes.Clear();
		}
		public void StorePath()
		{
			if (URL.Path == null)
			{
				URL.Path = new List<string>();
				return;
			}
			URL.Path.Add(Encoding.UTF8.GetString(TempBytes.ToArray()));
			TempBytes.Clear();
		}
		public void StoreURLKey()
		{
			Key = Encoding.UTF8.GetString(TempBytes.ToArray());
			TempBytes.Clear();
		}
		public void StoreURLValue()
		{
			if (Key != null)
			{
				URL.Values[Key, 0] = Encoding.UTF8.GetString(TempBytes.ToArray());
				Key = null;
			}
			TempBytes.Clear();
		}
		public void StoreHTTP()
		{
			Pre_Header.HTTP = Encoding.UTF8.GetString(TempBytes.ToArray());
			TempBytes.Clear();
		}
		public void StoreKey()
		{
			Key = Encoding.UTF8.GetString(TempBytes.ToArray());
			TempBytes.Clear();
		}
		public void StoreKeyPair()
		{
			Pre_Header.Values[Key, 0] = Encoding.UTF8.GetString(TempBytes.ToArray());
			TempBytes.Clear();
		}
		public override HashTable<Function> MarkFunctions()
		{
			HashTable<Function> hashTable = new();
			hashTable.Register(Throw, "Throw".GetHashCode());
			hashTable.Register(Init, "Init".GetHashCode());
			hashTable.Register(Push, "Push".GetHashCode());
			hashTable.Register(PushEscape, "PushEscape".GetHashCode());
			hashTable.Register(StoreEscape, "StoreEscape".GetHashCode());
			hashTable.Register(StoreMode, "StoreMode".GetHashCode());
			hashTable.Register(StorePath, "StorePath".GetHashCode());
			hashTable.Register(StoreURLKey, "StoreURLKey".GetHashCode());
			hashTable.Register(StoreURLValue, "StoreURLValue".GetHashCode());
			hashTable.Register(StoreHTTP, "StoreHTTP".GetHashCode());
			hashTable.Register(StoreKey, "StoreKey".GetHashCode());
			hashTable.Register(StoreKeyPair, "StoreKeyPair".GetHashCode());
			return hashTable;
		}
	}
}
