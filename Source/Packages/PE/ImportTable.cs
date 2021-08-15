using Collection;
using System;
using System.IO;

namespace PE
{
    public class ImportTable
    {
        public ImportDirectoryTable ImportDirectoryTable;
        public List<ulong> ImportLookupTable;
        public List<ulong> ImportAddressTableRVA;
        public List<HintNameTable> HintNameTable;
        public ImportTable(Stream stream,GetOffset get,RVASize rva)
        {
            stream.Position = get(rva.RVA);
            ImportDirectoryTable = new(stream, get);
            stream.Position = get(ImportDirectoryTable.ImportLookupTableRVA);
            ImportLookupTable = new();
            byte[] bs = new byte[8];
            while (true)
            {
                stream.Read(bs, 0, 8);
                ulong t = BitConverter.ToUInt64(bs);
                if (t == 0) break;
                else ImportLookupTable.Add(t);
            }
            HintNameTable = new();
            foreach (ulong lookup in ImportLookupTable)
                if ((lookup & 0x8000000000000000) == 0)
                {
                    stream.Position = get((long)lookup);
                    HintNameTable.Add(new(HintNameTable.Length, stream));
                }
                else HintNameTable.Add(null);
            stream.Position = get(ImportDirectoryTable.ImportAddressTableRVA);
            ImportAddressTableRVA = new();
            bs = new byte[8];
            while (true)
            {
                stream.Read(bs, 0, 8);
                ulong t = BitConverter.ToUInt64(bs);
                if (t == 0) break;
                else ImportAddressTableRVA.Add(t);
            }      
        }
    }
}
