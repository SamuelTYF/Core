using Net.Http;
using Net.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JSHost
{
	public class MultiServer
	{
		public class ThreadPackage
        {
			public Socket Client;
			public Pre_Header Header;
			public byte[] Buffer;
			public ThreadPackage(Socket client, Pre_Header header, byte[] buffer)
            {
				Client=client;
				Header=header;
				Buffer=buffer;
			}
		}
		private bool _StartConnect;
		private int _MaxConnectNumber;
		public Socket MainSocket;
		public IPAddress IP;
		public IPEndPoint IPEndPoint;
		public Dictionary<string, Module> Modules;
		public WriteLine Log;
		public Thread[] Threads;
		public Queue<ThreadPackage> ThreadPackages;
		public MultiServer(int Port, int MaxConnectNumber, WriteLine log)
		{
			_MaxConnectNumber = MaxConnectNumber;
			_StartConnect = false;
			IP = IPAddress.Any;
			IPEndPoint = new IPEndPoint(IP, Port);
			Modules = new();
			Log = log;
			Threads = new Thread[MaxConnectNumber];
			ThreadPackages = new();
			for (int i = 0; i < Threads.Length; i++)
				Threads[i] = new(() => {
					while (_StartConnect)
					{
						bool tf;
						ThreadPackage package = null;
						lock (ThreadPackages)
						{
							tf = ThreadPackages.Count > 0;
							if (tf)
								package = ThreadPackages.Dequeue();
						}
						if (tf) _Deal(package.Client, package.Header, package.Buffer);
						else Thread.Sleep(100);
					}
				});
		}
		public void BeginAccept()
		{
			if (!_StartConnect)
			{
				_StartConnect = true;
				MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				MainSocket.Bind(IPEndPoint);
				MainSocket.Listen(_MaxConnectNumber);
				Log($"Start Listen {MainSocket.LocalEndPoint}\n");
				MainSocket.BeginAccept(EndAccept, null);
				for (int i = 0; i < Threads.Length; i++)
					Threads[i].Start();
			}
		}
		public void EndAccept()=>_StartConnect = false;
		public void EndAccept(IAsyncResult asyncResult)
		{
			MainSocket.BeginAccept(EndAccept, null);
			if (!_StartConnect) return;
			Socket socket = MainSocket.EndAccept(asyncResult);
			Console.Write($"Accepted {socket.RemoteEndPoint}\n");
			byte[] array = new byte[1 << 16];
			socket.BeginReceive(array, 0, array.Length, SocketFlags.None, EndRead, (socket, array));
		}
		public void EndContinueRead(IAsyncResult asyncResult)
		{
			(Socket, byte[], Pre_Header, int) obj = ((Socket, byte[], Pre_Header, int))asyncResult.AsyncState;
			Socket client = obj.Item1;
			int readed = obj.Item4 + client.EndReceive(asyncResult);
			byte[] buffer = obj.Item2;
			Log(Encoding.UTF8.GetString(buffer));
			Pre_Header header = obj.Item3;
			int total = header.Data.index + 1 + int.Parse(header.Values["Content-Length"]);
			Log(readed);
			Log(total);
			if (total == readed) Deal(client, header, buffer);
			else client.BeginReceive(buffer, readed, total - readed, SocketFlags.None, EndContinueRead, (client, buffer, header, readed));
		}
		public void EndRead(IAsyncResult asyncResult)
		{
			(Socket, byte[]) obj = ((Socket, byte[]))asyncResult.AsyncState;
			Socket client = obj.Item1;
			int readed = client.EndReceive(asyncResult);
			byte[] buffer = obj.Item2;
			Log(Encoding.UTF8.GetString(buffer));
			try
			{
				Pre_Header header = HeaderReader.ReadFrom(Encoding.UTF8.GetString(buffer));
				int total = header.Data.index + 1 + int.Parse(header.Values["Content-Length"]);
				Log(readed);
				Log(total);
				if (total > buffer.Length)
				{
					Log("Extend");
					byte[] temp = new byte[total];
					Array.Copy(buffer, 0, temp, 0, readed);
					buffer = temp;
					header.Data.end = total;
				}
				if (total == readed) Deal(client, header, buffer);
				else client.BeginReceive(buffer, readed, total - readed, SocketFlags.None, EndContinueRead, (client, buffer, header, readed));
			}
			catch (Exception e)
			{
				Response response = new();
				response.Values["Date"] = DateTime.Now.ToString("dd MMM yyyy hh:mm::ss 'GMT'", new System.Globalization.CultureInfo("en-US"));
				response.Values["Server"] = "Pumpkin Server";
				response.Values["Content-Type"] = "application/json; charset=UTF-8";
				Log(e);
				response.State = HttpState.Error;
				ObjectNode node = new();
				node["error"] = e.ToString();
				response.Data = Encoding.UTF8.GetBytes(node.ToString());
				byte[] r = response.GetBytes();
				Log(Encoding.UTF8.GetString(r));
				client.BeginSend(r, 0, r.Length, SocketFlags.None, EndSend, client);
			}
		}
		public void Deal(Socket client, Pre_Header header, byte[] buffer)
        {
			lock(ThreadPackages)
				ThreadPackages.Enqueue(new ThreadPackage(client, header, buffer));
        }
		public void _Deal(Socket client, Pre_Header header, byte[] buffer)
		{
			Response response = new();
			response.Values["Date"] = DateTime.Now.ToString("dd MMM yyyy hh:mm::ss 'GMT'", new System.Globalization.CultureInfo("en-US"));
			response.Values["Server"] = "Pumpkin Server";
			response.Values["Content-Type"] = "application/json; charset=UTF-8";
			try
			{
				response.Values["Access-Control-Allow-Origin"] = header.Values["Origin"];
				Log(header.URL);
				Log(header.Data.index);
				Log(header.Data.end);
				Log(buffer.Length);
				if (header.Mode == "POST")
				{
					ObjectNode data = Node.Parse(Encoding.UTF8.GetString(buffer), header.Data.index, header.Data.end) as ObjectNode;
					Log(data);
					Module module = Modules[header.URL.Path[0]];
					module(data, response);
				}
			}
			catch (Exception e)
			{
				Log(e);
				response.State = HttpState.Error;
				ObjectNode node = new();
				node["error"] = e.ToString();
				response.Data = Encoding.UTF8.GetBytes(node.ToString());
			}
			response.Values["Content-Length"] = response.Data.Length.ToString();
			byte[] r = response.GetBytes();
			Log(Encoding.UTF8.GetString(r));
			client.BeginSend(r, 0, r.Length, SocketFlags.None, EndSend, client);
		}
		public void EndSend(IAsyncResult asyncResult)
		{
			Socket client = asyncResult.AsyncState as Socket;
			client.EndSend(asyncResult);
			client.Disconnect(true);
		}
		public void RegisterModule(string name, Module module) => Modules[name] = module;
	}
}