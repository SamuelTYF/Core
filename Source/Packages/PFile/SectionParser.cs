using System.IO;
namespace PFile
{
    public delegate ISection SectionParser(SectionHeader header,Stream stream);
}
