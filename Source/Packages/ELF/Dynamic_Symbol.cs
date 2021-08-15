using Collection;
using System;
using System.IO;
using System.Text;

namespace ELF
{
    public class Dynamic_Symbol
    {
        public uint Name_Offset;
        public string Name;
        public uint Value;
        public uint Size;
        public byte Info;
        public Dynamic_Symbol_Info_Bind Info_Bind;
        public Dynamic_Symbol_Info_Type Info_Type;
        public byte Other;
        public ushort Index;
        public Dynamic_Symbol(Stream stream)
        {
            byte[] bs = new byte[16];
            stream.Read(bs, 0, 16);
            Name_Offset = BitConverter.ToUInt32(bs, 0);
            Value = BitConverter.ToUInt32(bs, 4);
            Size = BitConverter.ToUInt32(bs, 8);
            Info = bs[12];
            Info_Bind=(Dynamic_Symbol_Info_Bind)(Info >> 4);
            Info_Type = (Dynamic_Symbol_Info_Type)(Info & 0xF);
            Other = bs[13];
            Index = BitConverter.ToUInt16(bs, 14);
        }
        public void UpdateName(byte[] data)
        {
            List<byte> bs = new();
            for (uint index = Name_Offset; index < data.Length && data[index] != 0; index++)
                bs.Add(data[index]);
            Name = Encoding.UTF8.GetString(bs.ToArray());
        }
        public override string ToString() => Name;
    }
}
