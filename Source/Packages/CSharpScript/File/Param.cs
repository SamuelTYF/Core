namespace CSharpScript.File
{
	public class Param
	{
		public MethodDef Parent;
		public ParamAttributes Flags;
		public int Sequence;
		public string Name;
		public ParamRow Row;
		public int Token;
		public TypeSig Type;
		public Constant Constant;
		public MarshalSpec NativeType;
		public CustomAttribute ParamArray;
		public Param(ParamRow row)
		{
			Row = row;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Flags = (ParamAttributes)Row.Flags;
			Sequence = Row.Sequence;
			Name = metadata.StringsHeap[Row.Name];
		}
		public override string ToString()
		{
			string text = "";
			if (ParamArray != null)
				text = "params ";
			if (Sequence == 0)
			{
				if (Flags.HasFlag(ParamAttributes.HasFieldMarshal))
					return $"{text}[return:{PrintConfig._MarshalAsAttribute}({NativeType})]\n{Type}";
				return text + Type.ToString();
			}
			bool flag = true;
			if (Flags.HasFlag(ParamAttributes.In))
			{
				text += "[In] ";
				flag = false;
			}
			if (Flags.HasFlag(ParamAttributes.Out))
			{
				text += "[Out] ";
				flag = false;
			}
			if (Flags.HasFlag(ParamAttributes.HasFieldMarshal))
				text += $"[{PrintConfig._MarshalAsAttribute}({NativeType})] ";
			if (flag && Type is TypeSig_ByRef)
				text += "ref ";
			text = text + Type.ToString() + " " + Name;
			if (Flags.HasFlag(ParamAttributes.HasDefault))
				text += $" = {Constant}";
			return text;
		}
	}
}
