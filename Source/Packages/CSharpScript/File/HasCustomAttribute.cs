using System;
namespace CSharpScript.File
{
	public class HasCustomAttribute
	{
		public HasCustomAttributeFlag Flag;
		public MethodDef MethodDef;
		public Field Field;
		public TypeRef TypeRef;
		public TypeDef TypeDef;
		public Param Param;
		public InterfaceImpl InterfaceImpl;
		public MemberRef MemberRef;
		public Module Module;
		public int Permission;
		public Property Property;
		public Event Event;
		public StandAloneSig StandAloneSig;
		public ModuleRef ModuleRef;
		public TypeSpec TypeSpec;
		public Assembly Assembly;
		public AssemblyRef AssemblyRef;
		public File File;
		public ExportedType ExportedType;
		public ManifestResource ManifestResource;
		public GenericParam GenericParam;
		public GenericParamConstraint GenericParamConstraint;
		public MethodSpec MethodSpec;
		public HasCustomAttribute(int value, MetadataRoot metadata)
		{
			Flag = (HasCustomAttributeFlag)(value & 0x1F);
			switch (Flag)
			{
			case HasCustomAttributeFlag.MethodDef:
				MethodDef = metadata.TildeHeap.MethodDefTable.MethodDefs[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.Field:
				Field = metadata.TildeHeap.FieldTable.Fields[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.TypeRef:
				TypeRef = metadata.TildeHeap.TypeRefTable.TypeRefs[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.TypeDef:
				TypeDef = metadata.TildeHeap.TypeDefTable.TypeDefs[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.Param:
				Param = metadata.TildeHeap.ParamTable.Params[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.InterfaceImpl:
				InterfaceImpl = metadata.TildeHeap.InterfaceImplTable.InterfaceImpls[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.MemberRef:
				MemberRef = metadata.TildeHeap.MemberRefTable.MemberRefs[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.Module:
				Module = metadata.TildeHeap.ModuleTable.Modules[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.Permission:
				throw new Exception();
			case HasCustomAttributeFlag.Property:
				Property = metadata.TildeHeap.PropertyTable.Properties[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.Event:
				Event = metadata.TildeHeap.EventTable.Events[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.StandAloneSig:
				StandAloneSig = metadata.TildeHeap.StandAloneSigTable.StandAloneSigs[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.ModuleRef:
				ModuleRef = metadata.TildeHeap.ModuleRefTable.ModuleRefs[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.TypeSpec:
				TypeSpec = metadata.TildeHeap.TypeSpecTable.TypeSpecs[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.Assembly:
				Assembly = metadata.TildeHeap.AssemblyTable.Assemblys[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.AssemblyRef:
				AssemblyRef = metadata.TildeHeap.AssemblyRefTable.AssemblyRefs[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.File:
				File = metadata.TildeHeap.FileTable.Files[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.ExportedType:
				ExportedType = metadata.TildeHeap.ExportedTypeTable.ExportedTypes[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.ManifestResource:
				ManifestResource = metadata.TildeHeap.ManifestResourceTable.ManifestResources[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.GenericParam:
				GenericParam = metadata.TildeHeap.GenericParamTable.GenericParams[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.GenericParamConstraint:
				GenericParamConstraint = metadata.TildeHeap.GenericParamConstraintTable.GenericParamConstraints[(value >> 5) - 1];
				break;
			case HasCustomAttributeFlag.MethodSpec:
				MethodSpec = metadata.TildeHeap.MethodSpecTable.MethodSpecs[(value >> 5) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public override string ToString()
		{
			return Flag switch
			{
				HasCustomAttributeFlag.MethodDef => MethodDef.ToString(), 
				HasCustomAttributeFlag.Field => Field.ToString(), 
				HasCustomAttributeFlag.TypeRef => TypeRef.ToString(), 
				HasCustomAttributeFlag.TypeDef => TypeDef.ToString(), 
				HasCustomAttributeFlag.Param => Param.ToString(), 
				HasCustomAttributeFlag.InterfaceImpl => InterfaceImpl.ToString(), 
				HasCustomAttributeFlag.MemberRef => MemberRef.ToString(), 
				HasCustomAttributeFlag.Module => Module.ToString(), 
				HasCustomAttributeFlag.Permission => Permission.ToString(), 
				HasCustomAttributeFlag.Property => Property.ToString(), 
				HasCustomAttributeFlag.Event => Event.ToString(), 
				HasCustomAttributeFlag.StandAloneSig => StandAloneSig.ToString(), 
				HasCustomAttributeFlag.ModuleRef => ModuleRef.ToString(), 
				HasCustomAttributeFlag.TypeSpec => TypeSpec.ToString(), 
				HasCustomAttributeFlag.Assembly => Assembly.ToString(), 
				HasCustomAttributeFlag.AssemblyRef => AssemblyRef.ToString(), 
				HasCustomAttributeFlag.File => File.ToString(), 
				HasCustomAttributeFlag.ExportedType => ExportedType.ToString(), 
				HasCustomAttributeFlag.ManifestResource => ManifestResource.ToString(), 
				HasCustomAttributeFlag.GenericParam => GenericParam.ToString(), 
				HasCustomAttributeFlag.GenericParamConstraint => GenericParamConstraint.ToString(), 
				HasCustomAttributeFlag.MethodSpec => MethodSpec.ToString(), 
				_ => throw new Exception(), 
			};
		}
	}
}
