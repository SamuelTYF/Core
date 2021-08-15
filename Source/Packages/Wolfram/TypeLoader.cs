using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace Wolfram.NETLink
{
	public class TypeLoader
	{
		private static Hashtable assemblyCache;

		private static DirectoryInfo gacDir;

		private static char[] pathChars;

		private static string dirToSearch;

		private static bool isBuildingDynAssembly;

		internal static bool isBuildingDynamicAssembly
        {
            get => isBuildingDynAssembly;
            set => isBuildingDynAssembly = value;
        }

        static TypeLoader()
		{
			assemblyCache = new Hashtable();
			pathChars = new char[2] { '/', '\\' };
			dirToSearch = null;
			isBuildingDynAssembly = false;
			AppDomain.CurrentDomain.AssemblyResolve += assemblyResolveEventHandler;
			AppDomain.CurrentDomain.TypeResolve += typeResolveEventHandler;
			string text = "";
			gacDir = new DirectoryInfo(Utils.IsMono ? (RuntimeEnvironment.GetRuntimeDirectory() + Path.DirectorySeparatorChar + "mono" + Path.DirectorySeparatorChar + "gac") : (new DirectoryInfo(Environment.SystemDirectory).Parent.FullName + Path.DirectorySeparatorChar + "assembly" + Path.DirectorySeparatorChar + "GAC"));
		}

		public static Type GetType(string typeName, bool throwOnError)
		{
			return GetType(typeName, "", throwOnError);
		}

		public static Type GetType(string typeName, string assemblyName, bool throwOnError)
		{
			Type type;
			if (assemblyName == "")
			{
				type = null;
				int num = typeName.LastIndexOf(']');
				if (num < 0)
					num = 0;
				int num2 = typeName.IndexOf(",", num);
				if (num2 != -1)
				{
					string name = typeName.Substring(0, num2);
					string text = typeName.Substring(num2 + 1).Replace(" ", "").ToLower();
					Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
					foreach (Assembly assembly in assemblies)
					{
						if (!(assembly.FullName.Replace(" ", "").ToLower() == text))
							continue;
						try
						{
							type = assembly.GetType(name, throwOnError: false);
							if (type != null)
								goto IL_00bb;
						}
						catch (Exception)
						{
						}
					}
				}
				goto IL_00bb;
			}
			Assembly assembly2 = LoadAssembly(assemblyName);
            return assembly2 == null
                ? throwOnError ? throw new TypeLoadException("Assembly " + assemblyName + " not found.") : (Type)null
                : assembly2.GetType(typeName, throwOnError);
        IL_00bb:
			if (type == null)type = Type.GetType(typeName, throwOnError: false);
            return type == null && throwOnError ? throw new TypeLoadException("Type " + typeName + " not found.") : type;
        }

        public static Type GetType(string typeName, Assembly assembly, bool throwOnError) => assembly.GetType(typeName, throwOnError);

        public static Assembly LoadAssembly(string assemblyName)
		{
			Assembly assembly = (Assembly)assemblyCache[assemblyName];
			if (assembly != null)
				return assembly;
			string text = assemblyName.ToLower();
			if (assemblyName.IndexOfAny(pathChars) != -1)
				assembly = Assembly.LoadFrom(assemblyName);
			else if (text.EndsWith(".dll") || text.EndsWith(".exe"))
			{
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				foreach (Assembly assembly2 in assemblies)
				{
					try
					{
						if (string.Compare(Path.GetFileName(assembly2.Location), assemblyName, ignoreCase: true) == 0)
						{
							assembly = assembly2;
							break;
						}
					}
					catch (Exception)
					{
					}
				}
				if (assembly == null)
				{
					FileInfo fileInfo = FindAssemblyFile(gacDir, assemblyName);
					if (fileInfo != null)
						assembly = Assembly.LoadFrom(fileInfo.FullName);
				}
			}
			else
			{
				assembly = Assembly.Load(assemblyName);
			}
			if (assembly != null)
			{
				assemblyCache.Add(assemblyName, assembly);
				string fullName = assembly.FullName;
				if (!assemblyCache.ContainsKey(fullName))
					assemblyCache.Add(fullName, assembly);
			}
			return assembly;
		}

		public static Assembly LoadAssembly(string assemblyName, string dir)
		{
			dirToSearch = new DirectoryInfo(dir).FullName;
			Assembly assembly = null;
			try
			{
				assembly = LoadAssembly(assemblyName);
			}
			catch (Exception)
			{
			}
			finally
			{
				dirToSearch = null;
			}
			if (assembly == null)
				assembly = LoadAssembly(assemblyName);
			return assembly;
		}

		private static Assembly assemblyResolveEventHandler(object sender, ResolveEventArgs args)
		{
			string name = args.Name;
			if (name.IndexOfAny(pathChars) != -1)
				return null;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				if (assembly.FullName == name)
					return assembly;
				if (!(assembly.GetName().Name == name))
					continue;
				if (dirToSearch == null)
					return assembly;
				try
				{
					if (string.Compare(new DirectoryInfo(assembly.Location).Parent.FullName, dirToSearch, ignoreCase: true) == 0)
						return assembly;
				}
				catch (Exception)
				{
				}
			}
			return null;
		}

		private static Assembly typeResolveEventHandler(object sender, ResolveEventArgs args)
		{
			string name = args.Name;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				if ((!isBuildingDynamicAssembly || !(assembly is AssemblyBuilder)) && assembly.GetType(name, throwOnError: false) != null)
					return assembly;
			}
			return null;
		}

		private static FileInfo FindAssemblyFile(DirectoryInfo dir, string fileToFind)
		{
			FileInfo[] files = dir.GetFiles(fileToFind);
			if (files.Length > 0)
				return files[0];
			DirectoryInfo[] directories = dir.GetDirectories();
			for (int num = directories.Length; num > 0; num--)
			{
				FileInfo fileInfo = FindAssemblyFile(directories[num - 1], fileToFind);
				if (fileInfo != null)
					return fileInfo;
			}
			return null;
		}
	}
}
