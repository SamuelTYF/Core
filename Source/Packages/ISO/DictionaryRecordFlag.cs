namespace ISO
{
    [Flags]
    public enum DictionaryRecordFlag:byte
    {
        Hidden = 1,
        Directory = 2,
        AssociatedFile = 4,
        Record = 8,
        Protection = 16,
        Reserved5 = 32,
        Reserved6 = 64,
        Multi_Extent = 128,
    }
}
