using System;
using System.IO;
using TestFramework;
namespace Code.Core.Test
{
    public class DLZW_Test : ITest
    {
        public FileInfo[] Files;
        public DLZW_Test()
            : base("DLZW_Test", 10) 
        {
            Files = new DirectoryInfo(Environment.CurrentDirectory).GetFiles();
            TaskCount = Files.Length;
        }
        public override void Run(UpdateTaskProgress update)
        {
            for(int i=0;i<TaskCount;i++)
            {
                using FileStream fs = Files[i].OpenRead();
                using MemoryStream ms = new();
                DLZW.Encode(fs, ms);
                ms.Position = 0;
                using MemoryStream rs = new();
                DLZW.Decode(ms, rs);
                Ensure.Equal(fs.Length, rs.Length);
                fs.Position = 0;
                rs.Position = 0;
                byte[] a = new byte[fs.Length];
                fs.Read(a, 0, a.Length);
                byte[] b = new byte[rs.Length];
                rs.Read(b, 0, b.Length);
                for (int j = 0; j < a.Length; j++)
                    Ensure.Equal(a[j], b[j]);
                update(i+1);
            }
        }
    }
}
