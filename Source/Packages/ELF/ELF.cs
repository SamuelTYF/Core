using Collection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELF
{
    public class ELF
    {
        public ELF_Header header;
        public TrieTree<Section_Header_Entry> Section_Header;
        public Program_Table_Entry[] Program_Table;
        public Dynamic_Symbol[] Symbols;
        public ELF(Stream stream)
        {
            header = new(stream);
            if (header.Program_Header_Entry_Size != 0)
            {
                if (stream.Position != header.Program_Header_Offset) throw new Exception();
                Program_Table = new Program_Table_Entry[header.Program_Header_Number];
                for (int i = 0; i < header.Program_Header_Number; i++)
                    Program_Table[i] = new(stream);
            }
            stream.Position = header.Section_Header_Offset;
            Section_Header_Entry[] _Section_Header = new Section_Header_Entry[header.Section_Header_Number];
            for (int i = 0; i < header.Section_Header_Number; i++)
                _Section_Header[i] = new(stream);
            Section_Header = new TrieTree<Section_Header_Entry>();
            Section_Header_Entry shstrtab = _Section_Header[header.String_Table_Index];
            if (shstrtab.Type != Section_Type.SHT_STRTAB) throw new Exception();
            shstrtab.UpdateData(stream);
            foreach (Section_Header_Entry entry in _Section_Header)
            {
                entry.UpdateName(shstrtab.Data);
                Section_Header[entry.Name] = entry;
                Console.WriteLine(entry);
            }
            Section_Header_Entry dynstr = Section_Header[".dynstr"];
            Section_Header_Entry dynsym = Section_Header[".dynsym"];
            if (dynstr != null && dynsym != null)
            {
                dynstr.UpdateData(stream);
                if (dynsym.Entry_Size != 16) throw new Exception();
                Symbols = new Dynamic_Symbol[dynsym.Size >> 4];
                stream.Position = dynsym.Offset;
                for (int i = 0; i < Symbols.Length; i++)
                {
                    Symbols[i] = new(stream);
                    Symbols[i].UpdateName(dynstr.Data);
                    Console.WriteLine(Symbols[i]);
                }
            }

            Section_Header_Entry symtab = Section_Header[".symtab"];
            Section_Header_Entry strtab = Section_Header[".strtab"];
            if (symtab != null && strtab != null)
            {
                strtab.UpdateData(stream);
                if (symtab.Entry_Size != 16) throw new Exception();
                Symbols = new Dynamic_Symbol[symtab.Size >> 4];
                stream.Position = symtab.Offset;
                for (int i = 0; i < Symbols.Length; i++)
                {
                    Symbols[i] = new(stream);
                    Symbols[i].UpdateName(strtab.Data);
                    Console.WriteLine(Symbols[i]);
                }
            }
        }
    }
}
