using Collection;
using System;
using System.Text;

namespace JPEG
{
    public class IFDEntry
    {
        public ExifTag ExifTag;
        public DataFormat DataFormat;
        public int Component;
        public int RawValue;
        public object Value;
        public IFDEntry(byte[] bs,ref int index)
        {
            ExifTag = (ExifTag)BitConverter.ToUInt16(bs, index);
            DataFormat = (DataFormat)BitConverter.ToUInt16(bs, index + 2);
            Component = BitConverter.ToInt32(bs, index + 4);
            RawValue = BitConverter.ToInt32(bs, index + 8);
            switch(DataFormat)
            {
                case DataFormat.UInt16:
                    Value = RawValue;
                    break;
                case DataFormat.UInt32:
                    Value = RawValue;
                    break;
                case DataFormat.String:
                    if (Component < 5)
                        Value = Encoding.UTF8.GetString(bs, index + 8, Component);
                    else
                    {
                        List<byte> ts = new();
                        for (int i = RawValue + 6; bs[i] != 0; i++)
                            ts.Add(bs[i]);
                        Value = Encoding.UTF8.GetString(ts.ToArray());
                    }
                    break;
                case DataFormat.Ratio:
                    Value = new Ratio(BitConverter.ToInt32(bs, RawValue + 6),
                        BitConverter.ToInt32(bs, RawValue + 10));
                    break;
            }
            index += 12;
        }
        public override string ToString() => $"{ExifTag}:{Value}";
    }
}
