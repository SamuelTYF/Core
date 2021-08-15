using Collection;
using System;
using System.IO;
using System.Text;
namespace PE
{
    public class ExportTable
    {
        public ExportDirectoryTable ExportDirectoryTable;
        public string[] Names;
        public short[] Orders;
        public ExportAddressTable[] ExportAddressTables;
        public ExportTable(Stream stream, GetOffset get, RVASize rva)
        {
            stream.Position = get(rva.RVA);
            ExportDirectoryTable = new(stream, get);
            stream.Position = get(ExportDirectoryTable.ExportAdressTableRVA);
            ExportAddressTables = new ExportAddressTable[ExportDirectoryTable.AddressTableEntries];
            for (int i = 0; i < ExportDirectoryTable.AddressTableEntries; i++)
                ExportAddressTables[i] = new(stream,get);
            stream.Position = get(ExportDirectoryTable.NamePointerRVA);
            Names = new string[ExportDirectoryTable.NumberofNamePointers];
            byte[] bs = new byte[Names.Length << 2];
            stream.Read(bs,0,bs.Length);
            for (int i=0;i<ExportDirectoryTable.NumberofNamePointers;i++)
                Names[i] = stream.GetString(get(BitConverter.ToInt32(bs, i << 2)));
            stream.Position = get(ExportDirectoryTable.OrdinalTableRVA);
            Orders = new short[ExportDirectoryTable.AddressTableEntries];
            bs = new byte[Names.Length << 1];
            stream.Read(bs, 0, bs.Length);
            for (int i = 0; i < ExportDirectoryTable.AddressTableEntries; i++)
                Orders[i] =BitConverter.ToInt16(bs, i << 1);
        }
    }
}
