namespace CSharpScript.File
{
	public class PermissionSetAttribute
	{
		public string Name;
		public NamedArg[] NamedArgs;
		public PermissionSetAttribute(byte[] bs, ref int index, MetadataRoot metadata, ref bool Success)
		{
			Name = BlobHeap.ReadString(bs, ref index, BlobHeap.ReadUnsigned(bs, ref index));
			int num = BlobHeap.ReadUnsigned(bs, ref index);
			int num2 = BlobHeap.ReadUnsigned(bs, ref index);
			NamedArgs = new NamedArg[num2];
			int num3 = 0;
			while (Success && num3 < num2)
			{
				NamedArgs[num3] = new NamedArg(bs, ref index, metadata, ref Success);
				num3++;
			}
		}
	}
}
