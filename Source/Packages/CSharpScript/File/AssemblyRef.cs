using System;
using Collection;
namespace CSharpScript.File
{
	public class AssemblyRef
	{
		public int MajorVersion;
		public int MinorVersion;
		public int BuildNumber;
		public int RevisionNumber;
		public AssemblyFlags Flags;
		public byte[] PublicKeyToken;
		public string Name;
		public string Culture;
		public byte[] HashValue;
		public AssemblyRefRow Row;
		public int Token;
		public MetadataRoot ThisAssemblyFile;
		public MetadataRoot AssemblyFile;
		public Assembly Assembly;
		public ManifestResource ManifestResource;
		public string FullName;
		public int Hash;
		public AssemblyRef(AssemblyRefRow row)
			=>Token = (Row = row).Index | 0x23000000;
		public void ResolveSignature(MetadataRoot metadata)
		{
			ThisAssemblyFile = metadata;
			MajorVersion = Row.MajorVersion;
			MinorVersion = Row.MinorVersion;
			BuildNumber = Row.BuildNumber;
			RevisionNumber = Row.RevisionNumber;
			Flags = (AssemblyFlags)Row.Flags;
			PublicKeyToken = metadata.BlobHeap.Values[Row.PublicKeyToken];
			Name = metadata.StringsHeap[Row.Name];
			Culture = ((Row.Culture == 0) ? "neutral" : metadata.StringsHeap[Row.Culture]);
			HashValue = metadata.BlobHeap.Values[Row.HashValue];
			FullName = $"{Name}, Version={MajorVersion}.{MinorVersion}.{BuildNumber}.{RevisionNumber}, Culture={Culture}, PublicKeyToken={BlobHeap.GetKey(PublicKeyToken)}";
			Hash = FullName.GetHashCode();
			Link(metadata.PEFile.PEManager);
		}
		public bool Link(PEManager manager)
		{
			if (manager.PEFiles.ContainsKey(Hash))
			{
				AssemblyFile = manager.PEFiles[Hash].MetadataRoot;
				Assembly = AssemblyFile.TildeHeap.AssemblyTable.Assemblys[0];
                return Assembly.Name != Name ? throw new Exception() : true;
            }
            if (!manager.AssemblyRefWaitingList.ContainsKey(Hash))
				manager.AssemblyRefWaitingList[Hash] = new Stack<AssemblyRef>();
			manager.AssemblyRefWaitingList[Hash].Insert(this);
			return false;
		}
        public override string ToString() => FullName;
    }
}
