using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO
{
    public class DictionaryRecord
    {
        public byte Length;
        public byte EntendedAttributeRecordLength;
        public LB32 LocationOfExtent;
        public LB32 DataLength;
        public DateAndTime Date;
        public DictionaryRecordFlag Flag;
        public byte FileUnitSize;
        public byte InterLeaveGapSize;
        public LB16 VolumnSequenceNumber;
        public int IndentifierLength;
        public byte[] Indentifier;
        public DictionaryRecord(ISOStreamReader sr)
        {
            Length = sr.ReadByte();
            EntendedAttributeRecordLength = sr.ReadByte();
            LocationOfExtent = sr.ReadLB32();
            DataLength = sr.ReadLB32();
            Date = sr.ReadDateAndTime();
            Flag = (DictionaryRecordFlag)sr.ReadByte();
            FileUnitSize = sr.ReadByte();
            InterLeaveGapSize = sr.ReadByte();
            VolumnSequenceNumber = sr.ReadLB16();
            IndentifierLength = sr.ReadByte();
            Indentifier = sr.ReadBytes(IndentifierLength);
        }
        public override string ToString()
            => $"Length:{EntendedAttributeRecordLength}\n" +
            $"LocationOfExtent:{LocationOfExtent.Value}\n" +
            $"DataLength:{DataLength.Value}\n" +
            $"Date:{Date}\n" +
            $"Indentifier:{Encoding.UTF8.GetString(Indentifier)}\n";
    }
}
