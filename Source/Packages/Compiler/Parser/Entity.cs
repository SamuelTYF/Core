namespace Compiler.Parser
{
    public class Entity
    {
    }
    public class Entity_Push:Entity
    {
        public int Index;
        public Entity_Push(int index) => Index = index;
        public override string ToString() => $"Push {Index}";
    }
    public class Entity_Reduce : Entity
    {
        public Delta Delta;
        public Entity_Reduce(Delta delta) => Delta = delta;
        public override string ToString() => $"Reduce {Delta}";
    }
}
