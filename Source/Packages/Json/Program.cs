namespace Json
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using StreamReader sr = new(@"D:\Core\Source\Packages\ParserGenerator\parser-schema");
            //string json = sr.ReadToEnd();
            string json = "[1\n,\nnull\n,\nfalse\n,\ntrue\n,\"123\"]";
            Console.WriteLine(json);

            JsonNode node= JsonNode.Parse(json);
            Console.WriteLine(node.FormatPrint());
            int? s = node[0]?.Get<int>();
            Console.WriteLine(s);
        }
    }
}
