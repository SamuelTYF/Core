namespace CSharpScript.DomTree
{
	public enum CorTokenType
	{
		mdtModule = 0,
		mdtTypeRef = 0x1000000,
		mdtTypeDef = 0x2000000,
		mdtFieldDef = 0x4000000,
		mdtMethodDef = 100663296,
		mdtParamDef = 0x8000000,
		mdtInterfaceImpl = 150994944,
		mdtMemberRef = 167772160,
		mdtCustomAttribute = 201326592,
		mdtPermission = 234881024,
		mdtSignature = 285212672,
		mdtEvent = 335544320,
		mdtProperty = 385875968,
		mdtModuleRef = 436207616,
		mdtTypeSpec = 452984832,
		mdtAssembly = 0x20000000,
		mdtAssemblyRef = 587202560,
		mdtFile = 637534208,
		mdtExportedType = 654311424,
		mdtManifestResource = 671088640,
		mdtGenericParam = 704643072,
		mdtMethodSpec = 721420288,
		mdtGenericParamConstraint = 738197504,
		mdtString = 1879048192,
		mdtName = 1895825408,
		mdtBaseType = 1912602624
	}
}
