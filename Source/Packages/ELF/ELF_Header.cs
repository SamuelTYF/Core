using System;
using System.IO;

namespace ELF
{
    public class ELF_Header
    {
        public byte[] Magic;
        public ELF_Type Type;
        public ELF_Machine Machine;
        public uint Version;
        public uint Address;
        public uint Program_Header_Offset;
        public uint Section_Header_Offset;
        public uint Flags;
        public ushort Size;
        public ushort Program_Header_Entry_Size;
        public ushort Program_Header_Number;
        public ushort Section_Header_Entry_Size;
        public ushort Section_Header_Number;
        public ushort String_Table_Index;
        public ELF_Header(Stream stream)
        {
            Magic = new byte[16];
            stream.Read(Magic, 0, 16);
            byte[] bs = new byte[36];
            stream.Read(bs, 0, 36);
            Type = (ELF_Type)BitConverter.ToUInt16(bs, 0);
            Machine = (ELF_Machine)BitConverter.ToUInt16(bs, 2);
            Version = BitConverter.ToUInt32(bs, 4);
            Address = BitConverter.ToUInt32(bs, 8);
            Program_Header_Offset= BitConverter.ToUInt32(bs, 12);
            Section_Header_Offset= BitConverter.ToUInt32(bs, 16);
            Flags = BitConverter.ToUInt32(bs, 20);
            Size = BitConverter.ToUInt16(bs, 24);
            Program_Header_Entry_Size= BitConverter.ToUInt16(bs, 26);
            Program_Header_Number= BitConverter.ToUInt16(bs, 28);
            Section_Header_Entry_Size = BitConverter.ToUInt16(bs, 30);
            Section_Header_Number= BitConverter.ToUInt16(bs, 32);
            String_Table_Index = BitConverter.ToUInt16(bs, 34);
        }
    }
}
