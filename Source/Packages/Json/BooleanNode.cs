namespace Json
{
    public class BooleanNode:JsonNode
    {
        public bool Value;
        public BooleanNode(bool value) => Value = value;
        public override string FormatPrint(string prefix = "") => Value ? "true" : "false";
        public override T? Get<T>()
            => Value is T t ? t : default(T?);
    }
}
