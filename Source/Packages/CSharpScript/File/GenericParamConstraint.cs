namespace CSharpScript.File
{
	public class GenericParamConstraint
	{
		public GenericParam Owner;
		public TypeDefOrRef Constraint;
		public GenericParamConstraintRow Row;
		public int Token;
		public GenericParamConstraint(GenericParamConstraintRow row)
		{
			GenericParamConstraintRow genericParamConstraintRow = (Row = row);
			Token = genericParamConstraintRow.Index | 0x2C000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			Owner = metadata.TildeHeap.GenericParamTable.GenericParams[Row.Owner - 1];
			Constraint = new TypeDefOrRef(Row.Constraint, metadata);
			Owner.RealConstraints.Add(this);
			switch (Constraint.Flag)
			{
			case TypeDefOrRefFlag.TypeDef:
				if (Constraint.TypeDef.TypeName == "ValueType")
					return;
				break;
			case TypeDefOrRefFlag.TypeRef:
				if (Constraint.TypeRef.TypeName == "ValueType")
					return;
				break;
			}
			Owner.Constraints.Add(this);
		}
		public override string ToString()
		{
			return Constraint.ToString();
		}
	}
}
