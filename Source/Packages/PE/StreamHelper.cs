using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE
{
    public static class StreamHelper
    {
        public static string GetString(this Stream stream, long offset)
        {
            long p = stream.Position;
            stream.Position = offset;
            List<byte> bs = new();
            while (stream.Position < stream.Length)
            {
                byte t = (byte)stream.ReadByte();
                if (t == 0) break;
                else bs.Add(t);
            }
            stream.Position = p;
            return Encoding.UTF8.GetString(bs.ToArray());
        }
    }
}
