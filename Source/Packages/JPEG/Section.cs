using Collection;
using System;
using System.IO;

namespace JPEG
{
    public delegate Section SectionBuild();
    public abstract class Section
    {
        public static readonly SectionBuild[] Build;
        static Section()
        {
            Build = new SectionBuild[256];
            Build[(byte)SectionType.SOI] = () => new SOI();
            Build[(byte)SectionType.APP0] = () => new APP0();
            for (int i = 0; i < 15; i++)
                Build[(byte)((byte)SectionType.APP1 + i)] = () => new APPn();
            Build[(byte)SectionType.DQT] = () => new DQT();
            Build[(byte)SectionType.SOF0] = () => new SOF0();
            Build[(byte)SectionType.DHT]=() => new DHT();
            Build[(byte)SectionType.SOS]= () => new SOS();
        }
        public static Section GetSection(Stream stream)
        {
            byte[] bs = new byte[2];
            stream.Read(bs, 0, 2);
            if(bs[0] != 0xFF)throw new Exception();
            Section section = Build[bs[1]]();
            section.Sign = (SectionType)bs[1];
            section.Update(stream);
            return section;
        }
        public SectionType Sign { get;private set; }
        public abstract void Update(Stream stream);
    }
}
