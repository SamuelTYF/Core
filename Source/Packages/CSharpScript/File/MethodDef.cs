using System;
using System.Collections.Generic;
using System.IO;
using Collection;
namespace CSharpScript.File
{
	public class MethodDef
	{
		public int RVA;
		public MethodImplAttributes ImplFlags;
		public MethodAttributes Flags;
		public string Name;
		public MethodDefSig Signature;
		public Param ReturnParam;
		public Param[] ParamList;
		public Collection.List<GenericParam> GenericParams = new Collection.List<GenericParam>();
		public int Index;
		public TypeDef Parent;
		public MethodDefRow Row;
		public int Token;
		public Collection.List<CustomAttribute> CustomAttributes = new Collection.List<CustomAttribute>();
		public DeclSecurity DeclSecurity;
		public MethodImpl Base;
		public ImplMap DLLImport;
		public bool HasMethodBody;
		public CorILMethod Method;
		public bool HasThis;
		public MethodDef(MethodDefRow row)
		{
			MethodDefRow methodDefRow = (Row = row);
			Token = methodDefRow.Index | 0x6000000;
		}
		public void ResolveSignature(MetadataRoot metadata, int nextparam)
		{
			RVA = Row.RVA;
			ImplFlags = (MethodImplAttributes)Row.ImplFlags;
			Flags = (MethodAttributes)Row.Flags;
			Name = metadata.StringsHeap[Row.Name];
			int index = 0;
			Signature = new MethodDefSig(metadata.BlobHeap.Values[Row.Signature], ref index, metadata);
			metadata.BlobHeap.ParsedValues[Row.Signature] = this;
			HasThis = Signature.Flags.HasFlag(MethodDefSigAbbreviations.HASTHIS);
			int num = nextparam - Row.ParamList;
			if (num != 0)
			{
				if (metadata.TildeHeap.ParamTable.Params[Row.ParamList - 1].Sequence == 0)
				{
					ParamList = new Param[num - 1];
					ReturnParam = metadata.TildeHeap.ParamTable.Params[Row.ParamList - 1];
					Array.Copy(metadata.TildeHeap.ParamTable.Params, Row.ParamList, ParamList, 0, num - 1);
					ReturnParam.Parent = this;
					ReturnParam.Type = Signature.ReturnType;
					for (int i = 0; i < num - 1; i++)
					{
						ParamList[i].Parent = this;
						ParamList[i].Type = Signature.ParamTypes[i];
					}
				}
				else
				{
					ParamList = new Param[num];
					Array.Copy(metadata.TildeHeap.ParamTable.Params, Row.ParamList - 1, ParamList, 0, num);
					for (int j = 0; j < num; j++)
					{
						ParamList[j].Parent = this;
						ParamList[j].Type = Signature.ParamTypes[j];
					}
				}
			}
			else
				ParamList = new Param[0];
			HasMethodBody = RVA != 0 && !Flags.HasFlag(MethodAttributes.PInvokeImpl);
			if (RVA != 0 && Flags.HasFlag(MethodAttributes.PInvokeImpl))
				Console.WriteLine($"{this} at 0x{RVA:X}");
		}
		public override string ToString()
		{
			return ((ReturnParam == null) ? "Void" : ReturnParam.Type.ToString()) + string.Format(" {0}.{1}({2})", Parent, Name, string.Join(", ", Array.ConvertAll(ParamList, (Param p) => p.Type.ToString())));
		}
		public string GetInformation()
		{
			string text = "";
			if (DLLImport != null)
				text += $"{DLLImport}\n";
			if (CustomAttributes.Length != 0)
				text = text + string.Join("\n", CustomAttributes) + "\n";
			if (DeclSecurity != null)
				text += $"{DeclSecurity.PermissionSet}\n";
			return string.Format("{0}{1} {2}.{3}({4})", text, (ReturnParam == null) ? "Void" : ReturnParam.ToString(), Parent, Name, string.Join(", ", (IEnumerable<Param>)ParamList));
		}
		public string GetFullInformation()
		{
			return (!HasMethodBody) ? GetInformation() : (GetInformation() + "\n" + Method.ToString());
		}
		public void Read(Stream stream, MetadataRoot metadata)
		{
			if (HasMethodBody)
			{
				stream.Position = metadata.PEFile.GetOffset(RVA);
				Method = new CorILMethod(stream, metadata);
			}
		}
	}
}
