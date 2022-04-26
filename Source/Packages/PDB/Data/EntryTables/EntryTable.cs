using System;
using System.IO;

namespace PDB.Data.EntryTables
{
    public interface IEntryTable
    {
        public EntryBlock GetEntryBlock(int index);
        public Entry GetEntry(long index);
    }
}
