using System;
namespace CSharpScript.File
{
	[Flags]
	public enum TypeAttributes : uint
	{
		NotPublic = 0x0u,
		Public = 0x1u,
		NestedPublic = 0x2u,
		NestedPrivate = 0x3u,
		NestedFamily = 0x4u,
		NestedAssembly = 0x5u,
		NestedFamANDAssem = 0x6u,
		NestedFamORAssem = 0x7u,
		LayoutMask = 0x18u,
		AutoLayout = 0x0u,
		SequentialLayout = 0x8u,
		ExplicitLayout = 0x10u,
		ClassSemanticsMask = 0x20u,
		Class = 0x0u,
		Interface = 0x20u,
		Abstract = 0x80u,
		Sealed = 0x100u,
		SpecialName = 0x400u,
		Import = 0x1000u,
		Serializable = 0x2000u,
		StringFormatMask = 0x30000u,
		AnsiClass = 0x0u,
		UnicodeClass = 0x10000u,
		AutoClass = 0x20000u,
		CustomFormatClass = 0x30000u,
		CustomStringFormatMask = 0xC00000u,
		BeforeFieldInit = 0x100000u,
		RTSpecialName = 0x800u,
		HasSecurity = 0x40000u,
		IsTypeForwarder = 0x200000u
	}
}
