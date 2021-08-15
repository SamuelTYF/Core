using System;
using System.IO;
namespace PFile.Pack
{
    public class PackHeader:ISection
    {
        public int Version;
        public long Manifast_Offset;
        public int Manifast_Size;
        public long Info_Offset;
        public int Info_Size;
        public PackHeader(SectionHeader header,Stream stream):base(header)
        {
            if (header.Length != 28) throw new Exception();
            byte[] bs=new byte[28];
            stream.Read(bs,0,28);
            Version=BitConverter.ToInt32(bs,0);
            Manifast_Offset=BitConverter.ToInt64(bs,4);
            Manifast_Size=BitConverter.ToInt32(bs,12);
            Info_Offset=BitConverter.ToInt64(bs,16);
            Info_Size=BitConverter.ToInt32(bs,24);
        }
        public override void UpdateHeader(SectionCollection sections)
        {
            //Header = new(offset, 28, (int)PackMagics.Header);
        }
    }
}