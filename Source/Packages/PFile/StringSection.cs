using Collection;
using System.IO;
using System.Text;

namespace PFile
{
    public class StringSection : ISection
    {
        public AVL<long, string> Values;
        public long Length;
        public int Magic;
        public StringSection(int magic):base(null)
        {
            Values = new();
            Magic = magic;
        }
        public StringSection(SectionHeader header, Stream stream):base(header)
        {
            byte[] bs = new byte[header.Length];
            stream.Read(bs, 0, bs.Length);
            Values = new();
            List<byte> temp = new();
            int index = 0;
            while (index < bs.Length)
                if (bs[index] == 0)
                {
                    Values[index - temp.Length] = Encoding.UTF8.GetString(temp.ToArray());
                    temp.Clear();
                }
                else temp.Add(bs[index]);
            Values[index - temp.Length] = Encoding.UTF8.GetString(temp.ToArray());
            Magic = header.Magic;
            Length = bs.Length;
        }
        public override void UpdateHeader(SectionCollection sections) { }
        public long Store(string value)
        {
            long index = Length;
            Values[index] = value;
            Length += value.Length + 1;
            return index;
        }
        public string Get(long index) => Values[index]; 
        public void GetHeader(long offset) => Header = new(offset, Length, Magic);
    }
}
