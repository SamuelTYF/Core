using System;
namespace CSharpScript.File
{
	public struct ExceptionClause
	{
		public ExceptionHandlingFlags Flags;
		public int TryOffset;
		public int TryLength;
		public int HandlerOffset;
		public int HandlerLength;
		public int ClassToken;
		public int FilterOffset;
		public TypeDefOrRef Type;
		public ExceptionClause(byte[] bs, MetadataRoot metadata)
		{
			if (bs.Length == 24)
			{
				Flags = (ExceptionHandlingFlags)(bs[0] | (bs[1] << 8) | (bs[2] << 16) | (bs[3] << 24));
				TryOffset = bs[4] | (bs[5] << 8) | (bs[6] << 16) | (bs[7] << 24);
				TryLength = bs[8] | (bs[9] << 8) | (bs[10] << 16) | (bs[11] << 24);
				HandlerOffset = bs[12] | (bs[13] << 8) | (bs[14] << 16) | (bs[15] << 24);
				HandlerLength = bs[16] | (bs[17] << 8) | (bs[18] << 16) | (bs[19] << 24);
				ClassToken = bs[20] | (bs[21] << 8) | (bs[22] << 16) | (bs[23] << 24);
				FilterOffset = bs[20] | (bs[21] << 8) | (bs[22] << 16) | (bs[23] << 24);
			}
			else
			{
				Flags = (ExceptionHandlingFlags)(bs[0] | (bs[1] << 8));
				TryOffset = bs[2] | (bs[3] << 8);
				TryLength = bs[4];
				HandlerOffset = bs[5] | (bs[6] << 8);
				HandlerLength = bs[7];
				ClassToken = bs[8] | (bs[9] << 8) | (bs[10] << 16) | (bs[11] << 24);
				FilterOffset = bs[8] | (bs[9] << 8) | (bs[10] << 16) | (bs[11] << 24);
			}
			if (Flags == ExceptionHandlingFlags.COR_ILEXCEPTION_CLAUSE_EXCEPTION)
			{
				switch (ClassToken >> 24)
				{
				case 1:
					Type = new TypeDefOrRef(metadata.TildeHeap.TypeRefTable.TypeRefs[(ClassToken & 0xFFFFFF) - 1]);
					break;
				case 2:
					Type = new TypeDefOrRef(metadata.TildeHeap.TypeDefTable.TypeDefs[(ClassToken & 0xFFFFFF) - 1]);
					break;
				default:
					throw new Exception();
				}
			}
			else
				Type = null;
		}
		public override string ToString()
		{
			return $"Flags:\t{Flags}\n" + $"TryOffset:\t{TryOffset:X}\n" + $"TryLength:\t{TryLength:X}\n" + $"HandlerOffset:\t{HandlerOffset:X}\n" + $"HandlerLength:\t{HandlerLength:X}\n" + $"ClassToken:\t{ClassToken:X}\n" + $"FilterOffset:\t{FilterOffset:X}\n";
		}
	}
}
