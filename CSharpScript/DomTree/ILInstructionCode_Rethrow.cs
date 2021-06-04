using System;
using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public class ILInstructionCode_Rethrow : ILInstructionCode
	{
		public ILCatch _Catch;
		public TypeDefOrRef Type => Catch.Type;
		public ILCatch Catch
		{
			get
			{
				if (_Catch == null)
				{
					ILBlock parent = Parent;
					while (parent != null)
					{
						if ((parent = Parent.Parent) is ILCatch)
							return _Catch = parent as ILCatch;
					}
					throw new Exception();
				}
				return _Catch;
			}
		}
		public ILInstructionCode_Rethrow(int offset, int length)
			: base(offset, length, ILCodeFlag.Rethrow)
		{
		}
		public override string Print()
		{
			return "rethrow";
		}
	}
}
