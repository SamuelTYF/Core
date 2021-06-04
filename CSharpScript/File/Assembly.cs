using System;
using Collection;
namespace CSharpScript.File
{
	public class Assembly
	{
		public AssemblyHashAlgorithm HashAlgId;
		public int MajorVersion;
		public int MinorVersion;
		public int BuildNumber;
		public int RevisionNumber;
		public AssemblyFlags Flags;
		public byte[] PublicKey;
		public string Name;
		public string Culture;
		public AssemblyRow Row;
		public int Token;
		public byte[] PublicKeyToken;
		public string FullName;
		public string NullTokenName;
		public int Hash;
		public List<CustomAttribute> CustomAttributes = new();
		public DeclSecurity DeclSecurity;
		public Assembly(AssemblyRow row)
			=>Token = (Row = row).Index | 0x20000000;
		public void ResolveSignature(MetadataRoot metadata)
		{
			HashAlgId = (AssemblyHashAlgorithm)Row.HashAlgId;
			MajorVersion = Row.MajorVersion;
			MinorVersion = Row.MinorVersion;
			BuildNumber = Row.BuildNumber;
			RevisionNumber = Row.RevisionNumber;
			Flags = (AssemblyFlags)Row.Flags;
			PublicKey = metadata.BlobHeap.Values[Row.PublicKey];
			Name = metadata.StringsHeap[Row.Name];
			Culture = ((Row.Culture == 0) ? "neutral" : metadata.StringsHeap[Row.Culture]);
			byte[] array = HashAlgId switch
			{
				AssemblyHashAlgorithm.SHA1 => MetadataRoot._SHA1.ComputeHash(PublicKey), 
				AssemblyHashAlgorithm.SHA256 => MetadataRoot._SHA256.ComputeHash(PublicKey), 
				_ => throw new Exception(), 
			};
			PublicKeyToken = new byte[8];
			for (int i = 0; i < 8; i++)
				PublicKeyToken[i] = array[array.Length - 1 - i];
			FullName = $"{Name}, Version={MajorVersion}.{MinorVersion}.{BuildNumber}.{RevisionNumber}, Culture={Culture}, PublicKeyToken={BlobHeap.GetKey(PublicKeyToken)}";
			NullTokenName = $"{Name}, Version={MajorVersion}.{MinorVersion}.{BuildNumber}.{RevisionNumber}, Culture={Culture}, PublicKeyToken=null";
			Hash = FullName.GetHashCode();
		}
        public override string ToString() => FullName;
        public string GetInformation()
		{
			string text = "";
			if (CustomAttributes.Length != 0)
				text = text + string.Join("\n", CustomAttributes) + "\n";
			if (DeclSecurity != null)
				text += $"{DeclSecurity.PermissionSet}\n";
			return text + ".assembly " + Name;
		}
	}
}
