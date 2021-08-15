using Collection;
using System.IO;

namespace PFile
{
    public abstract class SectionCollection
    {
        public AVL<int, ISection> Sections;
        public AVL<int, SectionParser> Parsers;
        public SectionCollection()
        {
            Sections = new();
            Parsers = new();
        }
        public void Register(ISection section)=>Sections[section.Header.Magic]=section;
        public ISection Get(int magic) => Sections[magic];
        public abstract void LoadParsers();
        public ISection ReadSection(SectionHeader header, Stream stream)
        {
            ISection section = Parsers[header.Magic](header, stream);
            Sections[header.Magic] = section;
            return section;
        }
    }
}
