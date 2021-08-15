using Collection;
using System;
using System.Text.RegularExpressions;

namespace FileParserBuilder
{
    class Program
    {
        static readonly Regex _R = new("public ([^ ]*) ([^;]*);");
        static void Main()
        {
            List<string> ss = new();
            int index = 0;
            while(true)
            {
                string s = Console.ReadLine();
                if (s.Length == 0) break;
                else
                {
                    Match m= _R.Match(s);
                    if (!m.Success) throw new Exception();
                    ss.Add(m.Groups[1].Value switch
                    {
                        "uint"=>$"{m.Groups[2].Value}=BitConverter.ToUInt32(bs,{(index+=4)-4});",
                        "ushort"=> $"{m.Groups[2].Value}=BitConverter.ToUInt16(bs,{(index += 2) - 2});",
                        "int" => $"{m.Groups[2].Value}=BitConverter.ToInt32(bs,{(index += 4) - 4});",
                        "long" => $"{m.Groups[2].Value}=BitConverter.ToInt64(bs,{(index += 8) - 8});",
                        "byte" =>$"{m.Groups[2].Value}=bs[{index++}];",
                        _=>throw new Exception()
                    });
                }
            }
            Console.Clear();
            Console.WriteLine($"byte[] bs=new byte[{index}];");
            Console.WriteLine($"stream.Read(bs,0,{index});");
            foreach (string s in ss) Console.WriteLine(s);
        }
    }
}
