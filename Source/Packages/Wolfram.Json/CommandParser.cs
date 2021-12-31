using Net.Json;
using System;
using Wolfram.NETLink;

namespace Wolfram.Json
{
    public static class CommandParser
    {
        public static object Parse(Node node)
        {
            if (node is ArrayNode array) return Parse(array);
            else if (node is StringNode str)
            {
                if (str.Value.StartsWith('\'') && str.Value.EndsWith('\''))
                    return str.Value[1.. ^1];
                else return new Symbol(str.Value);
            }
            else if (node is DoubleNode integer) return integer.Value;
            else throw new ArgumentException();
        }
        public static Command Parse(ArrayNode array)
        {
            string name = (array[0] as StringNode).Value;
            object[] commands=new object[array.Nodes.Length - 1];
            for (int i = 1; i < array.Nodes.Length; i++)
                commands[i - 1] = Parse(array[i]);
            return new(name, commands);
        }
        public static Command Parse(string json)
            =>Parse(Node.Parse(json) as ArrayNode);
    }
}
