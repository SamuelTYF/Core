using System;
using CSharpScript.DomTree;
namespace CSharpScript.File
{
	public abstract class CSharpCode
	{
		public static CSharpCode Parse(ILCode[] source)
		{
			int index = 0;
			int target;
			return Parse(source, ref index, source[source.Length - 1].Info.Offset + 1, out target, new CodeList());
		}
		public static CSharpCode Parse(ILCode[] source, ref int index, int end, out int target, CodeList Codes, bool dup = false)
		{
			target = -2;
			while (index < source.Length)
			{
				ILCode iLCode = source[index];
				if (iLCode.Info.Offset >= end)
					return Codes.ToCode();
				index++;
				int target2;
				switch (iLCode.Info.Flag)
				{
				case ILCodeFlag.LdArg:
					Codes.Add(new CSharpCodeParam((iLCode as ILInstructionCode_LdArg).Param));
					break;
				case ILCodeFlag.LdArgA:
					Codes.Add(new CSharpCodeParam((iLCode as ILInstructionCode_LdArgA).Param, isref: true));
					break;
				case ILCodeFlag.LdThis:
					Codes.Add(new CSharpCodeGetThis());
					break;
				case ILCodeFlag.StLoc:
					Codes.Add(new CSharpCodeSet(new CSharpCodeLocalVar((iLCode as ILInstructionCode_StLoc).Var), Codes.Pop()), isvoid: true);
					break;
				case ILCodeFlag.LdLoc:
					Codes.Add(new CSharpCodeLocalVar((iLCode as ILInstructionCode_LdLoc).Var));
					break;
				case ILCodeFlag.LdLen:
					Codes.Add(new CSharpCodeGetLength(Codes.Pop()));
					break;
				case ILCodeFlag.Conv:
					Codes.Add(new CSharpCodeConvert(Codes.Pop(), (iLCode as ILInstructionCode_Conv).Type));
					break;
				case ILCodeFlag.Call:
					if ((iLCode as ILInstructionCode_Call).Method.ToString() == "System.Object..ctor")
					{
						if (!(Codes.Pop() is CSharpCodeGetThis))
							throw new Exception();
						Codes.Add(new CSharpCodeNormalConstructor());
					}
					else
					{
						CSharpCode cSharpCode = new CSharpCodeInvoke((iLCode as ILInstructionCode_Call).Method, Codes);
						Codes.Add(cSharpCode, (cSharpCode as CSharpCodeInvoke).mds.ReturnType == TypeSig.VOID);
					}
					break;
				case ILCodeFlag.CallVirt:
				{
					CSharpCode cSharpCode = new CSharpCodeInvoke((iLCode as ILInstructionCode_CallVirt).Method, Codes);
					Codes.Add(cSharpCode, (cSharpCode as CSharpCodeInvoke).mds.ReturnType == TypeSig.VOID);
					break;
				}
				case ILCodeFlag.Ret:
					target = -1;
					return Codes.Return();
				case ILCodeFlag.LdC:
					Codes.Add(new CSharpCodeConstant(iLCode as ILInstructionCode_LdC));
					break;
				case ILCodeFlag.Operator:
				{
					ILInstructionCode_Operator iLInstructionCode_Operator = iLCode as ILInstructionCode_Operator;
					switch (iLInstructionCode_Operator.Operator)
					{
					case BaseOperator.add:
						Codes.Add(new CSharpCodeDoubleOperator(Codes.Pop(), Codes.Pop(), DoubleOperator.add, iLInstructionCode_Operator.Checked));
						break;
					case BaseOperator.sub:
						Codes.Add(new CSharpCodeDoubleOperator(Codes.Pop(), Codes.Pop(), DoubleOperator.sub, iLInstructionCode_Operator.Checked));
						break;
					case BaseOperator.mul:
						Codes.Add(new CSharpCodeDoubleOperator(Codes.Pop(), Codes.Pop(), DoubleOperator.mul, iLInstructionCode_Operator.Checked));
						break;
					case BaseOperator.div:
						Codes.Add(new CSharpCodeDoubleOperator(Codes.Pop(), Codes.Pop(), DoubleOperator.div, iLInstructionCode_Operator.Checked));
						break;
					case BaseOperator.rem:
						Codes.Add(new CSharpCodeDoubleOperator(Codes.Pop(), Codes.Pop(), DoubleOperator.rem, iLInstructionCode_Operator.Checked));
						break;
					case BaseOperator.and:
						Codes.Add(new CSharpCodeDoubleOperator(Codes.Pop(), Codes.Pop(), DoubleOperator.and, iLInstructionCode_Operator.Checked));
						break;
					case BaseOperator.or:
						Codes.Add(new CSharpCodeDoubleOperator(Codes.Pop(), Codes.Pop(), DoubleOperator.or, iLInstructionCode_Operator.Checked));
						break;
					case BaseOperator.xor:
						Codes.Add(new CSharpCodeDoubleOperator(Codes.Pop(), Codes.Pop(), DoubleOperator.xor, iLInstructionCode_Operator.Checked));
						break;
					case BaseOperator.shl:
						Codes.Add(new CSharpCodeDoubleOperator(Codes.Pop(), Codes.Pop(), DoubleOperator.shl, iLInstructionCode_Operator.Checked));
						break;
					case BaseOperator.shr:
						Codes.Add(new CSharpCodeDoubleOperator(Codes.Pop(), Codes.Pop(), DoubleOperator.shr, iLInstructionCode_Operator.Checked));
						break;
					case BaseOperator.neg:
						Codes.Add(new CSharpCodeSingleOperator(Codes.Pop(), SingleOperator.neg));
						break;
					case BaseOperator.not:
						Codes.Add(new CSharpCodeSingleOperator(Codes.Pop(), SingleOperator.not));
						break;
					default:
						throw new Exception();
					}
					break;
				}
				case ILCodeFlag.StFld:
					if (dup)
						return new CSharpCodeSet(new CSharpCodeDupField((iLCode as ILInstructionCode_StFld).Field), Codes.Pop());
					Codes.Add(new CSharpCodeSet(value: Codes.Pop(), des: new CSharpCodeField((iLCode as ILInstructionCode_StFld).Field.IsStatic ? null : Codes.Pop(), (iLCode as ILInstructionCode_StFld).Field, _Isref: false)));
					break;
				case ILCodeFlag.LdFld:
					Codes.Add(new CSharpCodeField(Codes.Pop(), (iLCode as ILInstructionCode_LdFld).Field, _Isref: false));
					break;
				case ILCodeFlag.LdFldA:
					Codes.Add(new CSharpCodeField(Codes.Pop(), (iLCode as ILInstructionCode_LdFldA).Field, _Isref: true));
					break;
				case ILCodeFlag.NewObj:
				{
					ILInstructionCode_NewObj iLInstructionCode_NewObj = iLCode as ILInstructionCode_NewObj;
					Codes.Add(new CSharpCodeNewObject(iLInstructionCode_NewObj.Method.Parent, iLInstructionCode_NewObj.Method, Codes));
					break;
				}
				case ILCodeFlag.IsInst:
					Codes.Add(new CSharpCodeAs(Codes.Pop(), (iLCode as ILInstructionCode_IsInst).Type));
					break;
				case ILCodeFlag.Br:
				{
					ILInstructionCode_Br iLInstructionCode_Br = iLCode as ILInstructionCode_Br;
					if (iLInstructionCode_Br.TargetIndex < iLCode.Info.Offset)
						throw new Exception();
					if (iLInstructionCode_Br.Condition == Condition.brfalse)
					{
						CSharpCode condition = new CSharpCodeSingleOperator(Codes.Pop(), SingleOperator.@true);
						CodeList codeList = new CodeList(Codes);
						CSharpCode cSharpCode = Parse(source, ref index, iLInstructionCode_Br.TargetIndex, out var target3, codeList);
						int lastStack = codeList.LastStack;
						switch (target3)
						{
						case -2:
							cSharpCode = new CSharpCodeIf(condition, cSharpCode, null);
							Codes.Update(lastStack);
							Codes.Add(cSharpCode);
							break;
						case -1:
							cSharpCode = new CSharpCodeIf(condition, cSharpCode, Parse(source, ref index, end, out target2, codeList = new CodeList(Codes)));
							if (codeList.LastStack != lastStack)
								throw new Exception();
							Codes.Update(lastStack);
							Codes.Add(cSharpCode);
							break;
						default:
							cSharpCode = new CSharpCodeIf(condition, cSharpCode, Parse(source, ref index, target3, out target2, codeList = new CodeList(Codes)));
							if (codeList.LastStack != lastStack)
								throw new Exception();
							Codes.Update(lastStack);
							Codes.Add(cSharpCode);
							break;
						}
						break;
					}
					if (iLInstructionCode_Br.Condition == Condition.brtrue)
					{
						CSharpCode condition2 = new CSharpCodeSingleOperator(Codes.Pop(), SingleOperator.@true);
						CodeList codeList2 = new CodeList(Codes);
						CSharpCode cSharpCode = Parse(source, ref index, iLInstructionCode_Br.TargetIndex, out var target4, codeList2);
						int lastStack2 = codeList2.LastStack;
						switch (target4)
						{
						case -2:
							cSharpCode = new CSharpCodeIf(condition2, null, cSharpCode);
							Codes.Update(lastStack2);
							Codes.Add(cSharpCode);
							break;
						case -1:
							cSharpCode = new CSharpCodeIf(condition2, Parse(source, ref index, end, out target2, codeList2 = new CodeList(Codes)), cSharpCode);
							if (codeList2.LastStack != lastStack2)
								throw new Exception();
							Codes.Update(lastStack2);
							Codes.Add(cSharpCode);
							break;
						default:
							cSharpCode = new CSharpCodeIf(condition2, Parse(source, ref index, target4, out target2, codeList2 = new CodeList(Codes)), cSharpCode);
							if (codeList2.LastStack != lastStack2)
								throw new Exception();
							Codes.Update(lastStack2);
							Codes.Add(cSharpCode);
							break;
						}
						break;
					}
					if (iLInstructionCode_Br.Condition == Condition.br)
					{
						target = iLInstructionCode_Br.TargetIndex;
						return Codes.ToCode();
					}
					throw new Exception();
				}
				case ILCodeFlag.NewArray:
					Codes.Add(new CSharpCodeNewArray((iLCode as ILInstructionCode_NewArray).Type, Codes.Pop()));
					break;
				case ILCodeFlag.Dup:
				{
					CSharpCode cSharpCode = Codes.Top();
					if (cSharpCode is CSharpCodeNewObject)
					{
						(cSharpCode as CSharpCodeNewObject).Dups.Add(Parse(source, ref index, end, out target2, new CodeList(), dup: true));
						break;
					}
					if (cSharpCode is CSharpCodeNewArray)
					{
						(cSharpCode as CSharpCodeNewArray).Register(Parse(source, ref index, end, out target2, new CodeList(), dup: true));
						break;
					}
					throw new Exception();
				}
				case ILCodeFlag.StElemType:
					if (dup)
						return new CSharpCodeStElem(Codes.Pop(Codes.Codes.Length - 1), Codes.Pop(), null);
					Codes.Add(new CSharpCodeStElem(Codes.Pop(), Codes.Pop(), Codes.Pop()));
					break;
				case ILCodeFlag.LdFieldToken:
					if (!dup)
						throw new Exception();
					return new CSharpCodeLdFieldToken((iLCode as ILInstructionCode_LdFieldToken).Field.FieldToken);
				case ILCodeFlag.InitObj:
					Codes.Pop();
					break;
				case ILCodeFlag.LdObj:
				{
					CSharpCode cSharpCode = Codes.Pop();
					if (cSharpCode is CSharpCodeLocalVar)
					{
						Codes.Add(new CSharpCodeLocalVar((cSharpCode as CSharpCodeLocalVar).Var));
						break;
					}
					throw new Exception();
				}
				case ILCodeFlag.Pop:
					Codes.Pop();
					break;
				default:
					throw new Exception();
				case ILCodeFlag.Nop:
				case ILCodeFlag.Constrained:
				case ILCodeFlag.Box:
					break;
				}
			}
			return Codes.ToCode();
		}
		public string GetTabs(int tabs)
		{
			return "".PadLeft(tabs, '\t');
		}
		public abstract string Print(int tabs = 0);
		public override string ToString()
		{
			return Print();
		}
	}
}
