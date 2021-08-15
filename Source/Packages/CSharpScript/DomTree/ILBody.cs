using System;
using Collection;
using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public sealed class ILBody : ILBlock
	{
		public ILMethod Method;
		public AVL<int, ILCode> Query;
		public ILBody(int offset, int length)
			: base(offset, length, ILCodeFlag.ILBody)
		{
		}
		public static ILCode[] Select(AVL<int, ILCode> query, ILCodeInfo info)
		{
			List<ILCode> list = new List<ILCode>();
			int i = info.Offset;
			ILCode iLCode;
			for (int num = i + info.Length; i < num; i += iLCode.Info.Length)
			{
				iLCode = query[i];
				list.Add(iLCode);
			}
			return list.ToArray();
		}
		public static ILBody ParseBody(MethodDef def, int start, int length)
		{
			CorILMethod method = def.Method;
			AVL<int, ILCode> aVL = new();
			Instruction[] instructions = method.MethodBody.Instructions;
			foreach (Instruction instruction in instructions)
			{
				ILCode value = ILInstructionCode.ParseInstruction(def, instruction);
				aVL[instruction.ILIndex] = value;
			}
			if (method.ExceptionHeaders.Length > 1)
				throw new Exception();
			if (method.ExceptionHeaders.Length == 1)
			{
				ILTry iLTry = null;
				ExceptionClause[] clauses = method.ExceptionHeaders[0].Clauses;
				for (int j = 0; j < clauses.Length; j++)
				{
					ExceptionClause exceptionClause = clauses[j];
					if (iLTry == null || iLTry.Info.Offset != exceptionClause.TryOffset || iLTry.Info.Length != exceptionClause.TryLength)
					{
						iLTry = new ILTry(exceptionClause.TryOffset, exceptionClause.TryLength);
						iLTry.Load(Select(aVL, iLTry.Info));
						aVL[exceptionClause.TryOffset] = iLTry;
					}
					switch (exceptionClause.Flags)
					{
					case ExceptionHandlingFlags.COR_ILEXCEPTION_CLAUSE_EXCEPTION:
					{
						ILCatch iLCatch = new(exceptionClause.HandlerOffset, exceptionClause.HandlerLength);
						iLCatch.Type = exceptionClause.Type;
						iLCatch.Parent = iLTry;
						iLCatch.Load(Select(aVL, iLCatch.Info));
						aVL[exceptionClause.HandlerOffset] = iLCatch;
						iLTry.Catches.Add(iLCatch);
						break;
					}
					case ExceptionHandlingFlags.COR_ILEXCEPTION_CLAUSE_FINALLY:
					{
						ILFinally iLFinally = new(exceptionClause.HandlerOffset, exceptionClause.HandlerLength);
						iLFinally.Parent = iLTry;
						iLFinally.Load(Select(aVL, iLFinally.Info));
						aVL[exceptionClause.HandlerOffset] = iLFinally;
						if (iLTry.Finally != null)
							throw new Exception();
						iLTry = null;
						break;
					}
					case ExceptionHandlingFlags.COR_ILEXCEPTION_CLAUSE_FAULT:
					{
						ILFault iLFault = new(exceptionClause.HandlerOffset, exceptionClause.HandlerLength);
						iLFault.Parent = iLTry;
						iLFault.Load(Select(aVL, iLFault.Info));
						aVL[exceptionClause.HandlerOffset] = iLFault;
						if (iLTry.Finally != null)
							throw new Exception();
						iLTry = null;
						break;
					}
					default:
						throw new Exception();
					}
				}
			}
			ILBody iLBody = new(start, length);
			iLBody.Query = aVL;
			iLBody.Load(Select(aVL, iLBody.Info));
			aVL[start] = iLBody;
			return iLBody;
		}
        public override string Print(int tabs = 0) 
			=> string.Join("\n", Array.ConvertAll(Codes, (ILCode code) => code.Print(tabs + 1)));
    }
}
