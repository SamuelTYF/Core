using System.Net;

namespace Remote
{
    public class IPHelper
    {
        public static IPAddress GetIP()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in ips)
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip;
            return IPAddress.Any;
        }
    }
}
