using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Collection;
using Collection.Serialization;
namespace Hook
{
	public class HookManager
	{
		public TrieTree<Delegate> Delegates;
		public Formatter Formatter;
		public HookManager()
		{
			Delegates = new();
			Formatter = new();
		}
		public Arg Parse(string command)
		{
			using MemoryStream ms = new(Convert.FromBase64String(command));
			return Formatter.Deserialize(ms) as Arg;
		}
		public IntPtr Execute(IntPtr source)
		{
			COPYDATASTRUCT cOPYDATASTRUCT = Marshal.PtrToStructure<COPYDATASTRUCT>(source);
			string command=cOPYDATASTRUCT.lpData;
			Arg arg = Parse(command);
			TrieTree<Delegate> node = Delegates.GetNode(arg.Name, 0);
			if (node.Value is object)
			{
				object value = node.Value.DynamicInvoke(arg.Params);
				using MemoryStream ms = new();
				using Formatter Formatter = new();
				Formatter.Serialize(ms, value);
				ms.Position = 0;
				int len = (int)ms.Length;
				IntPtr r = Marshal.AllocHGlobal(len + 4);
				Marshal.WriteInt32(r, len);
				Marshal.Copy(ms.ToArray(), 0, r + 4, len);
				return r;
			}
			else throw new Exception();
		}
		public void Register(Delegate del)
		{
			MethodInfo mi = del.GetMethodInfo();
			Delegates[$"{mi.Name}`{mi.GetParameters().Length}", 0] = del;
		}
	}
}
