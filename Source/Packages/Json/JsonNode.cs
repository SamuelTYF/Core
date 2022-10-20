using Json.Grammar;
using System.Text;

namespace Json
{
    public abstract class JsonNode
    {
        public static readonly NullNode Null = new();
        public static readonly BooleanNode True = new(true);
        public static readonly BooleanNode False = new(false);
        public virtual JsonNode? this[int index] => null;
        public virtual JsonNode? this[string key] => null;
        public JsonNode()
        {

        }
        public static JsonNode Parse(string json)
        {
            using MemoryStream ms = new(Encoding.UTF8.GetBytes(json));
            Tokenizer tokenizer = new();
            tokenizer.StartParse(ms);
            Parser parser = new();
            return parser.Parse(tokenizer);
        }
        public virtual string FormatPrint(string prefix="")
            => throw new NotImplementedException();
        public virtual T? Get<T>()where T:struct
            => default(T?);
        public virtual string? Get() => null;
    }
}
