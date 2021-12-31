namespace ISO
{
    public class VolumeRoot
    {
        public LB32 VolumnSpaceSize;
        public byte[] EscSeq;
        public LB16 VolumnSetSize;
        public LB16 VolumnSequenceNumber;
        public LB16 LogicalBlockSize;
        public LB32 PathTableSize;
        public int LocationOfPathTable;
        public int LocationOfOptionalPathTable;
        public int LocPathTable;
        public int LocOptPathTable;
        public DictionaryRecord Root;
        public VolumeRoot(ISOStreamReader sr)
        {
            VolumnSpaceSize = sr.ReadLB32();
            EscSeq = sr.ReadBytes(32);
            VolumnSetSize = sr.ReadLB16();
            VolumnSequenceNumber = sr.ReadLB16();
            LogicalBlockSize = sr.ReadLB16();
            PathTableSize = sr.ReadLB32();
            LocationOfPathTable = sr.ReadInt32();
            LocationOfOptionalPathTable = sr.ReadInt32();
            LocPathTable= sr.ReadInt32();
            LocOptPathTable=sr.ReadInt32();
            Root = new(sr);
        }
        public override string ToString()
            => $"VolumnSpaceSize:{VolumnSpaceSize.Value}\n" +
            $"VolumnSetSize:{VolumnSetSize.Value}\n" +
            $"VolumnSequenceNumber:{VolumnSequenceNumber.Value}\n" +
            $"LogicalBlockSize:{LogicalBlockSize.Value}\n" +
            $"PathTableSize:{PathTableSize.Value}\n" +
            $"LocationOfPathTable:{LocationOfPathTable}\n" +
            $"LocationOfOptionalPathTable:{LocationOfOptionalPathTable}\n" +
            $"LocPathTable:{LocPathTable}\n" +
            $"LocOptPathTable:{LocOptPathTable}\n";
    }
}
