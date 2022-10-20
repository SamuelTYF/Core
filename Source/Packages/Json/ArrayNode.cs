namespace Json
{
    public class ArrayNode:JsonNode
    {
        public List<JsonNode> Values;
        public ArrayNode() => Values = new();
        public override JsonNode? this[int index] => Values[index];
        public override string FormatPrint(string prefix = "")
        {
            if (Values.Count == 0) return "[]";
            else if (Values.Count == 1) return $"[{Values[0].FormatPrint(prefix + "\n")}]";
            else
            {
                string p = prefix + "\t";
                return "[" +
                    string.Join(",", Values.Select(value =>
                    $"\n{p}{value.FormatPrint(p)}"))
                    + "\n"+prefix+"]";
            }
        }
    }
}
