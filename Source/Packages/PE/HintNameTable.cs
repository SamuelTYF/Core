using System;
using System.IO;
namespace PE
{
    public class HintNameTable
    {
        public int Index;
        public short Hint;
        public string Name;
        public HintNameTable(int index,Stream stream)
        {
            Index = index;
            byte[] bs = new byte[2];
            stream.Read(bs, 0, 2);
            Hint = BitConverter.ToInt16(bs);
            Name = stream.GetString(stream.Position);
        }
    }
}
