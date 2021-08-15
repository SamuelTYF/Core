using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Collection;
namespace CSharpScript.File
{
	public class MethodBody
	{
		public Instruction[] Instructions;
		public AVL<int, Instruction> _Instructions;
		public MethodBody(byte[] bs, MetadataRoot metadata)
		{
			_Instructions = new AVL<int, Instruction>();
			int i = 0;
			for (int num = bs.Length; i < num; i++)
			{
				int num2 = i;
				Operator @operator = ((bs[i] == 254) ? Operators.Operators2[bs[++i]] : Operators.Operators1[bs[i]]);
				Instruction value = @operator.OperandType switch
				{
					OperandType.InlineBrTarget => new Instruction_InlineBrTarget(num2, @operator, bs, ref i), 
					OperandType.InlineField => new Instruction_InlineField(num2, @operator, bs, ref i), 
					OperandType.InlineI => new Instruction_InlineI(num2, @operator, bs, ref i), 
					OperandType.InlineI8 => new Instruction_InlineI8(num2, @operator, bs, ref i), 
					OperandType.InlineMethod => new Instruction_InlineMethod(num2, @operator, bs, ref i), 
					OperandType.InlineNone => new Instruction_InlineNone(num2, @operator, bs, ref i), 
					OperandType.InlineR => new Instruction_InlineR(num2, @operator, bs, ref i), 
					OperandType.InlineSig => new Instruction_InlineSig(num2, @operator, bs, ref i), 
					OperandType.InlineString => new Instruction_InlineString(num2, @operator, bs, ref i), 
					OperandType.InlineSwitch => new Instruction_InlineSwitch(num2, @operator, bs, ref i), 
					OperandType.InlineTok => new Instruction_InlineTok(num2, @operator, bs, ref i), 
					OperandType.InlineType => new Instruction_InlineType(num2, @operator, bs, ref i), 
					OperandType.InlineVar => new Instruction_InlineVar(num2, @operator, bs, ref i), 
					OperandType.ShortInlineBrTarget => new Instruction_ShortInlineBrTarget(num2, @operator, bs, ref i), 
					OperandType.ShortInlineI => new Instruction_ShortInlineI(num2, @operator, bs, ref i), 
					OperandType.ShortInlineR => new Instruction_ShortInlineR(num2, @operator, bs, ref i), 
					OperandType.ShortInlineVar => new Instruction_ShortInlineVar(num2, @operator, bs, ref i), 
					_ => throw new Exception(), 
				};
				_Instructions[num2] = value;
			}
			Instructions = _Instructions.LDRSort();
			Instruction[] instructions = Instructions;
			foreach (Instruction instruction in instructions)
			{
				if (instruction is UpdatableInstruction)
					(instruction as UpdatableInstruction).Update(_Instructions, metadata);
			}
		}
        public override string ToString() => string.Join("\n", (IEnumerable<Instruction>)Instructions);
    }
}
