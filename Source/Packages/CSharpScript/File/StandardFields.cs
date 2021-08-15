using System.IO;
namespace CSharpScript.File
{
	public struct StandardFields
	{
		public int Magic;
		public int MajorLinkerVersion;
		public int MinorLinkerVersion;
		public int SizeOfCode;
		public int SizeOfInitializedData;
		public int SizeOfUninitializedData;
		public int AddressOfEntryPoint;
		public int BaseOfCode;
		public int BaseOfData;
		public StandardFields(Stream stream)
		{
			byte[] array = new byte[2];
			stream.Read(array, 0, 2);
			Magic = array[0] | (array[1] << 8);
			if (Magic == 523)
			{
				array = new byte[24];
				stream.Read(array, 2, 22);
				MajorLinkerVersion = array[2];
				MinorLinkerVersion = array[3];
				SizeOfCode = array[4] | (array[5] << 8) | (array[6] << 16) | (array[7] << 24);
				SizeOfInitializedData = array[8] | (array[9] << 8) | (array[10] << 16) | (array[11] << 24);
				SizeOfUninitializedData = array[12] | (array[13] << 8) | (array[14] << 16) | (array[15] << 24);
				AddressOfEntryPoint = array[16] | (array[17] << 8) | (array[18] << 16) | (array[19] << 24);
				BaseOfCode = array[20] | (array[21] << 8) | (array[22] << 16) | (array[23] << 24);
				BaseOfData = 0;
			}
			else
			{
				array = new byte[28];
				stream.Read(array, 2, 26);
				MajorLinkerVersion = array[2];
				MinorLinkerVersion = array[3];
				SizeOfCode = array[4] | (array[5] << 8) | (array[6] << 16) | (array[7] << 24);
				SizeOfInitializedData = array[8] | (array[9] << 8) | (array[10] << 16) | (array[11] << 24);
				SizeOfUninitializedData = array[12] | (array[13] << 8) | (array[14] << 16) | (array[15] << 24);
				AddressOfEntryPoint = array[16] | (array[17] << 8) | (array[18] << 16) | (array[19] << 24);
				BaseOfCode = array[20] | (array[21] << 8) | (array[22] << 16) | (array[23] << 24);
				BaseOfData = array[24] | (array[25] << 8) | (array[26] << 16) | (array[27] << 24);
			}
		}
		public override string ToString()
		{
			return $"Magic :\t{Magic:X}\n" + $"MajorLinkerVersion :\t{MajorLinkerVersion}\n" + $"MinorLinkerVersion :\t{MinorLinkerVersion}\n" + $"SizeOfCode :\t{SizeOfCode:X}\n" + $"SizeOfInitializedData :\t{SizeOfInitializedData:X}\n" + $"SizeOfUninitializedData:\t{SizeOfUninitializedData:X}\n" + $"AddressOfEntryPoint :\t{AddressOfEntryPoint:X}\n" + $"BaseOfCode :\t{BaseOfCode:X}\n" + $"BaseOfData :\t{BaseOfData:X}\n";
		}
	}
}
