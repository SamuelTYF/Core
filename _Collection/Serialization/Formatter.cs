using System;
using System.IO;
using System.Text;

namespace Collection.Serialization
{
	public class Formatter : IDisposable
	{
		public TrieTree<Write> SystemTypeWrites;

		public List<Read> SystemTypeReads;

		public TrieTree<byte[]> SystemTypeIndexs;

		public TrieTree<CustomTypeInfo> CustomTypeInfoTypes;

		public List<CustomTypeInfo> Linkers;

		public Stack<Tuple<ReadReference, ReferenceCallBack>> ReferenceHosts;

		public Type[] Types;

		private int Index = 0;

		public Stream HeaderStream;

		public Stream TempStream;

		public Formatter()
		{
			SystemTypeWrites = new TrieTree<Write>();
			SystemTypeReads = new List<Read>();
			SystemTypeReads.Add(null);
			SystemTypeIndexs = new TrieTree<byte[]>();
			ReferenceHosts = new Stack<Tuple<ReadReference, ReferenceCallBack>>();
			Register(delegate(string value, Stream stream)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(value);
				stream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
				stream.Write(bytes, 0, bytes.Length);
			}, (Stream stream) => Encoding.UTF8.GetString(ReadBytes(ReadInt32())));
			Register(delegate(byte[] value, Stream stream)
			{
				stream.Write(BitConverter.GetBytes(value.Length), 0, 4);
				stream.Write(value, 0, value.Length);
			}, (Stream stream) => ReadBytes(ReadInt32()));
			Register(delegate(int value, Stream stream)
			{
				stream.Write(BitConverter.GetBytes(value), 0, 4);
			}, (Stream stream) => ReadInt32());
			Register(delegate(long value, Stream stream)
			{
				stream.Write(BitConverter.GetBytes(value), 0, 8);
			}, (Stream stream) => ReadInt64());
			Register(delegate(bool value, Stream stream)
			{
				stream.WriteByte((byte)(value ? 1 : 0));
			}, (Stream stream) => stream.ReadByte() == 1);
			Register(delegate(double value, Stream stream)
			{
				stream.Write(BitConverter.GetBytes(value), 0, 8);
			}, (Stream stream) => BitConverter.ToDouble(ReadBytes(8), 0));
		}

		private void Register<T>(Write<T> write, Read read)
		{
			string assemblyQualifiedName = typeof(T).AssemblyQualifiedName;
			byte[] bs = (SystemTypeIndexs[assemblyQualifiedName, 0] = BitConverter.GetBytes(--Index));
			SystemTypeWrites[assemblyQualifiedName, 0] = delegate(object value, Stream stream)
			{
				T val = (T)value;
				stream.Write(bs, 0, 4);
				write((T)value, stream);
			};
			SystemTypeReads.Add((Stream stream) => read(stream));
		}

		public void Write(object value)
		{
			if (value == null)
			{
				TempStream.Write(BitConverter.GetBytes(int.MinValue), 0, 4);
				return;
			}
			Type type = value.GetType();
			string assemblyQualifiedName = type.AssemblyQualifiedName;
			Write write = SystemTypeWrites[assemblyQualifiedName, 0];
			if (write != null)
			{
				write(value, TempStream);
			}
			else if (value is Array)
			{
				TrieTree<CustomTypeInfo> node = CustomTypeInfoTypes.GetNode(assemblyQualifiedName);
				CustomTypeInfo customTypeInfo = node.Value ?? (node.Value = new CustomTypeInfo(type, Linkers, HeaderStream));
				TempStream.Write(BitConverter.GetBytes(customTypeInfo.Index), 0, 4);
				TempStream.Write(BitConverter.GetBytes((value as Array).Length), 0, 4);
				foreach (object item in value as Array)
				{
					Write(item);
				}
			}
			else if (value is Enum)
			{
				TrieTree<CustomTypeInfo> node2 = CustomTypeInfoTypes.GetNode(assemblyQualifiedName);
				CustomTypeInfo customTypeInfo2 = node2.Value ?? (node2.Value = new CustomTypeInfo(type, Linkers, HeaderStream));
				TempStream.Write(BitConverter.GetBytes(customTypeInfo2.Index), 0, 4);
				byte[] bytes = Encoding.UTF8.GetBytes(value.ToString());
				TempStream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
				TempStream.Write(bytes, 0, bytes.Length);
			}
			else
			{
				if (!(value is ISerializable))
				{
					throw new Exception();
				}
				TrieTree<CustomTypeInfo> node3 = CustomTypeInfoTypes.GetNode(assemblyQualifiedName);
				CustomTypeInfo customTypeInfo3 = node3.Value ?? (node3.Value = new CustomTypeInfo(type, Linkers, HeaderStream));
				TempStream.Write(BitConverter.GetBytes(customTypeInfo3.Index), 0, 4);
				(value as ISerializable).Write(this);
			}
		}

