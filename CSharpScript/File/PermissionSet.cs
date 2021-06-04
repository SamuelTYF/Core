using System;
namespace CSharpScript.File
{
	public class PermissionSet
	{
		public PermissionSetAttribute[] Attributes;
		public PermissionSet(byte[] bs, ref int index, MetadataRoot metadata, ref bool Success)
		{
			if (bs[index++] != 46)
				throw new Exception();
			int num = BlobHeap.ReadUnsigned(bs, ref index);
			Attributes = new PermissionSetAttribute[num];
			int num2 = 0;
			while (Success && num2 < num)
			{
				Attributes[num2] = new PermissionSetAttribute(bs, ref index, metadata, ref Success);
				num2++;
			}
			if (!Success || bs.Length == index)
				return;
			throw new Exception();
		}
	}
}
