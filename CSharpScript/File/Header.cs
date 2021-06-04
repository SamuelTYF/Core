using System;
using System.IO;
namespace CSharpScript.File
{
	public struct Header
	{
		public MachineTypes Machine;
		public int NumberOfSections;
		public DateTime TimeStamp;
		public int PointerToSymbolTable;
		public int NumberOfSymbols;
		public int OptionalHeaderSize;
		public Characteristics Characteristics;
		public Header(Stream stream)
		{
			byte[] array = new byte[20];
			stream.Read(array, 0, 20);
			Machine = (MachineTypes)(array[0] | (array[1] << 8));
			NumberOfSections = array[2] | (array[3] << 8);
			TimeStamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((long)(array[4] | ((ulong)array[5] << 8) | ((ulong)array[6] << 16) | ((ulong)array[7] << 24)));
			PointerToSymbolTable = array[8] | (array[9] << 8) | (array[10] << 16) | (array[11] << 24);
			NumberOfSymbols = array[12] | (array[13] << 8) | (array[14] << 16) | (array[15] << 24);
			OptionalHeaderSize = array[16] | (array[17] << 8);
			Characteristics = new Characteristics((ushort)(array[18] | (array[19] << 8)));
		}
		public override string ToString()
		{
			return $"Machine:\t{Machine}\n" + $"NumberOfSections:\t{NumberOfSections}\n" + $"TimeStamp:\t{TimeStamp}\n" + $"PointerToSymbolTable:\t{PointerToSymbolTable}\n" + $"NumberOfSymbols:\t{NumberOfSymbols}\n" + $"OptionalHeaderSize:\t{OptionalHeaderSize}\n" + $"Characteristics:\t{Characteristics}\n";
		}
	}
}
