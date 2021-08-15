using System.Text;
using Automata;
using Collection;
namespace Net.Http
{
	public class CookieHost : AutomataHost
	{
		public TrieTree<string> Values;
		public List<byte> TempBytes;
		public string Key;
		public void Init()
		{
			Values = new TrieTree<string>();
			TempBytes = new List<byte>();
		}
        public void Push() => TempBytes.Add((byte)Input);
        public void StoreKey()
		{
			Key = Encoding.UTF8.GetString(TempBytes.ToArray());
			TempBytes.Clear();
		}
		public void StoreValue()
		{
			Values[Key, 0] = Encoding.UTF8.GetString(TempBytes.ToArray());
			TempBytes.Clear();
		}
		public override HashTable<Function> MarkFunctions()
		{
			HashTable<Function> hashTable = new();
			hashTable.Register(Throw, "Throw".GetHashCode());
			hashTable.Register(Init, "Init".GetHashCode());
			hashTable.Register(Push, "Push".GetHashCode());
			hashTable.Register(StoreKey, "StoreKey".GetHashCode());
			hashTable.Register(StoreValue, "StoreValue".GetHashCode());
			return hashTable;
		}
	}
}
