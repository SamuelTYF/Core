using System;
using System.IO;
namespace PFile
{
    public class File<TSectionCollection>where TSectionCollection : SectionCollection,new()
    {
        public Header Header;
        public SectionHeader[] SectionHeaders;
        public TSectionCollection SectionCollection;
        public File(Stream stream)
        {
            long offset = stream.Position;
            Header = new(stream);
            if(Header.Magic!=typeof(TSectionCollection).GetHashCode())
            SectionHeaders=new SectionHeader[Header.SectionCount];
            if (offset + Header.SectionHeaderOffset != stream.Position)
                stream.Position = offset + Header.SectionHeaderOffset;
            for (int i = 0; i < Header.SectionCount; i++)
                SectionHeaders[i] = new(stream);
            SectionCollection=new TSectionCollection();
            SectionCollection.LoadParsers();
            ISection[] sections=new ISection[SectionHeaders.Length];
            for (int i = 0; i < Header.SectionCount; i++)
            {
                if (offset + SectionHeaders[i].Offset != stream.Position)
                    stream.Position = offset + SectionHeaders[i].Offset;
                sections[i] = SectionCollection.ReadSection(SectionHeaders[i], stream);
            }
        }
    }
}
