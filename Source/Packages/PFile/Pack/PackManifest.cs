using System;
using System.IO;
using Collection;
namespace PFile.Pack
{
    public class PackManifest:ISection
    {
        public long FileLength;
        public long Offset;
        public int SubFileCount;
        public int DirectoryCount;
        public int FileCount;
        public PackFileInfo[] Files;
        public PackManifest[] Directories;
        public PackManifest(SectionHeader header, Stream stream) : base(header)
        {
            byte[] bs = new byte[28];
            stream.Read(bs, 0, 28);
            FileLength=BitConverter.ToInt64(bs, 0);
            Offset = BitConverter.ToInt64(bs, 8);
            SubFileCount = BitConverter.ToInt32(bs, 16);
            DirectoryCount=BitConverter.ToInt32(bs, 20);
            FileCount=BitConverter.ToInt32(bs, 24);
            Files=new PackFileInfo[FileCount];
            for (int i = 0; i < FileCount; i++)
                Files[i] = new PackFileInfo(stream);
            Directories = new PackManifest[DirectoryCount];
            for(int i=0;i<DirectoryCount;i++)
                Directories[i]=new PackManifest(null,stream);
        }
        public override void UpdateHeader(SectionCollection sections) => throw new NotImplementedException();
    }
}