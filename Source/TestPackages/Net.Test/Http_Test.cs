using Net.Http;
using System.Net;
using System.Net.Sockets;
using TestFramework;
namespace Net.Test
{
    public class Http_Test : ITest
    {
        public Http_Test()
            : base("Http_Test", 2)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            var auto=HeaderReader.CreateInstance();
            update(1);
            UpdateInfo(auto);
            Pre_Header ph=HeaderReader.ReadFrom(auto, Properties.Resources.baidu);
            update(2);
            UpdateInfo(ph);
        }
    }
}
