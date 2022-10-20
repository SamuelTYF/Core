namespace Json
{
    public class NullNode:JsonNode
    {
        public NullNode() { }
        public override string FormatPrint(string prefix = "") => "null";
    }
}
