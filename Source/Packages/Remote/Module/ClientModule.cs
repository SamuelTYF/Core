using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Remote.Module
{
	public abstract class ClientModule
	{
		public ModuleTools Tools;
		public byte[] Sign;
		public Socket Pipe;
		public IPEndPoint IPEndPoint;
		public event CompleteFunction Completed;
		public event ErrorFunction Error;
		public bool Connect()
		{
			try
			{
				Pipe.Connect(IPEndPoint);
				if (!Pipe.Connected)
					throw new Exception();
				Pipe.Send(Sign, 0, Sign.Length, SocketFlags.None);
				byte[] array = new byte[8];
				Pipe.Receive(array, 0, array.Length, SocketFlags.None);
                return BitConverter.ToInt32(array, 0) != Sign.Length ? throw new Exception() : true;
            }
            catch (Exception)
			{
				Error();
				return false;
			}
		}
		public ClientModule(string sign, ModuleTools tools = null)
		{
			Tools = tools ?? new ModuleTools_Normal();
			Sign = Encoding.UTF8.GetBytes(sign);
			if (Sign.Length > 32)
				Tools.Throw();
			Error += delegate
			{
				if (Pipe != null)
					Pipe.Dispose();
			};
		}
		public abstract void Start();
		public void Reset()
		{
			Completed = null;
			Error = delegate
			{
				if (Pipe != null)
					Pipe.Dispose();
			};
		}
		protected void Disconnect()
		{
			Pipe.Dispose();
			Completed();
		}
	}
}
