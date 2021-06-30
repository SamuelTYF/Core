using System;
using TestFramework;
namespace Remote.Test
{
    public class Remote_Test:ITest
    {
        public Remote_Test()
            :base("Remote_Test",9)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            byte[] temp = new byte[1 << 16];
            Random _R = new(DateTime.Now.Millisecond);
            _R.NextBytes(temp);
            Server server = new(8888, 1,new TestTool(UpdateInfo));
            update(1);
            TestServerModule servermodule = new();
            server.Register(servermodule);
            update(2);
            server.BeginAccept();
            update(3);
            Client client = new(IPHelper.GetIP().ToString(), 8888, 1);
            update(4);
            client.Insert(new TestClientModule(temp));
            update(5);
            client.Start();
            update(6);
            client.WaitForEnd();
            update(7);
            server.EndAccept();
            update(8);
            server.Close();
            byte[] r = servermodule.Buffer;
            Ensure.Equal(r.Length, temp.Length);
            for (int i = 0; i < r.Length; i++)
                Ensure.Equal(r[i], temp[i]);
            update(9);
        }
    }
}
