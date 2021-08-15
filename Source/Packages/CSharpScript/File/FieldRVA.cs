using System;
using System.IO;
namespace CSharpScript.File
{
	public class FieldRVA
	{
		public int RVA;
		public Field Field;
		public FieldRVARow Row;
		public int Token;
		public int Size;
		public byte[] Data;
		public FieldRVA(FieldRVARow row)
		{
			FieldRVARow fieldRVARow = (Row = row);
			Token = fieldRVARow.Index | 0x1D000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			RVA = Row.RVA;
			Field = metadata.TildeHeap.FieldTable.Fields[Row.Field - 1];
			Field.FieldToken = this;
		}
		public void ResolveData(Stream stream, MetadataRoot metadata)
		{
			stream.Position = metadata.PEFile.GetOffset(RVA);
			switch (Field.Signature.Type.ElementType)
			{
			case ElementType.ELEMENT_TYPE_VALUETYPE:
				Size = (Field.Signature.Type as TypeSig_ValueType).Encoded.Type.TypeDef.ClassLayout?.ClassSize ?? 0;
				break;
			case ElementType.ELEMENT_TYPE_I4:
				Size = 4;
				break;
			case ElementType.ELEMENT_TYPE_U4:
				Size = 4;
				break;
			case ElementType.ELEMENT_TYPE_I8:
				Size = 8;
				break;
			case ElementType.ELEMENT_TYPE_U8:
				Size = 8;
				break;
			case ElementType.ELEMENT_TYPE_PTR:
				Size = 8;
				break;
			case ElementType.ELEMENT_TYPE_FNPTR:
				Size = 0;
				break;
			case ElementType.ELEMENT_TYPE_BOOLEAN:
				Size = 1;
				break;
			default:
				throw new Exception();
			}
			Data = new byte[Size];
			stream.Read(Data, 0, Size);
		}
	}
}
