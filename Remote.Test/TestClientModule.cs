using Remote.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Remote.Test
{
    class TestClientModule : ClientModule
	{
		public int Index;
		private byte[] Temp;
		public TestClientModule(byte[] temp)
			: base("Test")
		{
			Temp = temp;
			Index = 0;
		}
		public override void Start()
		{
			if (Connect())
			{
				Pipe.SendBufferSize = 1024;
				Pipe.Send(BitConverter.GetBytes(Temp.Length), 0, 4, SocketFlags.None);
				Index = 0;
				Send();
			}
		}
		public void Send()
		{
			if (Index == Temp.Length)
			{
				byte[] array = new byte[4];
				Pipe.Receive(array, 0, 4, SocketFlags.None);
				if (BitConverter.ToInt32(array, 0) != 28938)
					Tools.Throw();
				Disconnect();
			}
			else
				Pipe.BeginSend(Temp,Index,Pipe.SendBufferSize, SocketFlags.None, EndSend,null);
		}
		public void EndSend(IAsyncResult asyncResult)
		{
			int len=Pipe.EndSend(asyncResult);
			Index += len;
			byte[] array = new byte[4];
			Pipe.Receive(array, 0, 4, SocketFlags.None);
			if(BitConverter.ToInt32(array)!=len)
				Tools.Throw();
			Send();
		}
	}
}
