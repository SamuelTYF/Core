using System.Net;

namespace Net.Http
{
    public static class IPHelper
    {
        public static IPAddress GetIP(string host)
        {
            foreach (IPAddress ip in Dns.GetHostAddresses(host))
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip;
            return null;
        }
    }
}
