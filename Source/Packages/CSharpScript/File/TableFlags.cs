using System;
namespace CSharpScript.File
{
	[Flags]
	public enum TableFlags : long
	{
		Module = 0x1L,
		TypeRef = 0x2L,
		TypeDef = 0x4L,
		Field = 0x10L,
		MethodDef = 0x40L,
		Param = 0x100L,
		InterfaceImpl = 0x200L,
		MemberRef = 0x400L,
		Constant = 0x800L,
		CustomAttribute = 0x1000L,
		FieldMarshal = 0x2000L,
		DeclSecurity = 0x4000L,
		ClassLayout = 0x8000L,
		FieldLayout = 0x10000L,
		StandAloneSig = 0x20000L,
		EventMap = 0x40000L,
		Event = 0x100000L,
		PropertyMap = 0x200000L,
		Property = 0x800000L,
		MethodSemantics = 0x1000000L,
		MethodImpl = 0x2000000L,
		ModuleRef = 0x4000000L,
		TypeSpec = 0x8000000L,
		ImplMap = 0x10000000L,
		FieldRVA = 0x20000000L,
		Assembly = 0x100000000L,
		AssemblyProcessor = 0x200000000L,
		AssemblyOS = 0x400000000L,
		AssemblyRef = 0x800000000L,
		AssemblyRefProcessor = 0x1000000000L,
		AssemblyRefOS = 0x2000000000L,
		File = 0x4000000000L,
		ExportedType = 0x8000000000L,
		ManifestResource = 0x10000000000L,
		NestedClass = 0x20000000000L,
		GenericParam = 0x40000000000L,
		MethodSpec = 0x80000000000L,
		GenericParamConstraint = 0x100000000000L
	}
}
