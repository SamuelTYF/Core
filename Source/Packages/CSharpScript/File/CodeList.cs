using System;
using Collection;
namespace CSharpScript.File
{
	public class CodeList
	{
		public CodeList Last;
		public List<CSharpCode> Codes;
		public int Stack;
		public int LastStack;
		public int LastCode;
		public CodeList(CodeList last = null)
		{
			Last = last;
			Codes = new();
			LastCode = last?.Codes.Length ?? 0;
			LastStack = 0;
			Stack = 0;
		}
		public CSharpCode Pop()
		{
			if (Stack == 0)
			{
				if (LastStack == LastCode)
					throw new Exception();
				LastStack++;
				return Last.Codes[LastCode - LastStack];
			}
			Stack--;
			return Codes.Pop();
		}
		public void Add(CSharpCode code, bool isvoid = false)
		{
			Codes.Add(code);
			if (!isvoid)
				Stack++;
		}
        public CSharpCode ToCode() => Codes.Length == 1 ? Codes[0] : new CSharpCodeBlock(Codes.ToArray());
        public CSharpCode Return()
		{
			Codes.Add(new CSharpCodeReturn((Stack == 0) ? null : Codes.Pop()));
			return ToCode();
		}
        public CSharpCode Top() => Stack == 0 ? throw new Exception() : Codes[^1];
        public void Update(int stack) => Codes.Pop(stack);
        public CSharpCode Pop(int length) => length == 1 ? Codes.Pop() : new CSharpCodeBlock(Codes.Pop(length));
    }
}
