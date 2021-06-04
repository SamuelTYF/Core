using Collection;
namespace CSharpScript.File
{
	public class Field
	{
		public int Index;
		public TypeDef Parent;
		public FieldAttributes Flags;
		public string Name;
		public FieldSig Signature;
		public FieldRow Row;
		public int Token;
		public string FullName;
		public Constant Constant;
		public FieldRVA FieldToken;
		public MarshalSpec NativeType;
		public List<CustomAttribute> CustomAttributes = new List<CustomAttribute>();
		public FieldMarshal FieldMarshal;
		public FieldLayout FieldLayout;
		public Field(FieldRow row)
		{
			FieldRow fieldRow = (Row = row);
			Token = fieldRow.Index | 0x4000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Flags = (FieldAttributes)Row.Flags;
			Name = metadata.StringsHeap[Row.Name];
			byte[] bs = metadata.BlobHeap.Values[Row.Signature];
			int index = 0;
			Signature = FieldSig.AnalyzeFieldSig(bs, ref index, metadata);
		}
		public override string ToString()
		{
			return $"{Signature.Type} {Parent}.{Name}";
		}
		public string GetInformation()
		{
			string text = "";
			if (CustomAttributes.Length != 0)
				text = text + string.Join("\n", CustomAttributes) + "\n";
			if (Flags.HasFlag(FieldAttributes.HasFieldMarshal))
				text += $"[{PrintConfig._MarshalAsAttribute}({NativeType})] ";
			if (Flags.HasFlag(FieldAttributes.NotSerialized))
				text += PrintConfig._NonSerialized;
			if (FieldLayout != null)
				text += $"{FieldLayout}\n";
			if (Flags.HasFlag(FieldAttributes.Private))
				text += "private ";
			if (Flags.HasFlag(FieldAttributes.Public))
				text += "public ";
			if (Flags.HasFlag(FieldAttributes.Static))
				text += "static ";
			if (Flags.HasFlag(FieldAttributes.InitOnly))
				text += "readonly ";
			if (Flags.HasFlag(FieldAttributes.HasDefault))
				return $"{text}{this} = {Constant}";
			return $"{text}{this}";
		}
	}
}
