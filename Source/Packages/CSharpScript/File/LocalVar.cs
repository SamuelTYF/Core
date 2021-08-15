namespace CSharpScript.File
{
	public class LocalVar
	{
		public int Index;
		public bool Pinned;
		public TypeSig Type;
		public string Name;
		public LocalVar(int index, bool pinned, TypeSig type, string name)
		{
			Index = index;
			Pinned = pinned;
			Type = type;
			Name = name;
		}
        public override string ToString() => $"[{Index}] {Type} {Name}";
    }
}
