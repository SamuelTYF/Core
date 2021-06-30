using System;
using System.Net.Sockets;
using Remote.Module;
namespace Remote.Test
{
    public class TestServerModule : ServerModule
    {
		public int Length;
		public byte[] Buffer;
		private static readonly byte[] CorrectByte = BitConverter.GetBytes(28938);
		public TestServerModule()
            :base("Test")
        {
        }
        public override void Disconnect() => _Disconnect();
        public override void Start(Socket pipe)
		{
			_Start(pipe);
			Pipe.ReceiveBufferSize = 1024;
			byte[] Temp = new byte[4];
			Pipe.Receive(Temp, 0, 4, SocketFlags.None);
			int len = BitConverter.ToInt32(Temp, 0);
			Tools.Write(len);
			Buffer = new byte[len];
			Length = 0;
			Recieve();
		}
		public void Recieve()
		{
			if (Buffer.Length == Length)
			{
				Pipe.Send(CorrectByte, 0, 4, SocketFlags.None);
				Tools.Write("Completed\n");
				Disconnect();
				return;
			}
			Pipe.BeginReceive(Buffer, Length, Pipe.ReceiveBufferSize, SocketFlags.None, EndRead, null);
		}
		public void EndRead(IAsyncResult asyncResult)
		{
			int num = Pipe.EndReceive(asyncResult);
			Tools.Write(num);
			Pipe.Send(BitConverter.GetBytes(num), 0, 4, SocketFlags.None);
			Length += num;
			Recieve();
		}
	}
}
