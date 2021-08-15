namespace ELF
{
    public enum Program_Table_Flag:uint
    {
        PF_None = 0x0,
        PF_Exec = 0x1,
        PF_Write = 0x2,
        PF_Write_Exec = 0x3,
        PF_Read = 0x4,
        PF_Read_Exec = 0x5,
        PF_Read_Write = 0x6,
        PF_Read_Write_Exec = 0x7
    }
}