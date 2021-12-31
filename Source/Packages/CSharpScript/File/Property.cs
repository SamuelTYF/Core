using System;
using Collection;
namespace CSharpScript.File
{
	public class Property
	{
		public PropertyAttributes Flags;
		public string Name;
		public PropertySig Type;
		public PropertyRow Row;
		public int Token;
		public MethodSemantic Setter;
		public MethodSemantic Getter;
		public int Index;
		public TypeDef Parent;
		public bool IsThis;
		public List<CustomAttribute> CustomAttributes = new();
		public Property(PropertyRow row)
		{
			PropertyRow propertyRow = (Row = row);
			Token = propertyRow.Index | 0x17000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Flags = (PropertyAttributes)Row.Flags;
			Name = metadata.StringsHeap[Row.Name];
			int index = 0;
			Type = new PropertySig(metadata.BlobHeap.Values[Row.Type], ref index, metadata);
			metadata.BlobHeap.ParsedValues[Row.Type] = this;
			IsThis = Type.Params.Length != 0;
			if (Flags.HasFlag(PropertyAttributes.HasDefault))
				throw new Exception();
		}
        public override string ToString() => $"{Type.Type} {Parent}.{Name}";
        public string GetThisInformation()
		{
			if (Setter != null)
				return "this[" + string.Join(",", (System.Collections.Generic.IEnumerable<Param>)Setter.Method.ParamList) + "]";
			string text = $"this[{Getter.Method.ParamList[0]}";
			for (int i = 1; i < Getter.Method.ParamList.Length - 1; i++)
			{
				text += $",{Getter.Method.ParamList[i]}";
			}
			return text + "]";
		}
        public string GetSetterInformation() => Setter.Method.CustomAttributes.Length == 0 ? "set;" : "\n" + string.Join("\n", Setter.Method.CustomAttributes) + "\nset;";
        public string GetGetterInformation()
		{
			if (Getter.Method.CustomAttributes.Length == 0)
				return "get;";
			return "\n" + string.Join("\n", Getter.Method.CustomAttributes) + "\nget;";
		}
		public string GetInformation()
		{
			return ((CustomAttributes.Length != 0) ? (string.Join("\n", CustomAttributes) + "\n") : "") + string.Format("{0} {1}.{2}{{{3}{4}}}", Type.Type, Parent, IsThis ? GetThisInformation() : Name, (Setter != null) ? GetSetterInformation() : "", (Getter != null) ? GetGetterInformation() : "");
		}
	}
}
