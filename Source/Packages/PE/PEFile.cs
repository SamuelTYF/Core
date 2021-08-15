using System;
using System.IO;
using Collection;
namespace PE
{
	public class PEFile
	{
		public static readonly byte[] MS_DOS_Header = new byte[128]
		{
			77, 90, 144, 0, 3, 0, 0, 0, 4, 0,
			0, 0, 255, 255, 0, 0, 184, 0, 0, 0,
			0, 0, 0, 0, 64, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			128, 0, 0, 0, 14, 31, 186, 14, 0, 180,
			9, 205, 33, 184, 1, 76, 205, 33, 84, 104,
			105, 115, 32, 112, 114, 111, 103, 114, 97, 109,
			32, 99, 97, 110, 110, 111, 116, 32, 98, 101,
			32, 114, 117, 110, 32, 105, 110, 32, 68, 79,
			83, 32, 109, 111, 100, 101, 46, 13, 13, 10,
			36, 0, 0, 0, 0, 0, 0, 0
		};
		public int HeaderSize;
		public Header Header;
		public OptionalHeader OptionalHeader;
		public AVL<int, SectionHeader> SectionHeaders;
		public long FileOffset;
		public byte[] StrongNameSignature;
		public long Offset;
		public ExportTable ExportTable;
		public ImportTable ImportTable;
		public static int IsMS_DOS_Header(byte[] bs)
		{
			for (int i = 0; i < 60; i++)
				if (bs[i] != MS_DOS_Header[i])
					return -1;
			int result = bs[60] | (bs[61] << 8) | (bs[62] << 16) | (bs[63] << 24);
			for (int j = 64; j < 128; j++)
				if (bs[j] != MS_DOS_Header[j])
					return -1;
			return result;
		}
        public static bool CheckPE(byte[] bs) => bs[0] == 80 && bs[1] == 69 && bs[2] == 0 && bs[3] == 0;
        public long GetOffset(long RVA)
		{
			SectionHeader value = null;
            return SectionHeaders.Predecessor((int)RVA + 1, ref value) ? Offset + RVA - value.ImageBase : throw new Exception();
        }
        public PEFile(Stream stream)
		{
			Offset = stream.Position;
			FileOffset = stream.Position;
			byte[] array = new byte[128];
			stream.Read(array, 0, 128);
			HeaderSize = IsMS_DOS_Header(array);
			if (HeaderSize < 0)
				throw new Exception();
			stream.Position = Offset + HeaderSize;
			stream.Read(array, 0, 4);
			if (!CheckPE(array))
				throw new Exception();
			Header = new Header(stream);
			OptionalHeader = new OptionalHeader(stream);
			SectionHeaders = new AVL<int, SectionHeader>();
			for (int i = 0; i < Header.NumberOfSections; i++)
			{
				SectionHeader sectionHeader = new(stream);
				SectionHeaders[sectionHeader.VirtualAddress] = sectionHeader;
			}
			ExportTable = new(stream,GetOffset,OptionalHeader.DataDirectories.ExportTable);
			ImportTable = new(stream, GetOffset, OptionalHeader.DataDirectories.ImportTable);
		}
        public override string ToString() => string.Format("Header:\n{0}\nOptionalHeader:\n{1}\nSectionHeaders:\n{2}\n", Header, OptionalHeader, string.Join("\n", SectionHeaders));
	}
}
