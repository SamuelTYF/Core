namespace Json
{
    public class DoubleNode:JsonNode
    {
        public double Value;
        public DoubleNode(double value) => Value = value;
        public override string ToString() => Value.ToString();
        public override T? Get<T>()
            => Value is T t ? t : default(T?);
    }
}
