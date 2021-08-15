using System;
using System.IO;
using System.Text;
using Collection;
namespace CSharpScript.Resources
{
	public class ResourceReader
	{
		public static readonly uint MagicNumber = 3203386062u;
		public int Version;
		public int ElementNumber;
		public string[] TypeName;
		public int[] NameHashes;
		public string[] Names;
		public int[] _Names;
		public ResourceType[] ValueTypes;
		public object[] ParsedValues;
		public ResourceReader(Stream stream)
		{
			long position = stream.Position;
			BinaryReader br = new(stream, Encoding.UTF8);
			if (br.ReadUInt32() != MagicNumber)
				throw new Exception();
			int num = br.ReadInt32();
			int num2 = br.ReadInt32();
			if (num > 1)
				br.BaseStream.Seek(num2, SeekOrigin.Current);
			else
			{
				Console.WriteLine(br.ReadString());
				Console.WriteLine(br.ReadString());
			}
			Version = br.ReadInt32();
			if (Version != 2)throw new Exception();
			int namecount = br.ReadInt32();
			if (namecount < 0)throw new Exception();
			int typecount = br.ReadInt32();
			if (typecount < 0)throw new Exception();
			TypeName = new string[typecount];
			for (int j = 0; j < typecount; j++)
				TypeName[j] = br.ReadString();
			br.ReadBytes((8 - (int)(br.BaseStream.Position - position)) & 7);
			NameHashes = new int[namecount];
			for (int k = 0; k < namecount; k++)
				NameHashes[k] = br.ReadInt32();
			Names = new string[namecount];
			_Names = new int[namecount];
			ValueTypes = new ResourceType[namecount];
			ParsedValues = new object[namecount];
			AVL<long, int> aVL = new();
			for (int i = 0; i < namecount; i++)
				aVL[br.ReadInt32()] = i;
			long offset = position + br.ReadInt32();
			br = new BinaryReader(stream, Encoding.Unicode);
			aVL.LDR(delegate(long position, int i)
			{
				Names[i] = br.ReadString();
				_Names[i] = br.ReadInt32();
			});
			br = new BinaryReader(stream, Encoding.UTF8);
			for (int m = 0; m < namecount; m++)
			{
				ResourceTypeCode type = (ResourceTypeCode)Read7BitEncodedInt(br);
				switch (type)
				{
				case ResourceTypeCode.Null:
					ValueTypes[m] = ResourceType.Null;
					ParsedValues[m] = null;
					continue;
				case ResourceTypeCode.String:
					ValueTypes[m] = ResourceType.String;
					ParsedValues[m] = br.ReadString();
					continue;
				case ResourceTypeCode.Boolean:
					ValueTypes[m] = ResourceType.Boolean;
					ParsedValues[m] = br.ReadBoolean();
					continue;
				case ResourceTypeCode.Char:
					ValueTypes[m] = ResourceType.Char;
					ParsedValues[m] = (char)br.ReadUInt16();
					continue;
				case ResourceTypeCode.Byte:
					ValueTypes[m] = ResourceType.Byte;
					ParsedValues[m] = br.ReadByte();
					continue;
				case ResourceTypeCode.SByte:
					ValueTypes[m] = ResourceType.SByte;
					ParsedValues[m] = br.ReadSByte();
					continue;
				case ResourceTypeCode.Int16:
					ValueTypes[m] = ResourceType.Int16;
					ParsedValues[m] = br.ReadInt16();
					continue;
				case ResourceTypeCode.UInt16:
					ValueTypes[m] = ResourceType.UInt16;
					ParsedValues[m] = br.ReadUInt16();
					continue;
				case ResourceTypeCode.Int32:
					ValueTypes[m] = ResourceType.Int32;
					ParsedValues[m] = br.ReadInt32();
					continue;
				case ResourceTypeCode.UInt32:
					ValueTypes[m] = ResourceType.UInt32;
					ParsedValues[m] = br.ReadUInt32();
					continue;
				case ResourceTypeCode.Int64:
					ValueTypes[m] = ResourceType.Int64;
					ParsedValues[m] = br.ReadInt64();
					continue;
				case ResourceTypeCode.UInt64:
					ValueTypes[m] = ResourceType.UInt64;
					ParsedValues[m] = br.ReadUInt64();
					continue;
				case ResourceTypeCode.Single:
					ValueTypes[m] = ResourceType.Single;
					ParsedValues[m] = br.ReadSingle();
					continue;
				case ResourceTypeCode.Double:
					ValueTypes[m] = ResourceType.Double;
					ParsedValues[m] = br.ReadDouble();
					continue;
				case ResourceTypeCode.Decimal:
					ValueTypes[m] = ResourceType.Decimal;
					ParsedValues[m] = br.ReadDecimal();
					continue;
				case ResourceTypeCode.DateTime:
					ValueTypes[m] = ResourceType.DateTime;
					ParsedValues[m] = DateTime.FromBinary(br.ReadInt64());
					continue;
				case ResourceTypeCode.TimeSpan:
					ValueTypes[m] = ResourceType.TimeSpan;
					ParsedValues[m] = new TimeSpan(br.ReadInt64());
					continue;
				case ResourceTypeCode.ByteArray:
					ValueTypes[m] = ResourceType.ByteArray;
					ParsedValues[m] = br.ReadBytes(br.ReadInt32());
					continue;
				case ResourceTypeCode.Stream:
					ValueTypes[m] = ResourceType.Stream;
					ParsedValues[m] = new MemoryStream(br.ReadBytes(br.ReadInt32()));
					continue;
				}
				if (type < ResourceTypeCode.StartOfUserTypes)throw new Exception();
				ValueTypes[m] = new ResourceType(ResourceTypeCode.StartOfUserTypes, TypeName[(int)(type - 64)]);
			}
		}
		public static int Read7BitEncodedInt(BinaryReader br)
		{
			int result = 0;
			int temp = 0;
			byte b;
			do
			{
				if (temp == 35)
					throw new Exception();
				b = br.ReadByte();
				result |= (b & 0x7F) << temp;
				temp += 7;
			}
			while ((b & 0x80u) != 0);
			return result;
		}
		public override string ToString()
		{
			string s = "";
			for (int i = 0; i < Names.Length; i++)
				s += $"{Names[i]}\t{ParsedValues[i]}\n";
			return s;
		}
	}
}
