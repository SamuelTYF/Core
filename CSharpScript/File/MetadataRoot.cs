using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Collection;
namespace CSharpScript.File
{
	public class MetadataRoot
	{
		public static HashAlgorithm _SHA1 = SHA1.Create();
		public static HashAlgorithm _SHA256 = SHA256.Create();
		public const int Signature = 1112167234;
		public short MajorVersion;
		public short MinorVersion;
		public int Reserved;
		public int Length;
		public string Version;
		public short Flags;
		public ushort Streams;
		public StreamHeader[] StreamHeaders;
		public StringsHeap StringsHeap;
		public TildeHeap TildeHeap;
		public BlobHeap BlobHeap;
		public USHeap USHeap;
		public GUIDHeap GUIDHeap;
		public AVL<int, int> QueryTree;
		public PEFile PEFile;
		public static void CheckSignature(byte[] bs)
		{
			if (bs[0] != 66 || bs[1] != 83 || bs[2] != 74 || bs[3] != 66)
				throw new Exception();
		}
		public MetadataRoot(PEFile file, Stream stream)
		{
			PEFile = file;
			long position = stream.Position;
			byte[] array = new byte[16];
			stream.Read(array, 0, 16);
			CheckSignature(array);
			MajorVersion = (short)(array[4] | (array[5] << 8));
			MinorVersion = (short)(array[6] | (array[7] << 8));
			Reserved = array[8] | (array[9] << 8) | (array[10] << 16) | (array[11] << 24);
			Length = array[12] | (array[13] << 8) | (array[14] << 16) | (array[15] << 24);
			array = new byte[Length];
			stream.Read(array, 0, Length);
			Version = Encoding.UTF8.GetString(array);
			array = new byte[4];
			stream.Read(array, 0, 4);
			Flags = (short)(array[0] | (array[1] << 8));
			Streams = (ushort)(array[2] | (array[3] << 8));
			StreamHeaders = new StreamHeader[Streams];
			for (int i = 0; i < Streams; i++)
				StreamHeaders[i] = new StreamHeader(stream);
			StreamHeader[] streamHeaders = StreamHeaders;
			foreach (StreamHeader streamHeader in streamHeaders)
			{
				stream.Position = streamHeader.Offset + position;
				array = new byte[streamHeader.Size];
				stream.Read(array, 0, streamHeader.Size);
				switch (streamHeader.Name)
				{
				case "#Strings":
					StringsHeap = new StringsHeap(array, streamHeader.Size);
					break;
				case "#US":
					USHeap = new USHeap(array, streamHeader.Size);
					break;
				case "#GUID":
					GUIDHeap = new GUIDHeap(array);
					break;
				case "#Blob":
					BlobHeap = new BlobHeap(array, streamHeader.Size);
					break;
				case "#~":
					TildeHeap = new TildeHeap(array);
					break;
				default:
					throw new Exception();
				}
			}
			QueryTree = new AVL<int, int>();
			TildeHeap.ResolveSignature(this);
			for (int k = 0; k < TildeHeap.FieldRVATable.Count; k++)
				TildeHeap.FieldRVATable.FieldRVAs[k].ResolveData(stream, this);
			for (int l = 0; l < TildeHeap.MethodDefTable.Count; l++)
				TildeHeap.MethodDefTable.MethodDefs[l].Read(stream, this);
		}
        public override string ToString() => $"Signature:\t{1112167234:X}\n" + $"MajorVersion:\t{MajorVersion:X}\n" + $"MinorVersion:\t{MinorVersion:X}\n" + $"Length:\t{Length}\n" + "Version:\t" + Version + "\n" + $"Flags:\t{Flags:X}\n" + $"Streams:\t{Streams}\n" + "StreamHeaders:\n" + string.Join("\n", (IEnumerable<StreamHeader>)StreamHeaders) + "\n" + $"#~:\n{TildeHeap}\n" + $"#Strings:\n{StringsHeap}\n" + $"#US:\n{USHeap}\n" + $"#GUID:\n{GUIDHeap}\n" + $"#Blob:\n{BlobHeap}\n";
        public TypeDef FindTypeDefInternal(string FullName)
		{
			if (!QueryTree.ContainsKey(FullName.GetHashCode()))
				return null;
			int num = QueryTree[FullName.GetHashCode()];
			return (num >> 24) switch
			{
				2 => TildeHeap.TypeDefTable.TypeDefs[(num & 0xFFFFFF) - 1], 
				1 => TildeHeap.TypeRefTable.TypeRefs[(num & 0xFFFFFF) - 1].TypeDef, 
				_ => throw new Exception(), 
			};
		}
		public TypeDef FindTypeDef(string FullName)
		{
			int num = FullName.IndexOf(',');
			if (num < 0)
				return FindTypeDefInternal(FullName);
			string fullName = FullName.Substring(0, num);
			TypeDef typeDef = FindTypeDefInternal(fullName);
			if (typeDef != null)
				return typeDef;
			int hashCode = FullName[(num + 2)..].GetHashCode();
            return !PEFile.PEManager.PEFiles.ContainsKey(hashCode) ? null : PEFile.PEManager.PEFiles[hashCode].MetadataRoot.FindTypeDef(fullName);
        }
        public TypeSpec FindTypeSpec(string FullName)
		{
			int num = QueryTree[FullName.GetHashCode()];
            return num >> 24 != 27 ? throw new Exception() : TildeHeap.TypeSpecTable.TypeSpecs[(num & 0xFFFFFF) - 1];
        }
        public TypeDefOrRef FindTypeInternal(string FullName)
		{
			if (!QueryTree.ContainsKey(FullName.GetHashCode()))
				return null;
			int num = QueryTree[FullName.GetHashCode()];
			return (num >> 24) switch
			{
				2 => new TypeDefOrRef(TildeHeap.TypeDefTable.TypeDefs[(num & 0xFFFFFF) - 1]), 
				1 => new TypeDefOrRef(TildeHeap.TypeRefTable.TypeRefs[(num & 0xFFFFFF) - 1]), 
				27 => new TypeDefOrRef(TildeHeap.TypeSpecTable.TypeSpecs[(num & 0xFFFFFF) - 1]), 
				_ => throw new Exception(), 
			};
		}
		public TypeDefOrRef FindType(string FullName)
		{
			int num = FullName.IndexOf(',');
			if (num < 0)
				return FindTypeInternal(FullName);
			string fullName = FullName.Substring(0, num);
			TypeDefOrRef typeDefOrRef = FindTypeInternal(fullName);
			if (typeDefOrRef != null)
				return typeDefOrRef;
			int hashCode = FullName[(num + 2)..].GetHashCode();
            return !PEFile.PEManager.PEFiles.ContainsKey(hashCode) ? null : PEFile.PEManager.PEFiles[hashCode].MetadataRoot.FindType(fullName);
        }
        public TypeSpec RegisterTypeArray(string TypeName) => new(TildeHeap.TypeSpecTable, new TypeSig_SZARRAY(new TypeSig_Class(new TypeDefOrRefOrSpecEncoded(FindType(TypeName)))));
        public TypeSpec RegisterTypeArray(TypeSig sig) => new(TildeHeap.TypeSpecTable, new TypeSig_SZARRAY(sig));
    }
}
