using Collection;
using System;
using System.IO;
using System.Text;

namespace ELF
{
    public class Section_Header_Entry
    {
        public uint Name_Offset;
        public string Name;
        public Section_Type Type;
        public Section_Flag Flag;
        public uint Address;
        public uint Offset;
        public uint Size;
        public uint Link;
        public uint Info;
        public uint Align;
        public uint Entry_Size;
        public byte[] Data;
        public Section_Header_Entry(Stream stream)
        {
            byte[] bs = new byte[40];
            stream.Read(bs, 0, 40);
            Name_Offset = BitConverter.ToUInt32(bs, 0);
            Type = (Section_Type)BitConverter.ToUInt32(bs, 4);
            Flag = (Section_Flag)BitConverter.ToUInt32(bs, 8);
            Address = BitConverter.ToUInt32(bs, 12);
            Offset = BitConverter.ToUInt32(bs, 16);
            Size = BitConverter.ToUInt32(bs, 20);
            Link = BitConverter.ToUInt32(bs, 24);
            Info = BitConverter.ToUInt32(bs, 28);
            Align = BitConverter.ToUInt32(bs, 32);
            Entry_Size = BitConverter.ToUInt32(bs, 36);
        }
        public void UpdateData(Stream stream)
        {
            stream.Position = Offset;
            Data = new byte[Size];
            stream.Read(Data, 0, Data.Length);
        }
        public void UpdateName(byte[] data)
        {
            List<byte> bs = new();
            for(uint index = Name_Offset; index < data.Length && data[index] != 0; index++)
                bs.Add(data[index]);
            Name = Encoding.UTF8.GetString(bs.ToArray());
        }
        public override string ToString() => Name;
    }
}
