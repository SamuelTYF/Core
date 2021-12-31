namespace ISO
{
    public class SupplementaryVolumeDescriptor
    {
        public byte Flags;
        public byte[] SystemId;
        public byte[] VolumeId;
        public byte[] Unused72;
        public VolumeRoot VolumeRoot;
        public byte[] VolumeSetId;
        public byte[] PublisherId;
        public byte[] DataPreparerId;
        public byte[] ApplicationId;
        public byte[] CopyrightFileId;
        public byte[] AbstractFileId;
        public byte[] BibliographicFileId;
        public DigitsZonedDateTime CreateTime;
        public DigitsZonedDateTime ModifyTime;
        public DigitsZonedDateTime ExpireTime;
        public DigitsZonedDateTime EffectiveTime;
        public byte FileStructureVersion;
        public byte[] ApplicationUse;
        public static SupplementaryVolumeDescriptor Parse(byte[] value)
        {
            using MemoryStream ms = new(value);
            return new(new ISOStreamReader(ms));
        }
        public SupplementaryVolumeDescriptor(ISOStreamReader sr)
        {
            Flags = sr.ReadByte();
            SystemId = sr.ReadBytes(32);
            VolumeId = sr.ReadBytes(32);
            Unused72 = sr.ReadBytes(8);
            VolumeRoot = new(sr);
            VolumeSetId = sr.ReadBytes(128);
            PublisherId = sr.ReadBytes(128);
            DataPreparerId = sr.ReadBytes(128);
            ApplicationId = sr.ReadBytes(128);
            CopyrightFileId = sr.ReadBytes(37);
            AbstractFileId = sr.ReadBytes(37);
            BibliographicFileId = sr.ReadBytes(37);
            CreateTime = sr.ReadDigitsZonedDateTime();
            ModifyTime = sr.ReadDigitsZonedDateTime();
            ExpireTime = sr.ReadDigitsZonedDateTime();
            EffectiveTime = sr.ReadDigitsZonedDateTime();
            FileStructureVersion = sr.ReadByte();
            ApplicationUse = sr.ReadBytes(512);
        }
    }
}
