using System;
using System.Collections.Generic;
namespace CSharpScript.File
{
	public class MethodDefOrRef
	{
		public MethodDefOrRefFlag Flag;
		public MethodDef MethodDef;
		public MemberRef MemberRef;
		public MethodSpec MethodSpec;
		public TypeDefOrRef _Parent;
		public TypeDefOrRef Parent
		{
			get
			{
				if (_Parent == null)
					return Flag switch
					{
						MethodDefOrRefFlag.MethodDef => _Parent = new TypeDefOrRef(MethodDef.Parent), 
						MethodDefOrRefFlag.MemberRef => _Parent = new TypeDefOrRef(MemberRef.Class.TypeDef, MemberRef.Class.TypeRef, MemberRef.Class.TypeSpec), 
						MethodDefOrRefFlag.MethodSpec => _Parent = MethodSpec.Method.Parent, 
						_ => throw new Exception(), 
					};
				return _Parent;
			}
		}
		public MethodDefOrRef(int value, MetadataRoot metadata)
		{
			Flag = (MethodDefOrRefFlag)(value & 1);
			switch (Flag)
			{
			case MethodDefOrRefFlag.MethodDef:
				MethodDef = metadata.TildeHeap.MethodDefTable.MethodDefs[(value >> 1) - 1];
				break;
			case MethodDefOrRefFlag.MemberRef:
				MemberRef = metadata.TildeHeap.MemberRefTable.MemberRefs[(value >> 1) - 1];
				break;
			default:
				throw new Exception();
			}
		}
		public MethodDefOrRef(MethodDef methoddef, MemberRef memberref, MethodSpec methodspec)
		{
			if ((MethodDef = methoddef) != null)
			{
				Flag = MethodDefOrRefFlag.MethodDef;
				return;
			}
			if ((MemberRef = memberref) != null)
			{
				Flag = MethodDefOrRefFlag.MemberRef;
				return;
			}
			if ((MethodSpec = methodspec) != null)
			{
				Flag = MethodDefOrRefFlag.MethodSpec;
				return;
			}
			throw new Exception();
		}
        public override string ToString() => Flag switch
        {
            MethodDefOrRefFlag.MethodDef => MethodDef.ToString(),
            MethodDefOrRefFlag.MemberRef => MemberRef.ToString(),
            MethodDefOrRefFlag.MethodSpec => MethodSpec.ToString(),
            _ => throw new Exception(),
        };
        public string Name() => Flag switch
        {
            MethodDefOrRefFlag.MethodDef => MethodDef.Name,
            MethodDefOrRefFlag.MemberRef => MemberRef.Name,
            MethodDefOrRefFlag.MethodSpec => MethodSpec.Method.Name() + "<" + string.Join(",", (IEnumerable<TypeSig>)MethodSpec.Instantiation.GenericTypes) + ">",
            _ => throw new Exception(),
        };
    }
}
