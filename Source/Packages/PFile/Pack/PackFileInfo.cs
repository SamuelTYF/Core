using System;
using System.IO;
namespace PFile.Pack
{
    public class PackFileInfo
    {
        public long NameOffset;
        public long DirectoryOffset;
        public long FileLength;
        public long CommentOffset;
        public PackCodes Codes;
        public string Name;
        public string Directory;
        public string Comment;
        public PackFileInfo(Stream stream)
        {
            byte[] bs = new byte[33];
            stream.Read(bs, 0, 33);
            NameOffset = BitConverter.ToInt64(bs, 0);
            DirectoryOffset = BitConverter.ToInt64(bs, 8);
            FileLength = BitConverter.ToInt64(bs, 16);
            CommentOffset = BitConverter.ToInt64(bs, 24);
            Codes = (PackCodes)bs[32];
        }
        public void Update(StringSection fis)
        {
            Name = fis.Get(NameOffset);
            Directory=fis.Get(DirectoryOffset);
            Comment = fis.Get(CommentOffset);
        }
    }
}
