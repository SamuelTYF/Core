using System;
using Collection;
namespace CSharpScript.File
{
	public class TypeRef
	{
		public ResolutionScope ResolutionScope;
		public string TypeName;
		public string TypeNamespace;
		public TypeRefRow Row;
		public int Token;
		public string FullName;
		public int Hash;
		public List<MemberRef> Methods = new List<MemberRef>();
		public List<MemberRef> Fields = new List<MemberRef>();
		public TypeDef _TypeDef;
		public List<CustomAttribute> CustomAttributes = new List<CustomAttribute>();
		public TypeDef TypeDef
		{
			get
			{
				if (_TypeDef == null)
				{
					switch (ResolutionScope.Flag)
					{
					case ResolutionScopeFlag.AssemblyRef:
						if (ResolutionScope.AssemblyRef.Assembly != null)
						{
							_TypeDef = ResolutionScope.AssemblyRef.AssemblyFile.FindTypeDef(FullName);
							if (CustomAttributes.Length != 0)
								_TypeDef.CustomAttributes.AddList(CustomAttributes);
							CustomAttributes.Clear();
						}
						break;
					case ResolutionScopeFlag.TypeRef:
					{
						TypeDef typeDef = ResolutionScope.TypeRef.TypeDef;
						if (typeDef != null)
						{
							_TypeDef = typeDef.AssemblyFile.FindTypeDef(FullName);
							if (CustomAttributes.Length != 0)
								_TypeDef.CustomAttributes.AddList(CustomAttributes);
							CustomAttributes.Clear();
						}
						break;
					}
					default:
						throw new Exception();
					}
				}
				return _TypeDef;
			}
		}
		public TypeRef(TypeRefRow row)
		{
			TypeRefRow typeRefRow = (Row = row);
			Token = typeRefRow.Index | 0x1000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			ResolutionScope = new ResolutionScope(Row.ResolutionScope, metadata);
			TypeName = metadata.StringsHeap[Row.TypeName];
			TypeNamespace = metadata.StringsHeap[Row.TypeNamespace];
			if (Row.TypeNamespace == 0)
				FullName = $"{ResolutionScope}+{TypeName}";
			else
				FullName = TypeNamespace + "." + TypeName;
			Hash = FullName.GetHashCode();
			if (metadata.QueryTree.ContainsKey(Hash))
				throw new Exception();
			metadata.QueryTree[Hash] = Token;
		}
		public override string ToString()
		{
			return FullName;
		}
	}
}
