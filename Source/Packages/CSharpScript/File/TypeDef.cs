using System;
using Collection;
namespace CSharpScript.File
{
	public class TypeDef
	{
		public TypeAttributes Flags;
		public string TypeName;
		public string TypeNamespace;
		public TypeDefOrRef Extends;
		public List<GenericParam> GenericParams = new();
		public Field[] FieldList;
		public MethodDef[] MethodList;
		public Event[] Events;
		public Property[] Properties;
		public TypeDefRow Row;
		public int Token;
		public TypeDef EnclosingClass;
		public string FullName;
		public int Hash;
		public string QualifiedName;
		public List<TypeDefOrRef> Interfaces = new();
		public List<TypeDef> NestedClasses = new();
		public List<CustomAttribute> CustomAttributes = new();
		public DeclSecurity DeclSecurity;
		public ClassLayout ClassLayout;
		public ExportedType ExportedType;
		public MetadataRoot AssemblyFile;
		public TypeDef(TypeDefRow row)
		{
			TypeDefRow typeDefRow = (Row = row);
			Token = typeDefRow.Index | 0x2000000;
		}
		public void ResolveSignature(MetadataRoot metadata, int nextfield, int nextmethod)
		{
			AssemblyFile = metadata;
			Flags = (TypeAttributes)Row.Flags;
			TypeName = metadata.StringsHeap[Row.TypeName];
			TypeNamespace = metadata.StringsHeap[Row.TypeNamespace];
			Extends = ((Row.Extends == 0) ? null : new TypeDefOrRef(Row.Extends, metadata));
			int num = nextfield - Row.FieldList;
			FieldList = new Field[num];
			if (num != 0)
				Array.Copy(metadata.TildeHeap.FieldTable.Fields, Row.FieldList - 1, FieldList, 0, num);
			for (int i = 0; i < num; i++)
			{
				FieldList[i].Parent = this;
				FieldList[i].Index = i;
			}
			num = nextmethod - Row.MethodList;
			MethodList = new MethodDef[num];
			if (num != 0)
				Array.Copy(metadata.TildeHeap.MethodDefTable.MethodDefs, Row.MethodList - 1, MethodList, 0, num);
			for (int j = 0; j < num; j++)
			{
				MethodList[j].Parent = this;
				MethodList[j].Index = j;
			}
			if (Row.TypeNamespace != 0)
			{
				FullName = TypeNamespace + "." + TypeName;
				Hash = FullName.GetHashCode();
				if (metadata.QueryTree.ContainsKey(Hash))
					throw new Exception();
				metadata.QueryTree[Hash] = Token;
			}
			else
				FullName = TypeName;
		}
        public override string ToString() => FullName;
        public string GetInformation()
		{
			string text = "";
			if (CustomAttributes.Length != 0)
				text = text + string.Join("\n", CustomAttributes) + "\n";
			if (DeclSecurity != null)
				text += $"{DeclSecurity.PermissionSet}\n";
			if (ClassLayout != null)
				text += $"{ClassLayout}\n";
			if (ExportedType != null)
				text += $"{ExportedType}\n";
			text += FullName;
			if (Interfaces.Length != 0)
				text = text + ":" + string.Join(",", Interfaces);
			return text;
		}
		public MethodDef[] GetMethod(string name)
        {
			List<MethodDef> methods = new();
			foreach (MethodDef method in MethodList)
				if (method.Name == name)
					methods.Add(method);
			return methods.ToArray();
        }
	}
}
