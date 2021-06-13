using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace Wolfram.NETLink.Internal
{
	public class DelegateHelper
	{
		private static ModuleBuilder delegateModuleBuilder;

		private static string delegateNamespace;

		private static string delegateAssemblyName;

		private static string delegateModuleName;

		private static int index;

		private static Hashtable linkTable;

		internal static MethodInfo callMathematicaMethod;

		private static int nextIndex;

		static DelegateHelper()
		{
			delegateNamespace = "Wolfram.NETLink.DynamicDelegateNamespace";
			delegateAssemblyName = "DynamicDelegateAssembly";
			delegateModuleName = "DynamicDelegateModule";
			index = 1;
			linkTable = new Hashtable();
			callMathematicaMethod = typeof(DelegateHelper).GetMethod("CallMathematica");
			nextIndex = 1;
			AssemblyName name = new AssemblyName
			{
				Name = delegateAssemblyName
			};
			delegateModuleBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run).DefineDynamicModule(delegateModuleName);
		}

		public static object CallMathematica(int linkHash, Type returnType, string func, int argsToSend, int callsUnshare, int wrapInNETBlock, params object[] args)
		{
			object result = null;
			int num = 0;
			bool[] array = new bool[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				if ((argsToSend & (1 << i)) != 0)
				{
					num++;
					array[i] = true;
				}
			}
			IKernelLink kernelLink = ((linkHash != 0) ? ((IKernelLink)linkTable[linkHash]) : StdLink.Link);
			if (kernelLink == null)
				return result;
			try
			{
				StdLink.RequestTransaction();
			}
			catch (Exception ex)
			{
				if (Utils.IsWindows)
					MessageBeep(0u);
				if (returnType == typeof(void))
					return null;
				throw ex;
			}
			lock (kernelLink)
			{
				kernelLink.PutFunction((callsUnshare == 0) ? "EvaluatePacket" : "EnterExpressionPacket", 1);
				kernelLink.PutFunction("NETLink`Package`delegateCallbackWrapper", 4);
				kernelLink.Put(func);
				kernelLink.PutFunction("List", num);
				for (int j = 0; j < args.Length; j++)
				{
					if (array[j])
						kernelLink.Put(args[j]);
				}
				kernelLink.Put(callsUnshare == 1);
				kernelLink.Put(wrapInNETBlock == 1);
				kernelLink.EndPacket();
				PacketType packetType = kernelLink.WaitForAnswer();
				try
				{
					if (callsUnshare == 0)
					{
						if (returnType != typeof(void))
						{
							kernelLink.CheckFunctionWithArgCount("List", 2);
							return Utils.readArgAs(kernelLink, kernelLink.GetInteger(), returnType);
						}
						return result;
					}
					return result;
				}
				catch (Exception)
				{
					throw new InvalidCastException("The return value from Mathematica was not of the expected type.");
				}
				finally
				{
					kernelLink.ClearError();
					kernelLink.NewPacket();
					if (packetType == PacketType.ReturnExpression)
					{
						kernelLink.NextPacket();
						kernelLink.NewPacket();
					}
				}
			}
		}

		internal static MethodInfo createDynamicMethod(IKernelLink ml, Type delegateType, string mFunc, int argsToSend, bool callsUnshare, bool wrapInNETBlock)
		{
			int num;
			if (ml == null)
				num = 0;
			else
			{
				num = ml.GetHashCode();
				if (!linkTable.ContainsKey(num))
					linkTable.Add(num, ml);
			}
			Type returnType = delegateType.GetMethod("Invoke").ReturnType;
			ParameterInfo[] parameters = delegateType.GetMethod("Invoke").GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			TypeBuilder typeBuilder = delegateModuleBuilder.DefineType("DummyType" + index++, TypeAttributes.Public);
			ILGenerator iLGenerator = typeBuilder.DefineMethod("delegateThunk", MethodAttributes.Public | MethodAttributes.Static, returnType, array).GetILGenerator();
			iLGenerator.DeclareLocal(typeof(object[]));
			iLGenerator.Emit(OpCodes.Ldc_I4, num);
			iLGenerator.Emit(OpCodes.Ldtoken, returnType);
			iLGenerator.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"));
			iLGenerator.Emit(OpCodes.Ldstr, mFunc);
			iLGenerator.Emit(OpCodes.Ldc_I4, argsToSend);
			iLGenerator.Emit(OpCodes.Ldc_I4, callsUnshare ? 1 : 0);
			iLGenerator.Emit(OpCodes.Ldc_I4, wrapInNETBlock ? 1 : 0);
			iLGenerator.Emit(OpCodes.Ldc_I4, array.Length);
			iLGenerator.Emit(OpCodes.Newarr, typeof(object));
			iLGenerator.Emit(OpCodes.Stloc_0);
			for (int j = 0; j < array.Length; j++)
			{
				iLGenerator.Emit(OpCodes.Ldloc_0);
				iLGenerator.Emit(OpCodes.Ldc_I4, j);
				iLGenerator.Emit(OpCodes.Ldarg, j);
				if (array[j].IsValueType)
					iLGenerator.Emit(OpCodes.Box, array[j]);
				else if (array[j].IsByRef)
				{
					Type elementType = array[j].GetElementType();
					iLGenerator.Emit(OpCodes.Ldobj, elementType);
					if (elementType.IsValueType)
						iLGenerator.Emit(OpCodes.Box, elementType);
				}
				iLGenerator.Emit(OpCodes.Stelem_Ref);
			}
			iLGenerator.Emit(OpCodes.Ldloc_0);
			iLGenerator.Emit(OpCodes.Call, callMathematicaMethod);
			if (returnType == typeof(void))
				iLGenerator.Emit(OpCodes.Pop);
			else if (Utils.IsTrulyPrimitive(returnType) || returnType.IsEnum)
			{
				iLGenerator.Emit(OpCodes.Unbox, returnType);
				iLGenerator.Emit(getLoadInstructionForType(returnType));
			}
			else if (returnType.IsValueType)
			{
				iLGenerator.Emit(OpCodes.Unbox, returnType);
				iLGenerator.Emit(OpCodes.Ldobj, returnType);
			}
			iLGenerator.Emit(OpCodes.Ret);
			return typeBuilder.CreateType().GetMethod("delegateThunk");
		}

		private static OpCode getLoadInstructionForType(Type t)
		{
			switch (Type.GetTypeCode(t))
			{
			case TypeCode.Boolean:
				return OpCodes.Ldind_I1;
			case TypeCode.Byte:
				return OpCodes.Ldind_U1;
			case TypeCode.SByte:
				return OpCodes.Ldind_I1;
			case TypeCode.Char:
				return OpCodes.Ldind_U2;
			case TypeCode.Int16:
				return OpCodes.Ldind_I2;
			case TypeCode.UInt16:
				return OpCodes.Ldind_U2;
			case TypeCode.Int32:
				return OpCodes.Ldind_I4;
			case TypeCode.UInt32:
				return OpCodes.Ldind_U4;
			case TypeCode.Int64:
				return OpCodes.Ldind_I8;
			case TypeCode.UInt64:
				return OpCodes.Ldind_I8;
			case TypeCode.Single:
				return OpCodes.Ldind_R4;
			case TypeCode.Double:
				return OpCodes.Ldind_R8;
			default:
				throw new ArgumentException();
			}
		}

		internal static string defineDelegate(string name, string retTypeName, string[] paramTypeNames)
		{
			Type returnType = ((retTypeName == null) ? typeof(void) : TypeLoader.GetType(Utils.addSystemNamespace(retTypeName), throwOnError: true));
			Type[] array = new Type[(paramTypeNames != null) ? paramTypeNames.Length : 0];
			if (paramTypeNames != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = TypeLoader.GetType(Utils.addSystemNamespace(paramTypeNames[i]), throwOnError: true);
				}
			}
			Type[] types = delegateModuleBuilder.GetTypes();
			ArrayList arrayList = new ArrayList(types.Length);
			Type[] array2 = types;
			for (int j = 0; j < array2.Length; j++)
			{
				arrayList.Add(array2[j].Name);
			}
			string text = name;
			while (arrayList.Contains(text))
			{
				text = name + "$" + nextIndex++;
			}
			TypeBuilder typeBuilder = delegateModuleBuilder.DefineType(delegateNamespace + "." + text, TypeAttributes.Public | TypeAttributes.Sealed, typeof(MulticastDelegate));
			typeBuilder.DefineConstructor(MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, new Type[2]
			{
				typeof(object),
				typeof(int)
			}).SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			typeBuilder.DefineMethod("Invoke", MethodAttributes.Public | MethodAttributes.Virtual, returnType, array).SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			return typeBuilder.CreateType().FullName;
		}

		[DllImport("user32.dll")]
		private static extern int MessageBeep(uint n);
	}
}
