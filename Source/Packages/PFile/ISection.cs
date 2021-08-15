namespace PFile
{
    public abstract class ISection
    {
        public SectionHeader Header;
        public ISection(SectionHeader header) => Header = header;
        public abstract void UpdateHeader(SectionCollection sections);
    }
}
