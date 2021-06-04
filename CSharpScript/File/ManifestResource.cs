using System;
using System.IO;
using CSharpScript.Resources;
namespace CSharpScript.File
{
	public class ManifestResource
	{
		public int Offset;
		public ManifestResourceAttributes Flags;
		public string Name;
		public Implementation Implementation;
		public ManifestResourceRow Row;
		public int Token;
		public ResourceReader ResourceReader;
		public long FileOffset;
		public int Length;
		public ManifestResource(ManifestResourceRow row)
		{
			ManifestResourceRow manifestResourceRow = (Row = row);
			Token = manifestResourceRow.Index | 0x28000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Offset = Row.Offset;
			Flags = (ManifestResourceAttributes)Row.Flags;
			Name = metadata.StringsHeap[Row.Name];
			if (Row.Implementation != 0)
			{
				Implementation = new Implementation(Row.Implementation, metadata);
				switch (Implementation.Flag)
				{
				case ImplementationFlag.File:
					Implementation.File.ManifestResource = this;
					break;
				case ImplementationFlag.AssemblyRef:
					Implementation.AssemblyRef.ManifestResource = this;
					break;
				default:
					throw new Exception();
				}
			}
			else if (Name.EndsWith(".resources"))
			{
				metadata.PEFile.Resources.Add(this);
			}
			else
			{
				metadata.PEFile.OtherResources.Add(this);
			}
		}
		public void ReadResource(Stream stream, long ResourceOffset)
		{
			FileOffset = (stream.Position = ResourceOffset + Offset) + 4;
			byte[] array = new byte[4];
			stream.Read(array, 0, 4);
			Length = array[0] | (array[1] << 8) | (array[2] << 16) | (array[3] << 24);
			ResourceReader = new ResourceReader(stream);
		}
		public void ReadOtherResourceInfo(Stream stream, long ResourceOffset)
		{
			FileOffset = (stream.Position = ResourceOffset + Offset) + 4;
			byte[] array = new byte[4];
			stream.Read(array, 0, 4);
			Length = array[0] | (array[1] << 8) | (array[2] << 16) | (array[3] << 24);
		}
		public override string ToString()=> 
			$"Name:{Name}\n" +
			$"Length:{Length}\n" +
			$"ResourceReader:{ResourceReader}\n";
    }
}
