using System;
namespace CSharpScript.File
{
	public class FieldOrRef
	{
		public Field Field;
		public MemberRef MemberRef;
		public bool IsField;
		public string Name;
		public MemberRefParent Parent;
		public bool IsStatic;
		public FieldOrRef(Field field)
		{
			Field = field;
			IsField = true;
			Name = Field.Name;
			Parent = new MemberRefParent(Field.Parent);
			IsStatic = field.Flags.HasFlag(FieldAttributes.Static);
		}
		public FieldOrRef(MemberRef memberRef)
		{
			MemberRef = memberRef;
			IsField = false;
			Name = MemberRef.Name;
			Parent = MemberRef.Class;
			if (Parent.Flag == MemberRefParentFlag.TypeSpec)
			{
				if (Parent.TypeSpec.Parent.Flag == TypeDefOrRefFlag.TypeDef)
				{
					Field[] fieldList = Parent.TypeSpec.Parent.TypeDef.FieldList;
					foreach (Field field in fieldList)
						if (field.Name == memberRef.Name)
						{
							IsStatic = field.Flags.HasFlag(FieldAttributes.Static);
							return;
						}
					throw new Exception();
				}
				else if(Parent.TypeSpec.Parent.Flag==TypeDefOrRefFlag.TypeRef)
                {
					foreach (MemberRef field in Parent.TypeSpec.Fields)
						if (field.Name == memberRef.Name)
							return;
					throw new Exception();
				}
			}
			else if(Parent.Flag==MemberRefParentFlag.TypeRef)
            {
				foreach (MemberRef field in Parent.TypeRef.Fields)
					if (field.Name == memberRef.Name)
						return;
				throw new Exception();
            }
			throw new Exception();
		}
        public override string ToString() => IsField ? Field.ToString() : MemberRef.ToString();
    }
}
