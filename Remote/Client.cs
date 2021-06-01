using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Collection;
using Remote.Module;
namespace Remote
{
	public class Client
	{
		private readonly ModuleTools Tools;
		private readonly int _MaxConnectNumber;
		private readonly Stack<ClientModule> Modules;
		private bool StartConnect;
		private int RunningCount;
		private readonly IPAddress IP;
		private readonly IPEndPoint IPEndPoint;
		public Client(string Server, int Port, int MaxConnectNumber, ModuleTools tools = null)
		{
			Tools = tools ?? new ModuleTools_Normal();
			_MaxConnectNumber = MaxConnectNumber;
			Modules = new Stack<ClientModule>();
			IP = Dns.GetHostAddresses(Server)[0];
			IPEndPoint = new IPEndPoint(IP, Port);
		}
        public void Insert(ClientModule module) => Modules.Insert(module);
        public void ModuleComplete() => RunningCount--;
        public void ModuleError(ClientModule module)
		{
			RunningCount--;
			Modules.Insert(module);
		}
		public void _Start()
		{
			while (StartConnect)
			{
				if (RunningCount == _MaxConnectNumber || Modules.Count == 0)
				{
					Thread.Sleep(10);
					continue;
				}
				RunningCount++;
				ClientModule cm = Modules.Pop();
				cm.Reset();
				cm.Pipe = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				cm.IPEndPoint = IPEndPoint;
				cm.Completed += ModuleComplete;
				cm.Error += delegate
				{
					ModuleError(cm);
				};
				cm.Start();
				Tools.Write($"Start {RunningCount} Work: {cm} {RunningCount}/{_MaxConnectNumber}\n");
			}
		}
		public void Start()
		{
			if (!StartConnect)
			{
				Tools.Write("Client Start\n");
				StartConnect = true;
				RunningCount = 0;
				Task.Run(_Start);
			}
		}
		public void WaitForEnd()
		{
			while (StartConnect)
			{
				if (RunningCount == 0 && Modules.Count == 0)
				{
					Tools.Write("Over");
					break;
				}
				Thread.Sleep(10);
			}
		}
	}
}
