using System;
using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public abstract class ILInstructionCode : ILCode
	{
		public ILInstructionCode(int offset, int length, ILCodeFlag flag)
			: base(offset, length, flag)
		{
		}
		public static ILCode ParseInstruction(MethodDef method, Instruction instruction)
		{
			switch (instruction.Operator.Value)
			{
			case 0:
				return new ILInstructionCode_Nop(instruction.ILIndex, instruction.Size);
			case 1:
				throw new Exception();
			case 2:
				if (method.HasThis)
					return new ILInstructionCode_LdThis(method.Parent, instruction.ILIndex, instruction.Size);
				return new ILInstructionCode_LdArg(0, method.ParamList, instruction.ILIndex, instruction.Size);
			case 3:
				return new ILInstructionCode_LdArg((!method.HasThis) ? 1 : 0, method.ParamList, instruction.ILIndex, instruction.Size);
			case 4:
				return new ILInstructionCode_LdArg(method.HasThis ? 1 : 2, method.ParamList, instruction.ILIndex, instruction.Size);
			case 5:
				return new ILInstructionCode_LdArg(method.HasThis ? 2 : 3, method.ParamList, instruction.ILIndex, instruction.Size);
			case 6:
				return new ILInstructionCode_LdLoc(0, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 7:
				return new ILInstructionCode_LdLoc(1, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 8:
				return new ILInstructionCode_LdLoc(2, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 9:
				return new ILInstructionCode_LdLoc(3, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 10:
				return new ILInstructionCode_StLoc(0, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 11:
				return new ILInstructionCode_StLoc(1, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 12:
				return new ILInstructionCode_StLoc(2, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 13:
				return new ILInstructionCode_StLoc(3, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 14:
				return new ILInstructionCode_LdArg(method.HasThis ? ((instruction as Instruction_ShortInlineVar).Value - 1) : (instruction as Instruction_ShortInlineVar).Value, method.ParamList, instruction.ILIndex, instruction.Size);
			case 15:
				return new ILInstructionCode_LdArgA(method.HasThis ? ((instruction as Instruction_ShortInlineVar).Value - 1) : (instruction as Instruction_ShortInlineVar).Value, method.ParamList, instruction.ILIndex, instruction.Size);
			case 16:
				return new ILInstructionCode_StArgA(method.HasThis ? ((instruction as Instruction_ShortInlineVar).Value - 1) : (instruction as Instruction_ShortInlineVar).Value, method.ParamList, instruction.ILIndex, instruction.Size);
			case 17:
				return new ILInstructionCode_LdLoc((instruction as Instruction_ShortInlineVar).Value, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 18:
				return new ILInstructionCode_LdLoc((instruction as Instruction_ShortInlineVar).Value, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 19:
				return new ILInstructionCode_StLoc((instruction as Instruction_ShortInlineVar).Value, method.Method.LocalVars, instruction.ILIndex, instruction.Size);
			case 20:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_OBJECT, null, instruction.ILIndex, instruction.Size);
			case 21:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, -1, instruction.ILIndex, instruction.Size);
			case 22:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, 0, instruction.ILIndex, instruction.Size);
			case 23:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, 1, instruction.ILIndex, instruction.Size);
			case 24:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, 2, instruction.ILIndex, instruction.Size);
			case 25:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, 3, instruction.ILIndex, instruction.Size);
			case 26:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, 4, instruction.ILIndex, instruction.Size);
			case 27:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, 5, instruction.ILIndex, instruction.Size);
			case 28:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, 6, instruction.ILIndex, instruction.Size);
			case 29:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, 7, instruction.ILIndex, instruction.Size);
			case 30:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, 8, instruction.ILIndex, instruction.Size);
			case 31:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I1, (instruction as Instruction_ShortInlineI).Value, instruction.ILIndex, instruction.Size);
			case 32:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I4, (instruction as Instruction_InlineI).Value, instruction.ILIndex, instruction.Size);
			case 33:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_I8, (instruction as Instruction_InlineI8).Value, instruction.ILIndex, instruction.Size);
			case 34:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_R4, (instruction as Instruction_ShortInlineR).Value, instruction.ILIndex, instruction.Size);
			case 35:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_R8, (instruction as Instruction_InlineR).Value, instruction.ILIndex, instruction.Size);
			case 37:
				return new ILInstructionCode_Dup(instruction.ILIndex, instruction.Size);
			case 38:
				return new ILInstructionCode_Pop(instruction.ILIndex, instruction.Size);
			case 39:
				throw new Exception();
			case 40:
				return new ILInstructionCode_Call(instruction as Instruction_InlineMethod, instruction.ILIndex, instruction.Size);
			case 41:
				throw new Exception();
			case 42:
				return new ILInstructionCode_Ret(instruction.ILIndex, instruction.Size);
			case 43:
				return new ILInstructionCode_Br(Condition.br, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 44:
				return new ILInstructionCode_Br(Condition.brfalse, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 45:
				return new ILInstructionCode_Br(Condition.brtrue, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 46:
				return new ILInstructionCode_Br(Condition.beq, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 47:
				return new ILInstructionCode_Br(Condition.bge, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 48:
				return new ILInstructionCode_Br(Condition.bgt, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 49:
				return new ILInstructionCode_Br(Condition.ble, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 50:
				return new ILInstructionCode_Br(Condition.blt, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 51:
				return new ILInstructionCode_Br(Condition.bne, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 52:
				return new ILInstructionCode_Br(Condition.bge, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 53:
				return new ILInstructionCode_Br(Condition.bgt, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 54:
				return new ILInstructionCode_Br(Condition.ble, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 55:
				return new ILInstructionCode_Br(Condition.blt, (instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 56:
				return new ILInstructionCode_Br(Condition.br, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 57:
				return new ILInstructionCode_Br(Condition.brfalse, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 58:
				return new ILInstructionCode_Br(Condition.brtrue, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 59:
				return new ILInstructionCode_Br(Condition.beq, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 60:
				return new ILInstructionCode_Br(Condition.bge, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 61:
				return new ILInstructionCode_Br(Condition.bgt, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 62:
				return new ILInstructionCode_Br(Condition.ble, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 63:
				return new ILInstructionCode_Br(Condition.blt, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 64:
				return new ILInstructionCode_Br(Condition.bne, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 65:
				return new ILInstructionCode_Br(Condition.bge, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 66:
				return new ILInstructionCode_Br(Condition.bgt, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 67:
				return new ILInstructionCode_Br(Condition.ble, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 68:
				return new ILInstructionCode_Br(Condition.blt, (instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 69:
				return new ILInstructionCode_Switch((instruction as Instruction_InlineSwitch).Labels, instruction.ILIndex, instruction.Size);
			case 70:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_I1, instruction.ILIndex, instruction.Size);
			case 71:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_U1, instruction.ILIndex, instruction.Size);
			case 72:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_I2, instruction.ILIndex, instruction.Size);
			case 73:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_U2, instruction.ILIndex, instruction.Size);
			case 74:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_I4, instruction.ILIndex, instruction.Size);
			case 75:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_U4, instruction.ILIndex, instruction.Size);
			case 76:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_I8, instruction.ILIndex, instruction.Size);
			case 77:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_I, instruction.ILIndex, instruction.Size);
			case 78:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_R4, instruction.ILIndex, instruction.Size);
			case 79:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_R8, instruction.ILIndex, instruction.Size);
			case 80:
				return new ILInstructionCode_Ldind(ElementType.ELEMENT_TYPE_BYREF, instruction.ILIndex, instruction.Size);
			case 81:
				return new ILInstructionCode_Stind(ElementType.ELEMENT_TYPE_BYREF, instruction.ILIndex, instruction.Size);
			case 82:
				return new ILInstructionCode_Stind(ElementType.ELEMENT_TYPE_I1, instruction.ILIndex, instruction.Size);
			case 83:
				return new ILInstructionCode_Stind(ElementType.ELEMENT_TYPE_I2, instruction.ILIndex, instruction.Size);
			case 84:
				return new ILInstructionCode_Stind(ElementType.ELEMENT_TYPE_I4, instruction.ILIndex, instruction.Size);
			case 85:
				return new ILInstructionCode_Stind(ElementType.ELEMENT_TYPE_I8, instruction.ILIndex, instruction.Size);
			case 86:
				return new ILInstructionCode_Stind(ElementType.ELEMENT_TYPE_R4, instruction.ILIndex, instruction.Size);
			case 87:
				return new ILInstructionCode_Stind(ElementType.ELEMENT_TYPE_R8, instruction.ILIndex, instruction.Size);
			case 88:
				return new ILInstructionCode_Operator(BaseOperator.add, instruction.ILIndex, instruction.Size);
			case 89:
				return new ILInstructionCode_Operator(BaseOperator.sub, instruction.ILIndex, instruction.Size);
			case 90:
				return new ILInstructionCode_Operator(BaseOperator.mul, instruction.ILIndex, instruction.Size);
			case 91:
				return new ILInstructionCode_Operator(BaseOperator.div, instruction.ILIndex, instruction.Size);
			case 92:
				return new ILInstructionCode_Operator(BaseOperator.div, instruction.ILIndex, instruction.Size);
			case 93:
				return new ILInstructionCode_Operator(BaseOperator.rem, instruction.ILIndex, instruction.Size);
			case 94:
				return new ILInstructionCode_Operator(BaseOperator.rem, instruction.ILIndex, instruction.Size);
			case 95:
				return new ILInstructionCode_Operator(BaseOperator.and, instruction.ILIndex, instruction.Size);
			case 96:
				return new ILInstructionCode_Operator(BaseOperator.or, instruction.ILIndex, instruction.Size);
			case 97:
				return new ILInstructionCode_Operator(BaseOperator.xor, instruction.ILIndex, instruction.Size);
			case 98:
				return new ILInstructionCode_Operator(BaseOperator.shl, instruction.ILIndex, instruction.Size);
			case 99:
				return new ILInstructionCode_Operator(BaseOperator.shr, instruction.ILIndex, instruction.Size);
			case 100:
				return new ILInstructionCode_Operator(BaseOperator.shr, instruction.ILIndex, instruction.Size);
			case 101:
				return new ILInstructionCode_Operator(BaseOperator.neg, instruction.ILIndex, instruction.Size);
			case 102:
				return new ILInstructionCode_Operator(BaseOperator.not, instruction.ILIndex, instruction.Size);
			case 103:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I1, instruction.ILIndex, instruction.Size);
			case 104:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I2, instruction.ILIndex, instruction.Size);
			case 105:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I4, instruction.ILIndex, instruction.Size);
			case 106:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I8, instruction.ILIndex, instruction.Size);
			case 107:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_R4, instruction.ILIndex, instruction.Size);
			case 108:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_R8, instruction.ILIndex, instruction.Size);
			case 109:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U4, instruction.ILIndex, instruction.Size);
			case 110:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U8, instruction.ILIndex, instruction.Size);
			case 111:
				return new ILInstructionCode_CallVirt(instruction as Instruction_InlineMethod, instruction.ILIndex, instruction.Size);
			case 112:
				throw new Exception();
			case 113:
				return new ILInstructionCode_LdObj(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 114:
				return new ILInstructionCode_LdC(ElementType.ELEMENT_TYPE_STRING, (instruction as Instruction_InlineString).String, instruction.ILIndex, instruction.Size);
			case 115:
				return new ILInstructionCode_NewObj(instruction as Instruction_InlineMethod, instruction.ILIndex, instruction.Size);
			case 116:
				return new ILInstructionCode_CastClass(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 117:
				return new ILInstructionCode_IsInst(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 118:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_R4, instruction.ILIndex, instruction.Size);
			case 121:
				return new ILInstructionCode_Unbox(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 122:
				return new ILInstructionCode_Throw(instruction.ILIndex, instruction.Size);
			case 123:
				return new ILInstructionCode_LdFld((instruction as Instruction_InlineField).Field, instruction.ILIndex, instruction.Size);
			case 124:
				return new ILInstructionCode_LdFldA((instruction as Instruction_InlineField).Field, instruction.ILIndex, instruction.Size);
			case 125:
				return new ILInstructionCode_StFld((instruction as Instruction_InlineField).Field, instruction.ILIndex, instruction.Size);
			case 126:
				return new ILInstructionCode_LdFld((instruction as Instruction_InlineField).Field, instruction.ILIndex, instruction.Size);
			case 127:
				return new ILInstructionCode_LdFldA((instruction as Instruction_InlineField).Field, instruction.ILIndex, instruction.Size);
			case 128:
				return new ILInstructionCode_StFld((instruction as Instruction_InlineField).Field, instruction.ILIndex, instruction.Size);
			case 129:
				return new ILInstructionCode_StObj(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 130:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I1, instruction.ILIndex, instruction.Size, @checked: true);
			case 131:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I2, instruction.ILIndex, instruction.Size, @checked: true);
			case 132:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I4, instruction.ILIndex, instruction.Size, @checked: true);
			case 133:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I8, instruction.ILIndex, instruction.Size, @checked: true);
			case 134:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U1, instruction.ILIndex, instruction.Size, @checked: true);
			case 135:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U2, instruction.ILIndex, instruction.Size, @checked: true);
			case 136:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U4, instruction.ILIndex, instruction.Size, @checked: true);
			case 137:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U8, instruction.ILIndex, instruction.Size, @checked: true);
			case 138:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I, instruction.ILIndex, instruction.Size, @checked: true);
			case 139:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U, instruction.ILIndex, instruction.Size, @checked: true);
			case 140:
				return new ILInstructionCode_Box(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 141:
				return new ILInstructionCode_NewArray(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 142:
				return new ILInstructionCode_LdLen(instruction.ILIndex, instruction.Size);
			case 143:
				return new ILInstructionCode_LdElemA(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 144:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_I1, instruction.ILIndex, instruction.Size);
			case 145:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_U1, instruction.ILIndex, instruction.Size);
			case 146:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_I2, instruction.ILIndex, instruction.Size);
			case 147:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_U2, instruction.ILIndex, instruction.Size);
			case 148:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_I4, instruction.ILIndex, instruction.Size);
			case 149:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_U4, instruction.ILIndex, instruction.Size);
			case 150:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_I8, instruction.ILIndex, instruction.Size);
			case 151:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_I, instruction.ILIndex, instruction.Size);
			case 152:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_R4, instruction.ILIndex, instruction.Size);
			case 153:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_R8, instruction.ILIndex, instruction.Size);
			case 154:
				return new ILInstructionCode_LdElemType(ElementType.ELEMENT_TYPE_BYREF, instruction.ILIndex, instruction.Size);
			case 155:
				return new ILInstructionCode_StElemType(ElementType.ELEMENT_TYPE_I, instruction.ILIndex, instruction.Size);
			case 156:
				return new ILInstructionCode_StElemType(ElementType.ELEMENT_TYPE_I1, instruction.ILIndex, instruction.Size);
			case 157:
				return new ILInstructionCode_StElemType(ElementType.ELEMENT_TYPE_I2, instruction.ILIndex, instruction.Size);
			case 158:
				return new ILInstructionCode_StElemType(ElementType.ELEMENT_TYPE_I4, instruction.ILIndex, instruction.Size);
			case 159:
				return new ILInstructionCode_StElemType(ElementType.ELEMENT_TYPE_I8, instruction.ILIndex, instruction.Size);
			case 160:
				return new ILInstructionCode_StElemType(ElementType.ELEMENT_TYPE_R4, instruction.ILIndex, instruction.Size);
			case 161:
				return new ILInstructionCode_StElemType(ElementType.ELEMENT_TYPE_R8, instruction.ILIndex, instruction.Size);
			case 162:
				return new ILInstructionCode_StElemType(ElementType.ELEMENT_TYPE_BYREF, instruction.ILIndex, instruction.Size);
			case 163:
				return new ILInstructionCode_LdElemObj(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 164:
				return new ILInstructionCode_StElemObj(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 165:
				return new ILInstructionCode_Unbox(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 179:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I1, instruction.ILIndex, instruction.Size, @checked: true);
			case 180:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U1, instruction.ILIndex, instruction.Size, @checked: true);
			case 181:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I2, instruction.ILIndex, instruction.Size, @checked: true);
			case 182:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U2, instruction.ILIndex, instruction.Size, @checked: true);
			case 183:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I4, instruction.ILIndex, instruction.Size, @checked: true);
			case 184:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U4, instruction.ILIndex, instruction.Size, @checked: true);
			case 185:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I8, instruction.ILIndex, instruction.Size, @checked: true);
			case 186:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U8, instruction.ILIndex, instruction.Size, @checked: true);
			case 194:
				throw new Exception();
			case 195:
				throw new Exception();
			case 198:
				return new ILInstructionCode_MkRefAny(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 208:
				return ILInstructionCode_LdToken.Read(instruction as Instruction_InlineTok, instruction.ILIndex, instruction.Size);
			case 209:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U2, instruction.ILIndex, instruction.Size);
			case 210:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U1, instruction.ILIndex, instruction.Size);
			case 211:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I, instruction.ILIndex, instruction.Size);
			case 212:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_I, instruction.ILIndex, instruction.Size, @checked: true);
			case 213:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U, instruction.ILIndex, instruction.Size, @checked: true);
			case 214:
				return new ILInstructionCode_Operator(BaseOperator.add, instruction.ILIndex, instruction.Size, @checked: true);
			case 215:
				return new ILInstructionCode_Operator(BaseOperator.add, instruction.ILIndex, instruction.Size, @checked: true);
			case 216:
				return new ILInstructionCode_Operator(BaseOperator.mul, instruction.ILIndex, instruction.Size, @checked: true);
			case 217:
				return new ILInstructionCode_Operator(BaseOperator.mul, instruction.ILIndex, instruction.Size, @checked: true);
			case 218:
				return new ILInstructionCode_Operator(BaseOperator.sub, instruction.ILIndex, instruction.Size, @checked: true);
			case 219:
				return new ILInstructionCode_Operator(BaseOperator.sub, instruction.ILIndex, instruction.Size, @checked: true);
			case 220:
				return new ILInstructionCode_EndFinally(instruction.ILIndex, instruction.Size);
			case 221:
				return new ILInstructionCode_Leave((instruction as Instruction_InlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 222:
				return new ILInstructionCode_Leave((instruction as Instruction_ShortInlineBrTarget).Value, instruction.ILIndex, instruction.Size);
			case 223:
				return new ILInstructionCode_Stind(ElementType.ELEMENT_TYPE_I, instruction.ILIndex, instruction.Size);
			case 224:
				return new ILInstructionCode_Conv(ElementType.ELEMENT_TYPE_U, instruction.ILIndex, instruction.Size);
			case 65024:
				return new ILInstructionCode_ArgList(instruction.ILIndex, instruction.Size);
			case 65025:
				return new ILInstructionCode_Compare(BaseCompare.ceq, instruction.ILIndex, instruction.Size);
			case 65026:
				return new ILInstructionCode_Compare(BaseCompare.cgt, instruction.ILIndex, instruction.Size);
			case 65027:
				return new ILInstructionCode_Compare(BaseCompare.cgt, instruction.ILIndex, instruction.Size);
			case 65028:
				return new ILInstructionCode_Compare(BaseCompare.clt, instruction.ILIndex, instruction.Size);
			case 65029:
				return new ILInstructionCode_Compare(BaseCompare.clt, instruction.ILIndex, instruction.Size);
			case 65030:
				return new ILInstructionCode_LdFTN(instruction as Instruction_InlineMethod, instruction.ILIndex, instruction.Size);
			case 65031:
				return new ILInstructionCode_LdFTN(instruction as Instruction_InlineMethod, instruction.ILIndex, instruction.Size);
			case 65033:
				throw new Exception();
			case 65034:
				throw new Exception();
			case 65035:
				throw new Exception();
			case 65036:
				throw new Exception();
			case 65037:
				throw new Exception();
			case 65038:
				throw new Exception();
			case 65039:
				return new ILInstructionCode_Localloc(instruction.ILIndex, instruction.Size);
			case 65041:
				throw new Exception();
			case 65042:
				throw new Exception();
			case 65043:
				return new ILInstructionCode_Volatile(instruction.ILIndex, instruction.Size);
			case 65044:
				throw new Exception();
			case 65045:
				return new ILInstructionCode_InitObj(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 65046:
				return new ILInstructionCode_Constrained(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 65047:
				throw new Exception();
			case 65048:
				throw new Exception();
			case 65050:
				return new ILInstructionCode_Rethrow(instruction.ILIndex, instruction.Size);
			case 65052:
				return new ILInstructionCode_SizeOf(instruction as Instruction_InlineType, instruction.ILIndex, instruction.Size);
			case 65053:
				return new ILInstructionCode_RefAnyType(instruction.ILIndex, instruction.Size);
			case 65054:
				return new ILInstructionCode_Readonly(instruction.ILIndex, instruction.Size);
			default:
				throw new Exception();
			}
		}
		public override string Print(int tabs = 0)
		{
			return Info.Offset + "".PadLeft(tabs, '\t') + Print();
		}
		public abstract string Print();
	}
}
