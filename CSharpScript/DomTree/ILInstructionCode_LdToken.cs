using System;
using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public abstract class ILInstructionCode_LdToken : ILInstructionCode
	{
		public ILInstructionCode_LdToken(int offset, int length, ILCodeFlag flag)
			: base(offset, length, flag)
		{
		}
		public static ILInstructionCode_LdToken Read(Instruction_InlineTok token, int offset, int length)
		{
			return (token.Value >> 24) switch
			{
				10 => throw new Exception(), 
				1 => new ILInstructionCode_LdTypeToken(new TypeDefOrRef(token.TypeRef), offset, length), 
				2 => new ILInstructionCode_LdTypeToken(new TypeDefOrRef(token.TypeDef), offset, length), 
				4 => new ILInstructionCode_LdFieldToken(token.Field, offset, length), 
				27 => new ILInstructionCode_LdTypeToken(new TypeDefOrRef(token.TypeSpec), offset, length), 
				_ => throw new Exception(), 
			};
		}
	}
}
