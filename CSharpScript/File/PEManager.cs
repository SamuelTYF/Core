using System;
using System.IO;
using Collection;
namespace CSharpScript.File
{
	public class PEManager
	{
		public AVL<int, PEFile> PEFiles;
		public AVL<int, Stack<AssemblyRef>> AssemblyRefWaitingList;
		public static readonly string[] Assemblies = new string[6] { "mscorlib.dll", "System.dll", "System.Configuration.dll", "System.Core.dll", "System.Data.dll", "System.Drawing.dll" };
		public PEFile Mscorlib;
		public InternalTypes InternalTypes;
		public PEManager()
		{
			PEFiles = new AVL<int, PEFile>();
			AssemblyRefWaitingList = new AVL<int, Stack<AssemblyRef>>();
			Mscorlib = Load("C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\mscorlib.dll");
			InternalTypes = new InternalTypes(Mscorlib.MetadataRoot);
		}
		public PEFile Load(string fname)
		{
			using FileStream stream = new FileInfo(fname).OpenRead();
			PEFile pEFile = new(this, stream);
			if (pEFile.MetadataRoot.TildeHeap.AssemblyTable.Count == 0)
				throw new Exception();
			int hash = pEFile.MetadataRoot.TildeHeap.AssemblyTable.Assemblys[0].Hash;
			PEFiles[hash] = pEFile;
			PEFiles[pEFile.MetadataRoot.TildeHeap.AssemblyTable.Assemblys[0].NullTokenName.GetHashCode()] = pEFile;
			if (AssemblyRefWaitingList.ContainsKey(hash))
			{
				Stack<AssemblyRef> stack = AssemblyRefWaitingList[hash];
				while (stack.Count != 0)
				{
					AssemblyRef assemblyRef = stack.Pop();
					assemblyRef.Link(this);
					assemblyRef.ThisAssemblyFile.TildeHeap.TryResolve(assemblyRef.ThisAssemblyFile);
				}
			}
			return pEFile;
		}
	}
}
