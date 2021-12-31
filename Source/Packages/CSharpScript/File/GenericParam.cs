using System;
using Collection;
namespace CSharpScript.File
{
	public class GenericParam
	{
		public int Number;
		public GenericParamAttributes Flags;
		public TypeOrMethodDef Owner;
		public string Name;
		public GenericParamRow Row;
		public int Token;
		public List<GenericParamConstraint> Constraints = new();
		public List<GenericParamConstraint> RealConstraints = new();
		public bool Covariant;
		public bool Contravariant;
		public bool IsClass;
		public bool IsStruct;
		public bool HasNew;
		public GenericParam(GenericParamRow row)
		{
			GenericParamRow genericParamRow = (Row = row);
			Token = genericParamRow.Index | 0x2A000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Number = Row.Number;
			Flags = (GenericParamAttributes)Row.Flags;
			Owner = new TypeOrMethodDef(Row.Owner, metadata);
			Name = metadata.StringsHeap[Row.Name];
			switch (Owner.Flag)
			{
			case TypeOrMethodDefFlag.TypeDef:
				if (Owner.TypeDef.GenericParams.Length != Number)
					throw new Exception();
				Owner.TypeDef.GenericParams.Add(this);
				break;
			case TypeOrMethodDefFlag.MethodDef:
				if (Owner.MethodDef.GenericParams.Length != Number)
					throw new Exception();
				Owner.MethodDef.GenericParams.Add(this);
				break;
			default:
				throw new Exception();
			}
			if (Flags.HasFlag(GenericParamAttributes.Covariant) && Flags.HasFlag(GenericParamAttributes.Contravariant))
				throw new Exception();
			Covariant = Flags.HasFlag(GenericParamAttributes.Covariant);
			Contravariant = Flags.HasFlag(GenericParamAttributes.Contravariant);
			IsClass = Flags.HasFlag(GenericParamAttributes.ReferenceTypeConstraint);
			IsStruct = Flags.HasFlag(GenericParamAttributes.NotNullableValueTypeConstraint);
			HasNew = Flags.HasFlag(GenericParamAttributes.DefaultConstructorConstraint);
		}
		public override string ToString()
		{
			if (Covariant)
				return "out " + Name;
			if (Contravariant)
				return "in " + Name;
			return Name;
		}
		public string GetConstraint()
		{
			List<string> list = new();
			if (IsClass)
				list.Add("struct");
			if (IsStruct)
				list.Add("struct");
			foreach (GenericParamConstraint constraint in Constraints)
			{
				list.Add(constraint.ToString());
			}
			if (!IsClass && !IsStruct && HasNew)
				list.Add("new()");
			if (list.Length == 0)
				return "";
			return "where " + Name + " : " + string.Join(",", list);
		}
	}
}
