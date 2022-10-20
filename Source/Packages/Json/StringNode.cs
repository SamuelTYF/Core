namespace Json
{
    public class StringNode:JsonNode
    {
        public string Value;
        public StringNode(string value) => Value = value;
        public override string FormatPrint(string prefix = "") => Value;
        public override string? Get() => Value;
    }
}
