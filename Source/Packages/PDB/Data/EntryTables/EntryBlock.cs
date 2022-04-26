using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDB.Data.EntryTables
{
    public class EntryBlock
    {
        public long Offset;
        public int Index;
        public Entry[] Entries;

    }
}