		public void Serialize(Stream stream, object value)
		{
			if (HeaderStream != null || TempStream != null)
			{
				throw new Exception();
			}
			CustomTypeInfoTypes = new TrieTree<CustomTypeInfo>();
			Linkers = new List<CustomTypeInfo>();
			using (HeaderStream = new MemoryStream())
			{
				using (TempStream = new MemoryStream())
				{
					Write(value);
					HeaderStream.Position = 0L;
					TempStream.Position = 0L;
					stream.Write(BitConverter.GetBytes(Linkers.Length), 0, 4);
					HeaderStream.CopyTo(stream);
					TempStream.CopyTo(stream);
				}
			}
		}

		public int ReadInt32()
		{
			byte[] array = new byte[4];
			TempStream.Read(array, 0, 4);
			return BitConverter.ToInt32(array, 0);
		}

		public long ReadInt64()
		{
			byte[] array = new byte[8];
			TempStream.Read(array, 0, 8);
			return BitConverter.ToInt64(array, 0);
		}

		public byte[] ReadBytes(int length)
		{
			byte[] array = new byte[length];
			TempStream.Read(array, 0, length);
			return array;
		}

		public object Read()
		{
			int num = ReadInt32();
			if (num == int.MinValue)
			{
				return null;
			}
			if (num < 0)
			{
				return SystemTypeReads[-num](TempStream);
			}
			Type type = Types[num];
			if (type.IsArray)
			{
				Array array = type.GetConstructors()[0].Invoke(new object[1] { ReadInt32() }) as Array;
				for (int i = 0; i < array.Length; i++)
				{
					array.SetValue(Read(), i);
				}
				return array;
			}
			if (type.IsEnum)
			{
				return Enum.Parse(type, Encoding.UTF8.GetString(ReadBytes(ReadInt32())));
			}
			if (type.GetInterface(typeof(ISerializable).FullName) != null)
			{
				return type.GetConstructor(new Type[1] { typeof(Formatter) }).Invoke(new object[1] { this });
			}
			throw new Exception();
		}

		public object Deserialize(Stream stream)
		{
			if (HeaderStream != null || TempStream != null)
			{
				throw new Exception();
			}
			TempStream = stream;
			Types = new Type[ReadInt32()];
			for (int i = 0; i < Types.Length; i++)
			{
				Types[i] = Type.GetType(Encoding.UTF8.GetString(ReadBytes(ReadInt32())));
			}
			object result = Read();
			TempStream = null;
			return result;
		}

		public void RegisterReferenceHost(ReadReference @interface)
		{
			ReferenceHosts.Insert(new Tuple<ReadReference, ReferenceCallBack>(@interface, null));
		}

		public void TackleReferences()
		{
			ReferenceHosts.Pop().Value2?.Invoke();
		}

		public void AddReference(ReferenceCallBack callBack)
		{
			Tuple<ReadReference, ReferenceCallBack> tuple = ReferenceHosts.Get();
			tuple.Value2 = (ReferenceCallBack)Delegate.Combine(tuple.Value2, callBack);
		}

		public ReadReference GetInterface()
		{
			return ReferenceHosts.Get().Value1;
		}

		public void Dispose()
		{
			SystemTypeWrites = null;
			SystemTypeReads = null;
			SystemTypeIndexs = null;
			ReferenceHosts = null;
			CustomTypeInfoTypes = null;
			Linkers = null;
			Types = null;
			Index = 0;
			if (HeaderStream != null)
			{
				HeaderStream.Dispose();
			}
			if (TempStream != null)
			{
				TempStream.Dispose();
			}
		}

		public static T Deserialize<T>(Stream stream) where T : class
		{
			using Formatter formatter = new Formatter();
			return formatter.Deserialize(stream) as T;
		}
	}
}
