using System;
namespace CSharpScript.File
{
	public class Implementation
	{
		public ImplementationFlag Flag;
		public File File;
		public AssemblyRef AssemblyRef;
		public ExportedType ExportedType;
		public Implementation(int value, MetadataRoot metadata)
		{
			Flag = (ImplementationFlag)(value & 3);
			switch (Flag)
			{
			case ImplementationFlag.File:
				File = metadata.TildeHeap.FileTable.Files[(value >> 2) - 1];
				break;
			case ImplementationFlag.AssemblyRef:
				AssemblyRef = metadata.TildeHeap.AssemblyRefTable.AssemblyRefs[(value >> 2) - 1];
				break;
			case ImplementationFlag.ExportedType:
				ExportedType = metadata.TildeHeap.ExportedTypeTable.ExportedTypes[(value >> 2) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			return Flag switch
			{
				ImplementationFlag.File => File.ToString(), 
				ImplementationFlag.AssemblyRef => AssemblyRef.ToString(), 
				ImplementationFlag.ExportedType => ExportedType.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
