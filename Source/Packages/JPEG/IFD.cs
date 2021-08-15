using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEG
{
    public class IFD
    {
        public IFDEntry[] Entries;
        public IFD SubIFD;
        public int NextIFDOffset;
        public IFD(byte[] bs,int offset)
        {
            int count = BitConverter.ToInt16(bs, offset + 6);
            Entries = new IFDEntry[count];
            int index = offset + 8;
            for (int i = 0; i < count; i++)
                Entries[i] = new(bs, ref index);
            if (Entries[^1].ExifTag == ExifTag.ExifOffset)
                SubIFD = new(bs, Entries[^1].RawValue);
            NextIFDOffset = BitConverter.ToInt32(bs, index);
        }
    }
}
