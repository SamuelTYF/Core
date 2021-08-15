using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFile.Pack
{
    public class PackSectionCollection : SectionCollection
    {
        public PackHeader Header;
        public PackManifest Manifest;
        public override void LoadParsers()
        {
            Parsers[(int)PackMagics.Header] = (header, stream) => new PackHeader(header, stream);
            Parsers[(int)PackMagics.Manifest] = (header, stream) => new PackManifest(header, stream);
        }
    }
}
