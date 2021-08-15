using Collection;
using System;
using System.IO;
using System.Text;

namespace PE
{
    public class ExportDirectoryTable
    {
        public int ExportFlag;
        public int DateStamp;
        public short Major;
        public short Minor;
        public int NameRVA;
        public int OrdinalBase;
        public int AddressTableEntries;
        public int NumberofNamePointers;
        public int ExportAdressTableRVA;
        public int NamePointerRVA;
        public int OrdinalTableRVA;
        public string Name;
        public ExportDirectoryTable(Stream stream,GetOffset get)
        {
            byte[] bs = new byte[40];
            stream.Read(bs, 0, 40);
            ExportFlag = BitConverter.ToInt32(bs, 0);
            DateStamp = BitConverter.ToInt32(bs, 4);
            Major = BitConverter.ToInt16(bs, 8);
            Minor = BitConverter.ToInt16(bs, 10);
            NameRVA = BitConverter.ToInt32(bs, 12);
            OrdinalBase = BitConverter.ToInt32(bs, 16);
            AddressTableEntries = BitConverter.ToInt32(bs, 20);
            NumberofNamePointers = BitConverter.ToInt32(bs, 24);
            ExportAdressTableRVA = BitConverter.ToInt32(bs, 28);
            NamePointerRVA = BitConverter.ToInt32(bs, 32);
            OrdinalTableRVA = BitConverter.ToInt32(bs, 36);
            Name = stream.GetString(get(NameRVA));
        }
    }
}
