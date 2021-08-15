using Collection;

namespace Remote.Module
{
	public class ModuleCollection
	{
		private TrieTree<ModuleList> Trees;
        public ServerModule GetModule(byte[] bs) => Trees[bs].Get();
		public void Register(ServerModule module)
            =>(Trees.GetNode(module.Sign).Value ??= new()).Insert(module);
        public void Disconnect() => Trees.Foreach((t) => t.Value.Disconnect());
        public ModuleCollection() => Trees = new();
    }
}
