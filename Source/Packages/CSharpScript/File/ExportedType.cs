using System;
namespace CSharpScript.File
{
	public class ExportedType
	{
		public TypeAttributes Flags;
		public int TypeDefId;
		public string TypeName;
		public string TypeNamespace;
		public string FullName;
		public Implementation Implementation;
		public ExportedTypeRow Row;
		public int Token;
		public ManifestResource ManifestResource;
		public TypeDef Source;
		public Assembly Assembly;
		public ExportedType(ExportedTypeRow row)
		{
			ExportedTypeRow exportedTypeRow = (Row = row);
			Token = exportedTypeRow.Index | 0x27000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Flags = (TypeAttributes)Row.Flags;
			TypeDefId = Row.TypeDefId;
			TypeName = metadata.StringsHeap[Row.TypeName];
			TypeNamespace = metadata.StringsHeap[Row.TypeNamespace];
			Implementation = new Implementation(Row.Implementation, metadata);
			Assembly = metadata.TildeHeap.AssemblyTable.Assemblys[0];
			switch (Implementation.Flag)
			{
			case ImplementationFlag.AssemblyRef:
                FullName = TypeNamespace + "." + TypeName;
				if(Implementation.AssemblyRef.AssemblyFile==null)
                {
					break;
                }
                Source = Implementation.AssemblyRef.AssemblyFile.FindTypeDef(FullName);
				Source.ExportedType = this;
				break;
			case ImplementationFlag.ExportedType:
				FullName = Implementation.ExportedType.FullName + "+" + TypeName;
				Source = Implementation.ExportedType.Source.AssemblyFile.FindTypeDef(FullName);
				Source.ExportedType = this;
				break;
			default:
				throw new Exception();
			}
		}
        public override string ToString() => $"[System.Runtime.CompilerServices.TypeForwardedFromAttribute(\"{Assembly}\")]";
    }
}
