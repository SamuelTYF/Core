using System;
using System.Net;
using System.Net.Sockets;
using Remote.Module;
namespace Remote
{
	public class Server
	{
		private ModuleTools Tools;
		private ModuleCollection Modules;
		private bool StartConnect;
		private int _MaxConnectNumber;
		public Socket MainSocket;
		public IPAddress IP;
		public IPEndPoint IPEndPoint;
		public Server(int Port, int MaxConnectNumber, ModuleTools tools = null)
		{
			Tools = tools ?? new ModuleTools_Normal();
			_MaxConnectNumber = MaxConnectNumber;
			StartConnect = false;
			Modules = new ModuleCollection();
			IP = IPAddress.Any;
			IPEndPoint = new IPEndPoint(IP, Port);
		}
		public void BeginAccept()
		{
			if (!StartConnect)
			{
				StartConnect = true;
				MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				MainSocket.Bind(IPEndPoint);
				MainSocket.Listen(_MaxConnectNumber);
				Tools.Write($"Start Listen {MainSocket.LocalEndPoint}\n");
				MainSocket.BeginAccept(EndAccept, null);
			}
		}
		public void EndAccept()=>StartConnect = false;
        public void Close() => MainSocket.Close();
        public void EndAccept(IAsyncResult asyncResult)
		{
			if (!StartConnect) return;
			Socket socket = MainSocket.EndAccept(asyncResult);
			Tools.Write($"Accepted {socket.RemoteEndPoint}\n");
			byte[] array = new byte[16];
			socket.BeginReceive(array, 0, array.Length, SocketFlags.None, EndRead, (socket, array));
			MainSocket.BeginAccept(EndAccept, null);
		}
		public void EndRead(IAsyncResult asyncResult)
		{
			(Socket, byte[]) obj = ((Socket, byte[]))asyncResult.AsyncState;
			Socket item = obj.Item1;
			byte[] item2 = obj.Item2;
			int num = item.EndReceive(asyncResult);
			if (num == 16 && item.Available != 0)
				Tools.Throw();
			item.Send(BitConverter.GetBytes(num), 0, 4, SocketFlags.None);
			byte[] array = new byte[num];
			Array.Copy(item2, 0, array, 0, num);
			Modules.GetModule(array).Start(item);
		}
		public void Register(ServerModule module)
		{
			Modules.Register(module);
			module.Tools = Tools;
		}
    }
}
