using System;
namespace Automata
{
    public class ByteArg : IStringArg
    {
        public byte[] bytes;
        public int index;
        public int end;
        public bool NotOver { get; private set; }
        public ByteArg(byte[] bs)
        {
            bytes = bs;
            index = 0;
            end = bytes.Length;
            NotOver = true;
        }
        public char Top() => (char)bytes[index];
        public void Pop() => NotOver = ++index != end;
        public byte[] Read(int length)
        {
            byte[] array = new byte[length];
            Array.Copy(bytes, index, array, 0, length);
            return array;
        }
        public char Last() => (char)bytes[index - 1];
    }
}
