namespace Json
{
    public class IntNode:JsonNode
    {
        public int Value;
        public IntNode(int value) => Value = value;
        public override string FormatPrint(string prefix = "") => Value.ToString();
        public override T? Get<T>()
            => Value is T t ? t : default(T?);
    }
}
