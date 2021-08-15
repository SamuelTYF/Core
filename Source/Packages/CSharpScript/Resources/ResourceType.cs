namespace CSharpScript.Resources
{
	public class ResourceType
	{
		public ResourceTypeCode Code;
		public string TypeName;
		public static readonly ResourceType Null = new(ResourceTypeCode.Null, "Null");
		public static readonly ResourceType String = new(ResourceTypeCode.String, "System.String");
		public static readonly ResourceType Boolean = new(ResourceTypeCode.Boolean, "System.Boolean");
		public static readonly ResourceType Char = new(ResourceTypeCode.Char, "System.Char");
		public static readonly ResourceType Byte = new(ResourceTypeCode.Byte, "System.Byte");
		public static readonly ResourceType SByte = new(ResourceTypeCode.SByte, "System.SByte");
		public static readonly ResourceType Int16 = new(ResourceTypeCode.Int16, "System.Int16");
		public static readonly ResourceType UInt16 = new(ResourceTypeCode.UInt16, "System.UInt16");
		public static readonly ResourceType Int32 = new(ResourceTypeCode.Int32, "System.Int32");
		public static readonly ResourceType UInt32 = new(ResourceTypeCode.UInt32, "System.UInt32");
		public static readonly ResourceType Int64 = new(ResourceTypeCode.Int64, "System.Int64");
		public static readonly ResourceType UInt64 = new(ResourceTypeCode.UInt64, "System.UInt64");
		public static readonly ResourceType Single = new(ResourceTypeCode.Single, "System.Single");
		public static readonly ResourceType Double = new(ResourceTypeCode.Double, "System.Double");
		public static readonly ResourceType Decimal = new(ResourceTypeCode.Decimal, "System.Decimal");
		public static readonly ResourceType DateTime = new(ResourceTypeCode.DateTime, "System.DateTime");
		public static readonly ResourceType TimeSpan = new(ResourceTypeCode.TimeSpan, "System.TimeSpan");
		public static readonly ResourceType ByteArray = new(ResourceTypeCode.ByteArray, "System.ByteArray");
		public static readonly ResourceType Stream = new(ResourceTypeCode.Stream, "System.Stream");
		public ResourceType(ResourceTypeCode code, string name)
		{
			Code = code;
			TypeName = name;
		}
        public override string ToString() => TypeName;
    }
}
