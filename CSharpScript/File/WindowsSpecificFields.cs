using System.IO;
namespace CSharpScript.File
{
	public struct WindowsSpecificFields
	{
		public long ImageBase;
		public int SectionAlignment;
		public int FileAlignment;
		public short MajorOperatingSystemVersion;
		public short MinorOperatingSystemVersion;
		public short MajorImageVersion;
		public short MinorImageVersion;
		public short MajorSubsystemVersion;
		public short MinorSubsystemVersion;
		public int Win32VersionValue;
		public int SizeOfImage;
		public int SizeOfHeaders;
		public int CheckSum;
		public WindowsSubsystem Subsystem;
		public DLLCharacteristics DllCharacteristics;
		public long SizeOfStackReserve;
		public long SizeOfStackCommit;
		public long SizeOfHeapReserve;
		public long SizeOfHeapCommit;
		public int LoaderFlags;
		public int NumberOfRvaAndSizes;
		public WindowsSpecificFields(Stream stream, int magic)
		{
			if (magic == 523)
			{
				byte[] array = new byte[88];
				stream.Read(array, 0, 88);
				ImageBase = (long)(array[0] | ((ulong)array[1] << 8) | ((ulong)array[2] << 16) | ((ulong)array[3] << 24) | ((ulong)array[4] << 32) | ((ulong)array[5] << 40) | ((ulong)array[6] << 48) | ((ulong)array[7] << 56));
				SectionAlignment = array[8] | (array[9] << 8) | (array[10] << 16) | (array[11] << 24);
				FileAlignment = array[12] | (array[13] << 8) | (array[14] << 16) | (array[15] << 24);
				MajorOperatingSystemVersion = (short)(array[16] | (array[17] << 8));
				MinorOperatingSystemVersion = (short)(array[18] | (array[19] << 8));
				MajorImageVersion = (short)(array[20] | (array[21] << 8));
				MinorImageVersion = (short)(array[22] | (array[23] << 8));
				MajorSubsystemVersion = (short)(array[24] | (array[25] << 8));
				MinorSubsystemVersion = (short)(array[26] | (array[27] << 8));
				Win32VersionValue = array[28] | (array[29] << 8) | (array[30] << 16) | (array[31] << 24);
				SizeOfImage = array[32] | (array[33] << 8) | (array[34] << 16) | (array[35] << 24);
				SizeOfHeaders = array[36] | (array[37] << 8) | (array[38] << 16) | (array[39] << 24);
				CheckSum = array[40] | (array[41] << 8) | (array[42] << 16) | (array[43] << 24);
				Subsystem = (WindowsSubsystem)(array[44] | (array[45] << 8));
				DllCharacteristics = (DLLCharacteristics)(array[46] | (array[47] << 8));
				SizeOfStackReserve = (long)(array[48] | ((ulong)array[49] << 8) | ((ulong)array[50] << 16) | ((ulong)array[51] << 24) | ((ulong)array[52] << 32) | ((ulong)array[53] << 40) | ((ulong)array[54] << 48) | ((ulong)array[55] << 56));
				SizeOfStackCommit = (long)(array[56] | ((ulong)array[57] << 8) | ((ulong)array[58] << 16) | ((ulong)array[59] << 24) | ((ulong)array[60] << 32) | ((ulong)array[61] << 40) | ((ulong)array[62] << 48) | ((ulong)array[63] << 56));
				SizeOfHeapReserve = (long)(array[64] | ((ulong)array[65] << 8) | ((ulong)array[66] << 16) | ((ulong)array[67] << 24) | ((ulong)array[68] << 32) | ((ulong)array[69] << 40) | ((ulong)array[70] << 48) | ((ulong)array[71] << 56));
				SizeOfHeapCommit = (long)(array[72] | ((ulong)array[73] << 8) | ((ulong)array[74] << 16) | ((ulong)array[75] << 24) | ((ulong)array[76] << 32) | ((ulong)array[77] << 40) | ((ulong)array[78] << 48) | ((ulong)array[79] << 56));
				LoaderFlags = array[80] | (array[81] << 8) | (array[82] << 16) | (array[83] << 24);
				NumberOfRvaAndSizes = array[84] | (array[85] << 8) | (array[86] << 16) | (array[87] << 24);
			}
			else
			{
				byte[] array2 = new byte[72];
				stream.Read(array2, 4, 68);
				ImageBase = array2[4] | (array2[5] << 8) | (array2[6] << 16) | (array2[7] << 24);
				SectionAlignment = array2[8] | (array2[9] << 8) | (array2[10] << 16) | (array2[11] << 24);
				FileAlignment = array2[12] | (array2[13] << 8) | (array2[14] << 16) | (array2[15] << 24);
				MajorOperatingSystemVersion = (short)(array2[16] | (array2[17] << 8));
				MinorOperatingSystemVersion = (short)(array2[18] | (array2[19] << 8));
				MajorImageVersion = (short)(array2[20] | (array2[21] << 8));
				MinorImageVersion = (short)(array2[22] | (array2[23] << 8));
				MajorSubsystemVersion = (short)(array2[24] | (array2[25] << 8));
				MinorSubsystemVersion = (short)(array2[26] | (array2[27] << 8));
				Win32VersionValue = array2[28] | (array2[29] << 8) | (array2[30] << 16) | (array2[31] << 24);
				SizeOfImage = array2[32] | (array2[33] << 8) | (array2[34] << 16) | (array2[35] << 24);
				SizeOfHeaders = array2[36] | (array2[37] << 8) | (array2[38] << 16) | (array2[39] << 24);
				CheckSum = array2[40] | (array2[41] << 8) | (array2[42] << 16) | (array2[43] << 24);
				Subsystem = (WindowsSubsystem)(array2[44] | (array2[45] << 8));
				DllCharacteristics = (DLLCharacteristics)(array2[46] | (array2[47] << 8));
				SizeOfStackReserve = array2[48] | (array2[49] << 8) | (array2[50] << 16) | (array2[51] << 24);
				SizeOfStackCommit = array2[52] | (array2[53] << 8) | (array2[54] << 16) | (array2[55] << 24);
				SizeOfHeapReserve = array2[56] | (array2[57] << 8) | (array2[58] << 16) | (array2[59] << 24);
				SizeOfHeapCommit = array2[60] | (array2[61] << 8) | (array2[62] << 16) | (array2[63] << 24);
				LoaderFlags = array2[64] | (array2[65] << 8) | (array2[66] << 16) | (array2[67] << 24);
				NumberOfRvaAndSizes = array2[68] | (array2[69] << 8) | (array2[70] << 16) | (array2[71] << 24);
			}
		}
        public override string ToString() => $"ImageBase :\t{ImageBase:X}\n" + $"SectionAlignment :\t{SectionAlignment}\n" + $"FileAlignment :\t{FileAlignment}\n" + $"MajorOperatingSystemVersion:\t{MajorOperatingSystemVersion}\n" + $"MinorOperatingSystemVersion:\t{MinorOperatingSystemVersion}\n" + $"MajorImageVersion :\t{MajorImageVersion}\n" + $"MinorImageVersion :\t{MinorImageVersion}\n" + $"MajorSubsystemVersion :\t{MajorSubsystemVersion}\n" + $"MinorSubsystemVersion :\t{MinorSubsystemVersion}\n" + $"Win32VersionValue :\t{Win32VersionValue}\n" + $"SizeOfImage :\t{SizeOfImage:X}\n" + $"SizeOfHeaders :\t{SizeOfHeaders:X}\n" + $"CheckSum :\t{CheckSum:X}\n" + $"Subsystem :\t{Subsystem}\n" + $"DllCharacteristics :\t{DllCharacteristics:X}\n" + $"SizeOfStackReserve :\t{SizeOfStackReserve}\n" + $"SizeOfStackCommit :\t{SizeOfStackCommit}\n" + $"SizeOfHeapReserve :\t{SizeOfHeapReserve}\n" + $"SizeOfHeapCommit :\t{SizeOfHeapCommit}\n" + $"LoaderFlags :\t{LoaderFlags}\n" + $"NumberOfRvaAndSizes :\t{NumberOfRvaAndSizes:X}\n";
    }
}
