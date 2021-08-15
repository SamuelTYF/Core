using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Code.Core;
using Remote.Module;
namespace FileTransmission
{
	public class LZWServerModule : ServerModule
	{
		public DirectoryInfo di;
		private FileStream fs;
		private (FileInfo, string)[] Files;
		private byte[] Temp;
		private long len;
		private int Index;
		private DateTime Last;
		private static readonly byte[] CorrectByte = BitConverter.GetBytes(1654324);
        public LZWServerModule(string dir)
            : base("LZWTransmission") => di = new DirectoryInfo(dir);
        public override void Disconnect() => _Disconnect();
        public override void Start(Socket pipe)
		{
			_Start(pipe);
			Pipe.ReceiveBufferSize = 1048576;
			Temp = new byte[4];
			Pipe.Receive(Temp, 0, 4, SocketFlags.None);
			int num = BitConverter.ToInt32(Temp, 0);
			Files = new(FileInfo, string)[num];
			Index = 0;
			Recieve();
		}
		public void Recieve()
		{
			if (Index == Files.Length)
			{
				Pipe.Send(CorrectByte, 0, 4, SocketFlags.None);
				Tools.Write("Completed\n");
				Disconnect();
				return;
			}
			Temp = new byte[4];
			Pipe.Receive(Temp, 0, 4, SocketFlags.None);
			Temp = new byte[BitConverter.ToInt32(Temp, 0)];
			Pipe.Receive(Temp, 0, Temp.Length, SocketFlags.None);
			Files[Index].Item2 = Encoding.UTF8.GetString(Temp);
			Files[Index].Item1 = new FileInfo("Temp\\" + Files[Index].Item2);
			fs = Files[Index].Item1.OpenWrite();
			Temp = new byte[8];
			Pipe.Receive(Temp, 0, 8, SocketFlags.None);
			len = BitConverter.ToInt64(Temp, 0);
			Last = DateTime.Now;
			Pipe.BeginReceive(Temp = new byte[Pipe.ReceiveBufferSize], 0, Pipe.ReceiveBufferSize, SocketFlags.None, EndRecieve, null);
		}
		public void EndRecieve(IAsyncResult asyncResult)
		{
			int num = Pipe.EndReceive(asyncResult);
			Pipe.Send(BitConverter.GetBytes(num), 0, 4, SocketFlags.None);
			using MemoryStream ms = new();
			ms.Write(Temp, 0, num);
			ms.Position = 0;
			using MemoryStream rs = new();
			DLZW.Decode(ms, rs);
			int tnum = (int)rs.Length;
			rs.Position = 0;
			Temp = new byte[tnum];
			rs.Read(Temp, 0, tnum);
			len -= tnum;
			TimeSpan interval = DateTime.Now - Last;
			double ticks = (interval).TotalMilliseconds;
			Tools.Write($"{num}\t{tnum}\t{100.0*(tnum-num)/tnum}\t{interval}\t{1000*tnum/ticks}");
			Last = DateTime.Now;
			if (len == 0L)
				fs.BeginWrite(Temp, 0, tnum, _EndWrite, null);
			else
				fs.BeginWrite(Temp, 0, tnum, EndWrite, null);
		}
		public void EndWrite(IAsyncResult asyncResult)
		{
			fs.EndWrite(asyncResult);
			Tools.Write($"{fs.Position * 100.0 / (fs.Position + len)}%\n");
			Pipe.BeginReceive(Temp = new byte[Pipe.ReceiveBufferSize], 0, (int)((len > Pipe.ReceiveBufferSize) ? Pipe.ReceiveBufferSize : len), SocketFlags.None, EndRecieve, null);
		}
		public void _EndWrite(IAsyncResult asyncResult)
		{
			fs.EndWrite(asyncResult);
			Tools.Write($"{fs.Position * 100.0 / (fs.Position + len)}%\n");
			Tools.Write(fs.Name + " Over\n");
			Index++;
			fs.Dispose();
			Recieve();
		}
	}
}
