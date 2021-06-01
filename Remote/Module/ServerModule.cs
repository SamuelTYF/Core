using System.Net.Sockets;
using System.Text;
namespace Remote.Module
{
	public abstract class ServerModule
	{
		public ModuleTools Tools;
		public Socket Pipe;
		public byte[] Sign;
		public event CompleteFunction Completed;
        public void SetSocket(Socket pipe) => Pipe = pipe;
        public ServerModule(string sign, ModuleTools tools = null)
		{
			Tools = tools ?? new ModuleTools_Normal();
			Sign = Encoding.UTF8.GetBytes(sign);
		}
		public abstract void Start(Socket pipe);
		public abstract void Disconnect();
		protected void _Disconnect()
		{
			Pipe.Dispose();
			Completed();
		}
        protected void _Start(Socket pipe) => Pipe = pipe;
    }
}
