namespace ELF
{
    public enum ELF_Type:ushort
    {
        ET_NONE     = 0x0,
        ET_REL      = 0x1,
        ET_EXEC     = 0x2,
        ET_DYN      = 0x3,
        ET_CORE     = 0x4,
        ET_LOOS     = 0xfe00,
        ET_HIOS     = 0xfeff,
        ET_LOPROC   = 0xff00,
        ET_HIPROC   = 0xffff
    }
}