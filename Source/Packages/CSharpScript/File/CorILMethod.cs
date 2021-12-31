using System.IO;
using Collection;
namespace CSharpScript.File
{
	public class CorILMethod
	{
		public MethodHeader MethodHeader;
		public MethodBody MethodBody;
		public ExceptionHeader[] ExceptionHeaders;
		public LocalVar[] LocalVars;
		public CorILMethod(Stream stream, MetadataRoot metadata)
		{
			MethodHeader = new MethodHeader(stream.ReadByte(), stream);
			if (MethodHeader.LocalVarSigTok != 0)
			{
				LocalVarSig localVarSig = metadata.TildeHeap.StandAloneSigTable.StandAloneSigs[(MethodHeader.LocalVarSigTok & 0xFFFFFF) - 1] as LocalVarSig;
				LocalVars = new LocalVar[localVarSig.Count];
				if (localVarSig.Count == 1)
					LocalVars[0] = new LocalVar(0, localVarSig.Pinned[0], localVarSig.Types[0], "num");
				for (int i = 0; i < localVarSig.Count; i++)
					LocalVars[i] = new LocalVar(i, localVarSig.Pinned[i], localVarSig.Types[i], $"num_{i + 1}");
			}
			else
				LocalVars = new LocalVar[0];
			byte[] array = new byte[MethodHeader.CodeSize];
			stream.Read(array, 0, array.Length);
			MethodBody = new MethodBody(array, metadata);
			List<ExceptionHeader> list = new();
			if (MethodHeader.MethodHeaderTypeValues.HasFlag(MethodHeaderTypeValues.CorILMethod_MoreSects))
			{
				array = new byte[(4294901760u - stream.Position) & 3];
				stream.Read(array, 0, array.Length);
				ExceptionHeader value = new(stream, metadata);
				list.Add(value);
				while (value.Kind.HasFlag(CorILMethodSectFlags.CorILMethod_Sect_MoreSects))
				{
					value = new ExceptionHeader(stream, metadata);
					list.Add(value);
				}
			}
			ExceptionHeaders = list.ToArray();
		}
		public override string ToString()
		{
			string text = $"MethodHeader:\n{MethodHeader}\n";
			if (LocalVars.Length != 0)
				text = text + "Locals:\n" + string.Join(",\n", (System.Collections.Generic.IEnumerable<LocalVar>)LocalVars) + "\n";
			text += $"MethodBody:\n{MethodBody}\n";
			if (ExceptionHeaders.Length != 0)
				text = text + "ExceptionHeaders:\n" + string.Join("\n", ExceptionHeaders);
			return text;
		}
	}
}
