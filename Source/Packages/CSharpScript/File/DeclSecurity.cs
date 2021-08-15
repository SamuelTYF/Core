namespace CSharpScript.File
{
	public class DeclSecurity
	{
		public bool Resolved = false;
		public int Action;
		public HasDeclSecurity Parent;
		public PermissionSet PermissionSet;
		public DeclSecurityRow Row;
		public int Token;
		public DeclSecurity(DeclSecurityRow row)
			=>Token = (Row = row).Index | 0xE000000;
		public void ResolveSignature(MetadataRoot metadata)
		{
			if (!Resolved)
			{
				Action = Row.Action;
				Parent = new HasDeclSecurity(Row.Parent, metadata);
				int index = 0;
				bool Success = true;
				PermissionSet = new PermissionSet(metadata.BlobHeap.Values[Row.PermissionSet], ref index, metadata, ref Success);
				metadata.BlobHeap.ParsedValues[Row.PermissionSet] = this;
				if (Success)
					Resolved = true;                                          
			}
		}
	}
}
