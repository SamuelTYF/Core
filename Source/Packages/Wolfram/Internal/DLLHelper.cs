using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using Microsoft.CSharp;

namespace Wolfram.NETLink.Internal
{
	internal class DLLHelper
	{
		private static ModuleBuilder dllModuleBuilder;

		private static string dllNamespace;

		private static string dllTypePrefix;

		private static string dllAssemblyName;

		private static string dllModuleName;

		private static int index;

		static DLLHelper()
		{
			dllNamespace = "Wolfram.NETLink.DynamicDLLNamespace";
			dllTypePrefix = "DLLWrapper";
			dllAssemblyName = "DynamicDLLAssembly";
			dllModuleName = "DynamicDLLModule";
			index = 1;
			AssemblyName name = new AssemblyName
			{
				Name = dllAssemblyName
			};
			dllModuleBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run).DefineDynamicModule(dllModuleName);
		}

		internal static string CreateDLLCall(string funcName, string dllName, string callConv, string retTypeName, string[] argTypeNames, bool[] areOutParams, string strFormat)
		{
			Type returnType = ((retTypeName == null) ? typeof(void) : TypeLoader.GetType(Utils.addSystemNamespace(retTypeName), throwOnError: true));
			Type[] array = new Type[(argTypeNames != null) ? argTypeNames.Length : 0];
			if (argTypeNames != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = TypeLoader.GetType(Utils.addSystemNamespace(argTypeNames[i]), throwOnError: true);
				}
			}
			CharSet nativeCharSet = ((strFormat == "ansi") ? CharSet.Ansi : ((strFormat == "unicode") ? CharSet.Unicode : CharSet.Auto));
			CallingConvention nativeCallConv;
			switch (callConv)
			{
			case "cdecl":
				nativeCallConv = CallingConvention.Cdecl;
				break;
			case "thiscall":
				nativeCallConv = CallingConvention.ThisCall;
				break;
			case "stdcall":
				nativeCallConv = CallingConvention.StdCall;
				break;
			default:
				nativeCallConv = CallingConvention.Winapi;
				break;
			}
			TypeBuilder typeBuilder = dllModuleBuilder.DefineType(dllNamespace + "." + dllTypePrefix + index++, TypeAttributes.Public);
			MethodBuilder methodBuilder = typeBuilder.DefinePInvokeMethod(funcName, dllName, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.PinvokeImpl, CallingConventions.Standard, returnType, array, nativeCallConv, nativeCharSet);
			methodBuilder.SetImplementationFlags(MethodImplAttributes.PreserveSig);
			for (int j = 0; j < areOutParams.Length; j++)
			{
				if (areOutParams[j])
					methodBuilder.DefineParameter(j + 1, ParameterAttributes.Out, null);
			}
			return typeBuilder.CreateType().FullName;
		}

		internal static string[] CreateDLLCall(string declaration, string[] referencedAssemblies, string language)
		{
			string text = dllTypePrefix + index++;
			string source = null;
			CodeSnippetCompileUnit compilationUnit = null;
			if (language == "csharp")
				compilationUnit = new CodeSnippetCompileUnit("using System;using System.Runtime.InteropServices;namespace " + dllNamespace + "{public class " + text + "{" + declaration + (declaration.EndsWith(";") ? "" : ";") + "}}");
			else
				source = "Imports System\nImports System.Runtime.InteropServices\nNamespace " + dllNamespace + "\nPublic Class " + text + "\n" + declaration + "\nEnd Class\nEnd Namespace";
			CompilerParameters compilerParameters = new CompilerParameters(referencedAssemblies);
			compilerParameters.GenerateInMemory = true;
			CompilerResults compilerResults = ((!(language == "csharp")) ?
				CodeDomProvider.CreateProvider("CSharp")
				.CompileAssemblyFromSource(compilerParameters, source) :
				CodeDomProvider.CreateProvider("CSharp")
				.CompileAssemblyFromDom(compilerParameters, compilationUnit));
			CompilerErrorCollection errors = compilerResults.Errors;
			if (errors.HasErrors)
			{
				string text2 = "";
				foreach (CompilerError item in errors)
				{
					if (!item.IsWarning)
						text2 = text2 + item.ErrorText + "\n";
				}
				return new string[1] { text2 };
			}
			Type type = compilerResults.CompiledAssembly.GetType(dllNamespace + "." + text, throwOnError: true);
			string fullName = type.FullName;
			string name = type.GetMethods(BindingFlags.Static | BindingFlags.Public)[0].Name;
			string text3 = type.GetMethods(BindingFlags.Static | BindingFlags.Public)[0].GetParameters().Length.ToString();
			return new string[3] { fullName, name, text3 };
		}
	}
}
