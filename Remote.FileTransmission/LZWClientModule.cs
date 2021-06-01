using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Code.Core;
using Remote.Module;
namespace FileTransmission
{
	public class LZWClientModule : ClientModule
	{
		public (FileInfo, string)[] Files;
		private int Index;
		private FileStream fs;
		private byte[] Temp;
        public LZWClientModule(params (FileInfo, string)[] fs)
            : base("LZWTransmission") => Files = fs;
        public override void Start()
		{
			if (Connect())
			{
				Pipe.SendBufferSize = 1048576;
				Pipe.Send(Temp = BitConverter.GetBytes(Files.Length), 0, 4, SocketFlags.None);
				Index = 0;
				Send();
			}
		}
		public void Send()
		{
			if (Index == Files.Length)
			{
				byte[] array = new byte[4];
				Pipe.Receive(array, 0, array.Length, SocketFlags.None);
				if (BitConverter.ToInt32(array, 0) != 1654324)
					Tools.Throw();
				Disconnect();
			}
			else
			{
				fs = Files[Index].Item1.OpenRead();
				byte[] bytes = Encoding.UTF8.GetBytes(Files[Index].Item2);
				Pipe.Send(Temp = BitConverter.GetBytes(bytes.Length), 0, 4, SocketFlags.None);
				Pipe.Send(bytes, 0, bytes.Length, SocketFlags.None);
				Pipe.Send(Temp = BitConverter.GetBytes(fs.Length), 0, 8, SocketFlags.None);
				fs.BeginRead(Temp = new byte[Pipe.SendBufferSize], 0, Pipe.SendBufferSize, EndRead, null);
			}
		}
		public void EndWrite(IAsyncResult asyncResult)
		{
			int num = Pipe.EndSend(asyncResult);
			while (num > 0)
			{
				byte[] array = new byte[4];
				Pipe.Receive(array, 0, array.Length, SocketFlags.None);
				num -= BitConverter.ToInt32(array, 0);
			}
			if (num < 0)
				Tools.Throw();
			Tools.Write($"{fs.Position * 100.0 /fs.Length}%\n");
			if (fs.Position == fs.Length)
			{
				Index++;
				Tools.Write(fs.Name + " Completed\n");
				fs.Dispose();
				Send();
			}
			else
				fs.BeginRead(Temp = new byte[Pipe.ReceiveBufferSize], 0, Pipe.ReceiveBufferSize, EndRead, null);
		}
		public void EndRead(IAsyncResult asyncResult)
		{
			int size = fs.EndRead(asyncResult);
			using MemoryStream ms = new();
			DLZW lzw = new();
			lzw.StartEncode(ms);
			lzw.Encode(Temp, size);
			lzw.EndEncode();
			size = (int)ms.Length;
			Temp = new byte[size];
			ms.Position = 0;
			ms.Read(Temp, 0, size);
			Pipe.BeginSend(Temp, 0, size, SocketFlags.None, EndWrite, null);
		}
	}
}
