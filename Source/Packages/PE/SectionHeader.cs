using System.IO;
using System.Text;
namespace PE
{
	public class SectionHeader
	{
		public string Name;
		public int VirtualSize;
		public int VirtualAddress;
		public long VirtualAddressEnd;
		public long ImageBase;
		public int SizeOfRawData;
		public int PointerToRawData;
		public int PointerToRelocations;
		public int PointerToLinenumbers;
		public short NumberOfRelocations;
		public short NumberOfLinenumbers;
		public SectionFlags Characteristics;
		public SectionHeader(Stream stream)
		{
			byte[] array = new byte[40];
			stream.Read(array, 0, 40);
			Name = Encoding.UTF8.GetString(array, 0, 8);
			VirtualSize = array[8] | (array[9] << 8) | (array[10] << 16) | (array[11] << 24);
			VirtualAddress = array[12] | (array[13] << 8) | (array[14] << 16) | (array[15] << 24);
			SizeOfRawData = array[16] | (array[17] << 8) | (array[18] << 16) | (array[19] << 24);
			PointerToRawData = array[20] | (array[21] << 8) | (array[22] << 16) | (array[23] << 24);
			PointerToRelocations = array[24] | (array[25] << 8) | (array[26] << 16) | (array[27] << 24);
			PointerToLinenumbers = array[28] | (array[29] << 8) | (array[30] << 16) | (array[31] << 24);
			NumberOfRelocations = (short)(array[32] | (array[33] << 8));
			NumberOfLinenumbers = (short)(array[34] | (array[35] << 8));
			Characteristics = (SectionFlags)(array[36] | (array[37] << 8) | (array[38] << 16) | (array[39] << 24));
			VirtualAddressEnd = SizeOfRawData + VirtualAddress;
			ImageBase = VirtualAddress - PointerToRawData;
		}
		public override string ToString()
		{
			return "Name:\t" + Name + "\n" + $"VirtualSize:\t{VirtualSize:X}\n" + $"VirtualAddress:\t{VirtualAddress:X}\n" + $"SizeOfRawData:\t{SizeOfRawData:X}\n" + $"PointerToRawData:\t{PointerToRawData:X}\n" + $"PointerToRelocations:\t{PointerToRelocations:X}\n" + $"PointerToLinenumbers:\t{PointerToLinenumbers:X}\n" + $"NumberOfRelocations:\t{NumberOfRelocations:X}\n" + $"NumberOfLinenumbers:\t{NumberOfLinenumbers:X}\n" + $"Characteristics:\t{Characteristics}\n";
		}
	}
}
