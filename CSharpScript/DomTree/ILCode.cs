using CSharpScript.File;
namespace CSharpScript.DomTree
{
	public abstract class ILCode
	{
		public ILBlock Parent;
		public ILCodeInfo Info;
        public ILCode(int offset, int length, ILCodeFlag flag)
			=> Info = new ILCodeInfo
        {
            Offset = offset,
            Length = length,
            Flag = flag
        };
        public static ILMethod Parse(MethodDef method)
		{
			if (!method.HasMethodBody)
				return null;
			CorILMethod method2 = method.Method;
			return new ILMethod(method.ParamList, method2.LocalVars, ILBody.ParseBody(method, 0, method2.MethodHeader.CodeSize));
		}
		public abstract string Print(int tabs = 0);
        public override string ToString() => Print();
    }
}
