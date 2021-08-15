using System;

namespace Wolfram.NETLink
{
	public class MathLinkException : ApplicationException
	{
		public const int MLEOK = 0;

		public const int MLEUSER = 1000;

		public const int MLE_NON_ML_ERROR = 1000;

		public const int MLE_OUT_OF_MEMORY = 1001;

		public const int MLE_BAD_ARRAY_DEPTH = 1002;

		public const int MLE_BAD_ARRAY_SHAPE = 1003;

		public const int MLE_ARRAY_NOT_RECTANGULAR = 1004;

		public const int MLE_BAD_ARRAY = 1005;

		public const int MLE_EMPTY_ARRAY = 1006;

		public const int MLE_MULTIDIM_ARRAY_OF_ARRAY = 1007;

		public const int MLE_ARRAY_OF_ARRAYCLASS = 1008;

		public const int MLE_BAD_COMPLEX = 1009;

		public const int MLE_NO_COMPLEX = 1010;

		public const int MLE_BAD_ENUM = 1011;

		public const int MLE_CREATION_FAILED = 1012;

		public const int MLE_CONNECT_TIMEOUT = 1013;

		public const int MLE_GETFUNCTION = 1014;

		public const int MLE_CHECKFUNCTION = 1015;

		public const int MLE_SYMBOL = 1016;

		public const int MLE_WRAPPED_EXCEPTION = 1017;

		public const int MLE_FIRST_USER_EXCEPTION = 2000;

		public const int MLE_BAD_OBJECT = 1100;

		private int code;

		public int ErrCode => code;

		public MathLinkException(int code, string msg)
			: base("Error code: " + code + ". " + msg)
		{
			this.code = code;
		}

		public MathLinkException(int code)
			: this(code, lookupMessageText(code))
		{
		}

		private static string lookupMessageText(int code)
		{
			string text = null;
			switch (code)
			{
			case 1002:
				return "You are attempting to read an array using a depth specification that does not match the incoming array.";
			case 1003:
				return "The array being read has an irregular shape that cannot be read as any .NET type.";
			case 1004:
				return "The array being read is not rectangular. It is either jagged (e.g., {{1,2,3},{4,5}}) or misshapen (e.g., {{1,2,3},4}).";
			case 1005:
				return "Array contains data of a type that cannot be read as a native .NET type (for example, a Mathematica symbol or function).";
			case 1007:
				return "Cannot read arays that are jagged but start with a multidimensionl array, for example int[,][].";
			case 1008:
				return "Cannot read arrays whose element type is the Array class, i.e. Array[].";
			case 1006:
				return "The array being read is empty and there is no .NET type info available, so it is impossible to determine the correct .NET array type.";
			case 1009:
				return "Expression could not be read as a complex number.";
			case 1010:
				return "Complex numbers cannot be read or sent unless a Type to represent them is designated using IMathLink.SetComplexType().";
			case 1013:
				return "The link was not connected before the requested time limit elapsed.";
			case 1014:
				return "GetFunction() was called when the expression waiting on the link was not a function.";
			case 1015:
				return "The expression waiting on the link did not match the specification in CheckFunction() or CheckFunctionWithArgCount().";
			case 1100:
				return "The expression waiting on the link is not a valid .NET object reference.";
			case 1016:
				return "The expression waiting on the link is a symbol. You cannot use GetObject() to read a symbol because it cannot be represented as a .NET object.";
			default:
				return "Extended error message not available.";
			}
		}
	}
}
