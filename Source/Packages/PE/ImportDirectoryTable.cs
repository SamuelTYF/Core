using System;
using System.IO;
namespace PE
{
    public class ImportDirectoryTable
    {
        public int ImportLookupTableRVA;
        public int DateStamp;
        public int ForwarderChain;
        public int NameRVA;
        public int ImportAddressTableRVA;
        public string Name;
        public ImportDirectoryTable(Stream stream,GetOffset get)
        {
            byte[] bs = new byte[20];
            stream.Read(bs, 0, 20);
            ImportLookupTableRVA = BitConverter.ToInt32(bs, 0);
            DateStamp = BitConverter.ToInt32(bs, 4);
            ForwarderChain = BitConverter.ToInt32(bs, 8);
            NameRVA = BitConverter.ToInt32(bs, 12);
            ImportAddressTableRVA = BitConverter.ToInt32(bs, 16);
            Name = stream.GetString(get(NameRVA));
        }
    }
}
