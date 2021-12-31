using Collection;
using System.IO;
using System.Text;

namespace Net.Http
{
    public class Response
    {
        public HttpState State;
        public TrieTree<string> Values;
        public byte[] Data;
        public Response()
        {
            Values = new();
            Data = new byte[0];
        }
        public byte[] GetBytes()
        {
            using MemoryStream ms = new();
            byte[] bs = Encoding.UTF8.GetBytes($"HTTP/1.1 {(int)State} {State}\r\n");
            ms.Write(bs, 0, bs.Length);
            Values.Foreach((key, value) =>
            {
                bs = Encoding.UTF8.GetBytes($"{key}: {value.Value}\r\n");
                ms.Write(bs, 0, bs.Length);
            });
            bs = Encoding.UTF8.GetBytes("\r\n");
            ms.Write(bs, 0, bs.Length);
            if (Data!=null)
                ms.Write(Data, 0, Data.Length);
            ms.Position = 0;
            return ms.ToArray();
        }
    }
}
