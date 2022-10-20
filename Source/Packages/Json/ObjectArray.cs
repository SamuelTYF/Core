namespace Json
{
    public class ObjectNode:JsonNode
    {
        public Dictionary<string, JsonNode> Values;
        public override JsonNode? this[string key] => Values[key];
        public ObjectNode() => Values = new();
        public override string FormatPrint(string prefix = "")
        {
            if (Values.Count == 0) return "{}";
            else
            {
                string p = prefix + "\t";
                return "{" +
                    string.Join(",", Values.Select(pair =>
                    $"\n{p}{pair.Key} : {pair.Value.FormatPrint(p)}"))
                    +"\n"+prefix+"}";
            }
        }
    }
}
