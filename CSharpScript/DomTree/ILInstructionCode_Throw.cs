using System;
using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Throw : ILInstructionCode
	{
		public TypeDefOrRef _Type;
		public TypeDefOrRef Type
		{
			get
			{
				if (_Type == null)
				{
					ILCode iLCode = Parent.Codes[Info.Index - 1];
					if (iLCode is ILInstructionCode_NewObj)
						return _Type = (iLCode as ILInstructionCode_NewObj).Method.Parent;
					throw new Exception();
				}
				return _Type;
			}
		}
		public ILInstructionCode_Throw(int offset, int length)
			: base(offset, length, ILCodeFlag.Throw)
		{
		}
		public override string Print()
		{
			return "throw";
		}
	}
}
