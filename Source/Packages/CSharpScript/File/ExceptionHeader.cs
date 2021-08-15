using System.IO;
namespace CSharpScript.File
{
	public struct ExceptionHeader
	{
		public CorILMethodSectFlags Kind;
		public int DataSize;
		public ExceptionClause[] Clauses;
		public ExceptionHeader(Stream stream, MetadataRoot metadata)
		{
			byte[] array = new byte[4];
			stream.Read(array, 0, 4);
			Kind = (CorILMethodSectFlags)array[0];
			DataSize = array[1] | (array[2] << 8) | (array[3] << 16);
			if (Kind.HasFlag(CorILMethodSectFlags.CorILMethod_Sect_FatFormat))
			{
				int num = (DataSize - 4) / 24;
				Clauses = new ExceptionClause[num];
				array = new byte[24];
				for (int i = 0; i < num; i++)
				{
					stream.Read(array, 0, 24);
					Clauses[i] = new ExceptionClause(array, metadata);
				}
			}
			else
			{
				int num2 = (DataSize - 4) / 12;
				Clauses = new ExceptionClause[num2];
				array = new byte[12];
				for (int j = 0; j < num2; j++)
				{
					stream.Read(array, 0, 12);
					Clauses[j] = new ExceptionClause(array, metadata);
				}
			}
		}
		public override string ToString()
		{
			return $"Kind:\t{Kind}\n" + $"DataSize:\t{DataSize}\n" + "Clauses:\n" + string.Join("\n", Clauses) + "\n";
		}
	}
}
