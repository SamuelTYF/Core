using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Collection.Serialization;
using Microsoft.Win32.SafeHandles;

namespace Hook
{
	public class Manager : IDisposable
	{
		public Process Process;
		public Manager(string fname)
		{
			Process = new Process();
			Process.StartInfo.FileName = fname;
		}
		public void Start()
		{
			Process.Start();
			while (Process.MainWindowHandle == IntPtr.Zero)
				Thread.Sleep(10);
		}
		[DllImport("User32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref COPYDATASTRUCT pcd);
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ReadProcessMemory(SafeProcessHandle hProcess, IntPtr Source, IntPtr Dest, int size, out int bytesRead);
		public IntPtr Get(IntPtr source,int length,out int len)
        {
			IntPtr r = Marshal.AllocHGlobal(length);
			ReadProcessMemory(Process.SafeHandle, source, r, length, out len);
			return r;
		}
		public byte[] SendMsg(string message)
		{
			COPYDATASTRUCT copydata = default(COPYDATASTRUCT);
			copydata.cbData = Encoding.UTF8.GetBytes(message).Length + 1;
			copydata.lpData = message;
			IntPtr ptr=SendMessage(Process.MainWindowHandle, 74, Process.GetCurrentProcess().Handle, ref copydata);
			IntPtr c = Get(ptr, 4,out int len);
			if (len != 4) throw new Exception();
			c = Get(ptr+4, Marshal.ReadInt32(c), out len);
			byte[] bs = new byte[len];
			Marshal.Copy(c, bs, 0, len);
			return bs;
		}
		public void Dispose()
		{
			if (!Process.HasExited)
				Process.Kill();
			Process.Dispose();
		}
		public object Execute(string name, params object[] values)
		{
			Arg arg = new(name, values);
			using MemoryStream ms = new();
			using Formatter Formatter = new();
			Formatter.Serialize(ms,arg);
			ms.Position = 0;
			using MemoryStream rs=new(SendMsg(Convert.ToBase64String(ms.ToArray())));
			using Formatter rf = new();
			object r = rf.Deserialize(rs);
			return r;
		}
	}
}
