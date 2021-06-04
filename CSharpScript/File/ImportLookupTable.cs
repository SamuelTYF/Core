using System;
using System.IO;
using System.Text;
namespace CSharpScript.File
{
	public struct ImportLookupTable
	{
		public long RVA;
		public long HintNameTableRVA;
		public string Name;
		public static void CheckZero(byte[] bs, int offset, int end)
		{
			for (int i = offset; i < end; i++)
			{
				if (bs[i] != 0)
					throw new Exception();
			}
		}
		public ImportLookupTable(Stream stream, long rva, PEFile file)
		{
			RVA = rva;
			stream.Position = file.GetOffset(rva);
			byte[] array = new byte[8];
			stream.Read(array, 0, 8);
			HintNameTableRVA = array[0] | (array[1] << 8) | (array[2] << 16) | (array[3] << 24);
			CheckZero(array, 4, 8);
			stream.Position = file.GetOffset(HintNameTableRVA);
			stream.Read(array, 0, 2);
			CheckZero(array, 0, 2);
			StringBuilder stringBuilder = new StringBuilder();
			for (int num = stream.ReadByte(); num != 0; num = stream.ReadByte())
			{
				stringBuilder.Append((char)num);
			}
			Name = stringBuilder.ToString();
		}
		public override string ToString()
		{
			return $"RVA:\t{RVA:X}\tHintNameTableRVA:\t{HintNameTableRVA}\tName:\t{Name}";
		}
	}
}
