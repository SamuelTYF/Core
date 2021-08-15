using System.IO;
namespace CSharpScript.File
{
	public struct CLIHeader
	{
		public int Cb;
		public short MajorRuntimeVersion;
		public short MinorRuntimeVersion;
		public RVASize MetaData;
		public RuntimeFlag Flags;
		public int EntryPointToken;
		public RVASize Resources;
		public RVASize StrongNameSignature;
		public RVASize CodeManagerTable;
		public RVASize VTableFixups;
		public RVASize ExportAddressTableJumps;
		public RVASize ManagedNativeHeader;
		public CLIHeader(Stream stream)
		{
			byte[] array = new byte[72];
			stream.Read(array, 0, 72);
			Cb = array[0] | (array[1] << 8) | (array[2] << 16) | (array[3] << 24);
			MajorRuntimeVersion = (short)(array[4] | (array[5] << 8));
			MinorRuntimeVersion = (short)(array[6] | (array[7] << 8));
			MetaData = (long)(array[8] | ((ulong)array[9] << 8) | ((ulong)array[10] << 16) | ((ulong)array[11] << 24) | ((ulong)array[12] << 32) | ((ulong)array[13] << 40) | ((ulong)array[14] << 48) | ((ulong)array[15] << 56));
			Flags = (RuntimeFlag)(array[16] | (array[17] << 8) | (array[18] << 16) | (array[19] << 24));
			EntryPointToken = array[20] | (array[21] << 8) | (array[22] << 16) | (array[23] << 24);
			Resources = (long)(array[24] | ((ulong)array[25] << 8) | ((ulong)array[26] << 16) | ((ulong)array[27] << 24) | ((ulong)array[28] << 32) | ((ulong)array[29] << 40) | ((ulong)array[30] << 48) | ((ulong)array[31] << 56));
			StrongNameSignature = (long)(array[32] | ((ulong)array[33] << 8) | ((ulong)array[34] << 16) | ((ulong)array[35] << 24) | ((ulong)array[36] << 32) | ((ulong)array[37] << 40) | ((ulong)array[38] << 48) | ((ulong)array[39] << 56));
			CodeManagerTable = (long)(array[40] | ((ulong)array[41] << 8) | ((ulong)array[42] << 16) | ((ulong)array[43] << 24) | ((ulong)array[44] << 32) | ((ulong)array[45] << 40) | ((ulong)array[46] << 48) | ((ulong)array[47] << 56));
			VTableFixups = (long)(array[48] | ((ulong)array[49] << 8) | ((ulong)array[50] << 16) | ((ulong)array[51] << 24) | ((ulong)array[52] << 32) | ((ulong)array[53] << 40) | ((ulong)array[54] << 48) | ((ulong)array[55] << 56));
			ExportAddressTableJumps = (long)(array[56] | ((ulong)array[57] << 8) | ((ulong)array[58] << 16) | ((ulong)array[59] << 24) | ((ulong)array[60] << 32) | ((ulong)array[61] << 40) | ((ulong)array[62] << 48) | ((ulong)array[63] << 56));
			ManagedNativeHeader = (long)(array[64] | ((ulong)array[65] << 8) | ((ulong)array[66] << 16) | ((ulong)array[67] << 24) | ((ulong)array[68] << 32) | ((ulong)array[69] << 40) | ((ulong)array[70] << 48) | ((ulong)array[71] << 56));
		}
        public override string ToString() => $"Cb:\t{Cb:X}\n" + $"MajorRuntimeVersion:\t{MajorRuntimeVersion:X}\n" + $"MinorRuntimeVersion:\t{MinorRuntimeVersion:X}\n" + $"MetaData:\t{MetaData}\n" + $"Flags:\t{Flags}\n" + $"EntryPointToken:\t{EntryPointToken:X}\n" + $"Resources:\t{Resources}\n" + $"StrongNameSignature:\t{StrongNameSignature}\n" + $"CodeManagerTable:\t{CodeManagerTable}\n" + $"VTableFixups:\t{VTableFixups}\n" + $"ExportAddressTableJumps:\t{ExportAddressTableJumps}\n" + $"ManagedNativeHeader:\t{ManagedNativeHeader}\n";
	}
}
