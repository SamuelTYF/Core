namespace ELF
{
    public enum Section_Name:uint
    {
        SHN_UNDEF = 0x0,      /* undefined, e.g. undefined symbol */
        SHN_LORESERVE = 0xff00, /* Lower bound of reserved indices */
        SHN_LOPROC = 0xff00, /* Lower bound processor-specific index */
        SHN_BEFORE = 0xff00, /* Order section before all others (Solaris) */
        SHN_AFTER = 0xff01, /* Order section after all others (Solaris) */
        SHN_HIPROC = 0xff1f, /* Upper bound processor-specific index */
        SHN_LOOS = 0xff20, /* Lower bound OS-specific index */
        SHN_HIOS = 0xff3f, /* Upper bound OS-specific index */
        SHN_ABS = 0xfff1, /* Absolute value, not relocated */
        SHN_COMMON = 0xfff2, /* FORTRAN common or unallocated C */
        SHN_XINDEX = 0xffff, /* Index is in extra table */
        SHN_HIRESERVE = 0xffff  /* Upper bound of reserved indices */
    }
}
