namespace CSharpScript.File
{
	public class TypeDefOrRefOrSpecEncoded
	{
		public TypeDefOrRefOrSpecFlag Flag;
		public static readonly int[] TableSigns = new int[3] { 2, 1, 27 };
		public int Token;
		public int Index;
		public TypeDefOrRef Type;
		public TypeDefOrRefOrSpecEncoded(byte[] bs, ref int index, MetadataRoot metadata)
		{
			int num = BlobHeap.ReadUnsigned(bs, ref index);
			Flag = (TypeDefOrRefOrSpecFlag)(num & 3);
			Index = num >> 2;
			Token = Index | (TableSigns[num & 3] << 24);
			Type = new TypeDefOrRef((Index << 2) | (int)Flag, metadata);
		}
		public TypeDefOrRefOrSpecEncoded(TypeDefOrRef type)
		{
			Type = type;
			switch (type.Flag)
			{
			case TypeDefOrRefFlag.TypeDef:
				Flag = TypeDefOrRefOrSpecFlag.TypeDef;
				Token = type.TypeDef.Token;
				break;
			case TypeDefOrRefFlag.TypeRef:
				Flag = TypeDefOrRefOrSpecFlag.TypeRef;
				Token = type.TypeRef.Token;
				break;
			case TypeDefOrRefFlag.TypeSpec:
				Flag = TypeDefOrRefOrSpecFlag.TypeSpec;
				Token = type.TypeSpec.Token;
				break;
			}
			Index = Token & 0xFFFFFF;
		}
		public override string ToString()
		{
			return Type.ToString();
		}
	}
}
