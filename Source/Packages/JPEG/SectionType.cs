namespace JPEG
{
    public enum SectionType:byte
    {
        SOI=0xD8,
        APP0=0xE0,
        APP1=0xE1,
        DQT=0xDB,
        SOF0=0xC0,
        DHT=0xC4,
        SOS=0xDA,
        EO=0xD9
    }
}
