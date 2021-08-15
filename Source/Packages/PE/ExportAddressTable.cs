using System;
using System.IO;
namespace PE
{
    public class ExportAddressTable
    {
        public int ExportRVA;
        public string Forwarder;
        public ExportAddressTable(Stream stream,GetOffset get)
        {
            byte[] bs = new byte[4];
            stream.Read(bs, 0, 4);
            ExportRVA=BitConverter.ToInt32(bs, 0);
            Forwarder = stream.GetString(get(ExportRVA));
        }
        public override string ToString() => Forwarder;
    }
}
