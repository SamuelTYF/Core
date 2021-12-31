using System;
using Wolfram.NETLink.Internal;

namespace Wolfram.NETLink
{
	public class MathDelegate
	{
        public static Delegate CreateDelegate(Type delegateType, string mFunc) => CreateDelegate(delegateType, mFunc, null, callsUnshare: false, wrapInNETBlock: true);

        public static Delegate CreateDelegate(Type delegateType, string mFunc, IKernelLink ml) => CreateDelegate(delegateType, mFunc, ml, callsUnshare: false, wrapInNETBlock: true);

        public static Delegate CreateDelegate(Type delegateType, string mFunc, IKernelLink ml, bool callsUnshare, bool wrapInNETBlock) => Delegate.CreateDelegate(delegateType, DelegateHelper.createDynamicMethod(ml, delegateType, mFunc, -1, callsUnshare, wrapInNETBlock: true));

        private MathDelegate()
		{
		}
	}
}
