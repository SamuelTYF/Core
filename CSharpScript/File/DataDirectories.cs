using System.IO;
namespace CSharpScript.File
{
	public struct DataDirectories
	{
		public RVASize ExportTable;
		public RVASize ImportTable;
		public RVASize ResourceTable;
		public RVASize ExceptionTable;
		public RVASize CertificateTable;
		public RVASize BaseRelocationTable;
		public RVASize Debug;
		public RVASize Architecture;
		public RVASize GlobalPtr;
		public RVASize TLSTable;
		public RVASize LoadConfigTable;
		public RVASize BoundImport;
		public RVASize IAT;
		public RVASize DelayImportDescriptor;
		public RVASize CLRRuntimeHeader;
		public RVASize Reserved;
		public DataDirectories(Stream stream)
		{
			byte[] array = new byte[128];
			stream.Read(array, 0, 128);
			ExportTable = (long)(array[0] | ((ulong)array[1] << 8) | ((ulong)array[2] << 16) | ((ulong)array[3] << 24) | ((ulong)array[4] << 32) | ((ulong)array[5] << 40) | ((ulong)array[6] << 48) | ((ulong)array[7] << 56));
			ImportTable = (long)(array[8] | ((ulong)array[9] << 8) | ((ulong)array[10] << 16) | ((ulong)array[11] << 24) | ((ulong)array[12] << 32) | ((ulong)array[13] << 40) | ((ulong)array[14] << 48) | ((ulong)array[15] << 56));
			ResourceTable = (long)(array[16] | ((ulong)array[17] << 8) | ((ulong)array[18] << 16) | ((ulong)array[19] << 24) | ((ulong)array[20] << 32) | ((ulong)array[21] << 40) | ((ulong)array[22] << 48) | ((ulong)array[23] << 56));
			ExceptionTable = (long)(array[24] | ((ulong)array[25] << 8) | ((ulong)array[26] << 16) | ((ulong)array[27] << 24) | ((ulong)array[28] << 32) | ((ulong)array[29] << 40) | ((ulong)array[30] << 48) | ((ulong)array[31] << 56));
			CertificateTable = (long)(array[32] | ((ulong)array[33] << 8) | ((ulong)array[34] << 16) | ((ulong)array[35] << 24) | ((ulong)array[36] << 32) | ((ulong)array[37] << 40) | ((ulong)array[38] << 48) | ((ulong)array[39] << 56));
			BaseRelocationTable = (long)(array[40] | ((ulong)array[41] << 8) | ((ulong)array[42] << 16) | ((ulong)array[43] << 24) | ((ulong)array[44] << 32) | ((ulong)array[45] << 40) | ((ulong)array[46] << 48) | ((ulong)array[47] << 56));
			Debug = (long)(array[48] | ((ulong)array[49] << 8) | ((ulong)array[50] << 16) | ((ulong)array[51] << 24) | ((ulong)array[52] << 32) | ((ulong)array[53] << 40) | ((ulong)array[54] << 48) | ((ulong)array[55] << 56));
			Architecture = (long)(array[56] | ((ulong)array[57] << 8) | ((ulong)array[58] << 16) | ((ulong)array[59] << 24) | ((ulong)array[60] << 32) | ((ulong)array[61] << 40) | ((ulong)array[62] << 48) | ((ulong)array[63] << 56));
			GlobalPtr = (long)(array[64] | ((ulong)array[65] << 8) | ((ulong)array[66] << 16) | ((ulong)array[67] << 24) | ((ulong)array[68] << 32) | ((ulong)array[69] << 40) | ((ulong)array[70] << 48) | ((ulong)array[71] << 56));
			TLSTable = (long)(array[72] | ((ulong)array[73] << 8) | ((ulong)array[74] << 16) | ((ulong)array[75] << 24) | ((ulong)array[76] << 32) | ((ulong)array[77] << 40) | ((ulong)array[78] << 48) | ((ulong)array[79] << 56));
			LoadConfigTable = (long)(array[80] | ((ulong)array[81] << 8) | ((ulong)array[82] << 16) | ((ulong)array[83] << 24) | ((ulong)array[84] << 32) | ((ulong)array[85] << 40) | ((ulong)array[86] << 48) | ((ulong)array[87] << 56));
			BoundImport = (long)(array[88] | ((ulong)array[89] << 8) | ((ulong)array[90] << 16) | ((ulong)array[91] << 24) | ((ulong)array[92] << 32) | ((ulong)array[93] << 40) | ((ulong)array[94] << 48) | ((ulong)array[95] << 56));
			IAT = (long)(array[96] | ((ulong)array[97] << 8) | ((ulong)array[98] << 16) | ((ulong)array[99] << 24) | ((ulong)array[100] << 32) | ((ulong)array[101] << 40) | ((ulong)array[102] << 48) | ((ulong)array[103] << 56));
			DelayImportDescriptor = (long)(array[104] | ((ulong)array[105] << 8) | ((ulong)array[106] << 16) | ((ulong)array[107] << 24) | ((ulong)array[108] << 32) | ((ulong)array[109] << 40) | ((ulong)array[110] << 48) | ((ulong)array[111] << 56));
			CLRRuntimeHeader = (long)(array[112] | ((ulong)array[113] << 8) | ((ulong)array[114] << 16) | ((ulong)array[115] << 24) | ((ulong)array[116] << 32) | ((ulong)array[117] << 40) | ((ulong)array[118] << 48) | ((ulong)array[119] << 56));
			Reserved = (long)(array[120] | ((ulong)array[121] << 8) | ((ulong)array[122] << 16) | ((ulong)array[123] << 24) | ((ulong)array[124] << 32) | ((ulong)array[125] << 40) | ((ulong)array[126] << 48) | ((ulong)array[127] << 56));
		}
		public override string ToString()
		{
			return $"ExportTable:\t{ExportTable:X}\n" + $"ImportTable:\t{ImportTable:X}\n" + $"ResourceTable:\t{ResourceTable:X}\n" + $"ExceptionTable:\t{ExceptionTable:X}\n" + $"CertificateTable:\t{CertificateTable:X}\n" + $"BaseRelocationTable:\t{BaseRelocationTable:X}\n" + $"Debug:\t{Debug:X}\n" + $"Architecture:\t{Architecture:X}\n" + $"GlobalPtr:\t{GlobalPtr:X}\n" + $"TLSTable:\t{TLSTable:X}\n" + $"LoadConfigTable:\t{LoadConfigTable:X}\n" + $"BoundImport:\t{BoundImport:X}\n" + $"IAT:\t{IAT:X}\n" + $"DelayImportDescriptor:\t{DelayImportDescriptor:X}\n" + $"CLRRuntimeHeader:\t{CLRRuntimeHeader:X}\n" + $"Reserved:\t{Reserved:X}\n";
		}
	}
}
