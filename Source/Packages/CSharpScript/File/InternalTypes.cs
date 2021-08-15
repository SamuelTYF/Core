namespace CSharpScript.File
{
	public class InternalTypes
	{
		public MetadataRoot MetadataRoot;
		private TypeDefOrRef _Boolean;
		private TypeDefOrRef _Byte;
		private TypeDefOrRef _Char;
		private TypeDefOrRef _Single;
		private TypeDefOrRef _Double;
		private TypeDefOrRef _Int16;
		private TypeDefOrRef _Int32;
		private TypeDefOrRef _Int64;
		private TypeDefOrRef _SByte;
		private TypeDefOrRef _UInt16;
		private TypeDefOrRef _UInt32;
		private TypeDefOrRef _UInt64;
		private TypeDefOrRef _String;
		private TypeDefOrRef _Object;
		private TypeDefOrRef _BooleanArray;
		private TypeDefOrRef _ByteArray;
		private TypeDefOrRef _CharArray;
		private TypeDefOrRef _SingleArray;
		private TypeDefOrRef _DoubleArray;
		private TypeDefOrRef _Int16Array;
		private TypeDefOrRef _Int32Array;
		private TypeDefOrRef _Int64Array;
		private TypeDefOrRef _SByteArray;
		private TypeDefOrRef _UInt16Array;
		private TypeDefOrRef _UInt32Array;
		private TypeDefOrRef _UInt64Array;
		private TypeDefOrRef _StringArray;
		private TypeDefOrRef _ObjectArray;
		public TypeDefOrRef Boolean => _Boolean ?? (_Boolean = MetadataRoot.FindType("System.Boolean"));
		public TypeDefOrRef Byte => _Byte ?? (_Byte = MetadataRoot.FindType("System.Byte"));
		public TypeDefOrRef Char => _Char ?? (_Char = MetadataRoot.FindType("System.Char"));
		public TypeDefOrRef Single => _Single ?? (_Single = MetadataRoot.FindType("System.Single"));
		public TypeDefOrRef Double => _Double ?? (_Double = MetadataRoot.FindType("System.Double"));
		public TypeDefOrRef Int16 => _Int16 ?? (_Int16 = MetadataRoot.FindType("System.Int16"));
		public TypeDefOrRef Int32 => _Int32 ?? (_Int32 = MetadataRoot.FindType("System.Int32"));
		public TypeDefOrRef Int64 => _Int64 ?? (_Int64 = MetadataRoot.FindType("System.Int64"));
		public TypeDefOrRef SByte => _SByte ?? (_SByte = MetadataRoot.FindType("System.SByte"));
		public TypeDefOrRef UInt16 => _UInt16 ?? (_UInt16 = MetadataRoot.FindType("System.UInt16"));
		public TypeDefOrRef UInt32 => _UInt32 ?? (_UInt32 = MetadataRoot.FindType("System.UInt32"));
		public TypeDefOrRef UInt64 => _UInt64 ?? (_UInt64 = MetadataRoot.FindType("System.UInt64"));
		public TypeDefOrRef String => _String ?? (_String = MetadataRoot.FindType("System.String"));
		public TypeDefOrRef Object => _Object ?? (_Object = MetadataRoot.FindType("System.Object"));
		public TypeDefOrRef BooleanArray => _BooleanArray ?? (_BooleanArray = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.Boolean)));
		public TypeDefOrRef ByteArray => _ByteArray ?? (_ByteArray = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.I1)));
		public TypeDefOrRef CharArray => _CharArray ?? (_CharArray = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.CHAR)));
		public TypeDefOrRef SingleArray => _SingleArray ?? (_SingleArray = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.R4)));
		public TypeDefOrRef DoubleArray => _DoubleArray ?? (_DoubleArray = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.R8)));
		public TypeDefOrRef Int16Array => _Int16Array ?? (_Int16Array = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.I2)));
		public TypeDefOrRef Int32Array => _Int32Array ?? (_Int32Array = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.I4)));
		public TypeDefOrRef Int64Array => _Int64Array ?? (_Int64Array = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.I8)));
		public TypeDefOrRef SByteArray => _SByteArray ?? (_SByteArray = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.U1)));
		public TypeDefOrRef UInt16Array => _UInt16Array ?? (_UInt16Array = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.U2)));
		public TypeDefOrRef UInt32Array => _UInt32Array ?? (_UInt32Array = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.U4)));
		public TypeDefOrRef UInt64Array => _UInt64Array ?? (_UInt64Array = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.U8)));
		public TypeDefOrRef StringArray => _StringArray ?? (_StringArray = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.STRING)));
		public TypeDefOrRef ObjectArray => _ObjectArray ?? (_ObjectArray = new TypeDefOrRef(MetadataRoot.RegisterTypeArray(TypeSig.OBJECT)));
		public InternalTypes(MetadataRoot metadata)
		{
			MetadataRoot = metadata;
		}
	}
}
