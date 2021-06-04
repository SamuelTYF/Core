using System;
namespace CSharpScript.File
{
	public class ImplMap
	{
		public PInvokeAttributes MappingFlags;
		public MemberForwarded MemberForwarded;
		public string ImportName;
		public ModuleRef ImportScope;
		public ImplMapRow Row;
		public int Token;
		public ImplMap(ImplMapRow row)
		{
			ImplMapRow implMapRow = (Row = row);
			Token = implMapRow.Index | 0x1C000000;
		}
		public void ResolveSignature(MetadataRoot metadata)
		{
			MappingFlags = (PInvokeAttributes)Row.MappingFlags;
			MemberForwarded = new MemberForwarded(Row.MemberForwarded, metadata);
			ImportName = metadata.StringsHeap[Row.ImportName];
			ImportScope = metadata.TildeHeap.ModuleRefTable.ModuleRefs[Row.ImportScope - 1];
			if (MemberForwarded.Flag == MemberForwardedFlag.MethodDef)
			{
				MemberForwarded.MethodDef.DLLImport = this;
				return;
			}
			throw new Exception();
		}
		public override string ToString()
		{
			string text = $"[System.Runtime.InteropServices.DllImportAttribute(\"{ImportScope}\"";
			switch (MappingFlags & PInvokeAttributes.CallConvMask)
			{
			case PInvokeAttributes.CallConvCdecl:
				text += ", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl";
				break;
			case PInvokeAttributes.CallConvStdcall:
				text += ", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall";
				break;
			case PInvokeAttributes.CallConvThiscall:
				text += ", CallingConvention = System.Runtime.InteropServices.CallingConvention.ThisCall";
				break;
			case PInvokeAttributes.CallConvFastcall:
				text += ", CallingConvention = System.Runtime.InteropServices.CallingConvention.FastCall";
				break;
			}
			switch (MappingFlags & PInvokeAttributes.BestFitMask)
			{
			case PInvokeAttributes.BestFitEnabled:
				text += ", BestFitMapping = True";
				break;
			case PInvokeAttributes.BestFitDisabled:
				text += ", BestFitMapping = False";
				break;
			}
			switch (MappingFlags & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetAnsi:
				text += ", CharSet = System.Runtime.InteropServices.CharSet.Ansi";
				break;
			case PInvokeAttributes.CharSetUnicode:
				text += ", CharSet = System.Runtime.InteropServices.CharSet.Unicode";
				break;
			case PInvokeAttributes.CharSetMask:
				text += ", CharSet = System.Runtime.InteropServices.CharSet.Auto";
				break;
			}
			if (MemberForwarded.Name != ImportName)
				text = text + ", EnterPoint = \"" + ImportName + "\"";
			if (MappingFlags.HasFlag(PInvokeAttributes.NoMangle))
				text += ", ExactSpelling = True";
			if (MappingFlags.HasFlag(PInvokeAttributes.SupportsLastError))
				text += ", SetLastError = True";
			switch (MappingFlags & PInvokeAttributes.ThrowOnUnmappableCharMask)
			{
			case PInvokeAttributes.ThrowOnUnmappableCharEnabled:
				text += ", ThrowOnUnmappableChar = True";
				break;
			case PInvokeAttributes.ThrowOnUnmappableCharDisabled:
				text += ", ThrowOnUnmappableChar = False";
				break;
			}
			return text + ")]";
		}
	}
}
