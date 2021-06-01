using System;
using TestFramework;
using FileTransmission;
using System.IO;

namespace Remote.Test
{
    public class LZWFTransmission_Test : ITest
    {
        public LZWFTransmission_Test()
            : base("LZWFTransmission_Test", 9)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            DirectoryInfo di = new($"{Environment.CurrentDirectory}/Temp");
            if (!di.Exists) di.Create();
            FileInfo fi1 = new("Remote.dll");
            FileInfo fi2 = new("Code.Core.dll");
            Server server = new(8888, 1, new TestTool(UpdateInfo));
            update(1);
            server.Register(new LZWServerModule(Environment.CurrentDirectory));
            update(2);
            server.BeginAccept();
            update(3);
            Client client = new(IPHelper.GetIP().ToString(), 8888, 1);
            update(4);
            client.Insert(new LZWClientModule((fi1,fi1.Name), (fi2, fi2.Name)));
            update(5);
            client.Start();
            update(6);
            client.WaitForEnd();
            update(7);
            server.EndAccept();
            server.Close();
            update(8);
            using FileStream fs1 = fi1.OpenRead();
            using FileStream fs2 = new FileInfo($"Temp/{fi1.Name}").OpenRead();
            Ensure.Equal(fs1, fs2);
            update(9);
            fs2.Dispose();
            di.Delete(true);
        }
    }
}
