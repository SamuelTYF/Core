namespace ELF
{
    public enum Dynamic_Symbol_Info_Bind:byte
    {
        STB_LOCAL = 0x0,
        STB_GLOBAL = 0x1,
        STB_WEAK = 0x2,
        STB_NUM = 0x3,
        STB_LOOS = 0xA,
        STB_GNU_UNIQUE = 0xA,
        STB_HIOS = 0xC,
        STB_LOPROC = 0xD,
        STB_HIPROC = 0xE,
        STB_UNKNOWN = 0xF
    }
    public enum Dynamic_Symbol_Info_Type : byte
    {
        STT_NOTYPE = 0x0,
        STT_OBJECT = 0x1,
        STT_FUNC = 0x2,
        STT_SECTION = 0x3,
        STT_FILE = 0x4,
        STT_COMMON = 0x5,
        STT_TLS = 0x6,
        STT_NUM = 0x7,
        STT_LOOS = 0xA,
        STT_GNU_IFUNC = 0xA,
        STT_HIOS = 0xB,
        STT_LOPROC = 0xC,
        STT_HIPROC = 0xD
    }
}