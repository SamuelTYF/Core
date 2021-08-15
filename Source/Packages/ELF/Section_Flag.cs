namespace ELF
{
    public enum Section_Flag:uint
    {
        SF32_None = 0x0,
        SF32_Exec = 0x1,
        SF32_Alloc = 0x2,
        SF32_Alloc_Exec = 0x3,
        SF32_Write = 0x4,
        SF32_Write_Exec = 0x5,
        SF32_Write_Alloc = 0x6,
        SF32_Write_Alloc_Exec = 0x7
    }
}
