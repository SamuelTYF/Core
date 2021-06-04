using System;
using System.IO;
using System.Text;
namespace CSharpScript.File
{
	public struct ImportTable
	{
		public int RVA;
		public int ImportLookupTableRVA;
		public int DateTimeStamp;
		public int ForwarderChain;
		public long NameRVA;
		public int ImportAddressTableRVA;
		public string Name;
		public ImportLookupTable ImportLookupTable;
		public IAT IAT;
		public static void CheckZero(byte[] bs, int offset, int end)
		{
			for (int i = offset; i < end; i++)
			{
				if (bs[i] != 0)
					throw new Exception();
			}
		}
		public ImportTable(Stream stream, int rva, PEFile file)
		{
			RVA = rva;
			stream.Position = file.GetOffset(rva);
			byte[] array = new byte[20];
			stream.Read(array, 0, 20);
			ImportLookupTableRVA = array[0] | (array[1] << 8) | (array[2] << 16) | (array[3] << 24);
			DateTimeStamp = array[4] | (array[5] << 8) | (array[6] << 16) | (array[7] << 24);
			ForwarderChain = array[8] | (array[9] << 8) | (array[10] << 16) | (array[11] << 24);
			NameRVA = array[12] | (array[13] << 8) | (array[14] << 16) | (array[15] << 24);
			ImportAddressTableRVA = array[16] | (array[17] << 8) | (array[18] << 16) | (array[19] << 24);
			ImportLookupTable = new ImportLookupTable(stream, ImportLookupTableRVA, file);
			IAT = new IAT(stream, ImportAddressTableRVA, file);
			stream.Position = file.GetOffset(NameRVA);
			StringBuilder stringBuilder = new StringBuilder();
			for (int num = stream.ReadByte(); num != 0; num = stream.ReadByte())
			{
				stringBuilder.Append((char)num);
			}
			Name = stringBuilder.ToString();
		}
		public override string ToString()
		{
			return $"ImportLookupTable:\t{ImportLookupTable}\n" + $"DateTimeStamp:\t{DateTimeStamp}\n" + $"ForwarderChain:\t{ForwarderChain}\n" + "Name:\t" + Name + "\n" + $"ImportLookupTable:\t{ImportLookupTable}\n" + $"IAT:\t{IAT}\n";
		}
	}
}
