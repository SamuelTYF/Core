using System;
using System.IO;

namespace ELF
{
    public class Program_Table_Entry
    {
        public Program_Table_Type Type;
        public uint Offset;
        public uint Virtual_Address;
        public uint Physical_Address;
        public uint File_Length;
        public uint RAM_Length;
        public Program_Table_Flag Flags;
        public uint Align;
        public Program_Table_Entry(Stream stream)
        {
            byte[] bs = new byte[32];
            stream.Read(bs, 0, 32);
            Type = (Program_Table_Type)BitConverter.ToUInt32(bs, 0);
            Offset = BitConverter.ToUInt32(bs, 4);
            Virtual_Address = BitConverter.ToUInt32(bs, 8);
            Physical_Address = BitConverter.ToUInt32(bs, 12);
            File_Length = BitConverter.ToUInt32(bs, 16);
            RAM_Length = BitConverter.ToUInt32(bs, 20);
            Flags = (Program_Table_Flag)BitConverter.ToUInt32(bs, 24);
            Align = BitConverter.ToUInt32(bs, 28);
            if (Virtual_Address != Physical_Address) throw new Exception();
        }
    }
}
