using System;
using System.IO;
using System.Text;

namespace Collection.Serialization
{
	public class CustomTypeInfo
	{
		public Type Type;

		public int Index;

		public CustomTypeInfo(Type type, List<CustomTypeInfo> linkers, Stream stream)
		{
			Type = type;
			Index = linkers.Length;
			linkers.Add(this);
			byte[] bytes = Encoding.UTF8.GetBytes(Type.AssemblyQualifiedName);
			stream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
			stream.Write(bytes, 0, bytes.Length);
		}
	}
}
