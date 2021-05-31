namespace Automata
{
	public sealed class AutomataKernel
	{
		public HashTable<string> Modes;
		public int[] Rules;
		public int InputCount;
		public HashTable<string> Functions;
		public int InitFunction;
		public bool EndCheck;
		public static readonly int[] LowerEnglis = new int[26]
		{
			97, 98, 99, 100, 101, 102, 103, 104, 105, 106,
			107, 108, 109, 110, 111, 112, 113, 114, 115, 116,
			117, 118, 119, 120, 121, 122
		};
		public static readonly int[] UpperEnglish = new int[26]
		{
			65, 66, 67, 68, 69, 70, 71, 72, 73, 74,
			75, 76, 77, 78, 79, 80, 81, 82, 83, 84,
			85, 86, 87, 88, 89, 90
		};
		public static readonly int[] English = new int[52]
		{
			65, 66, 67, 68, 69, 70, 71, 72, 73, 74,
			75, 76, 77, 78, 79, 80, 81, 82, 83, 84,
			85, 86, 87, 88, 89, 90, 97, 98, 99, 100,
			101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
			111, 112, 113, 114, 115, 116, 117, 118, 119, 120,
			121, 122
		};
		public static readonly int[] Number = new int[10] 
		{ 
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57 
		};
		public static readonly int[] Sign = new int[29]
		{
			44, 46, 60, 62, 40, 41, 91, 93, 123, 125,
			92, 39, 34, 45, 61, 45, 43, 126, 96, 95,
			42, 47, 58, 59, 124, 63, 64, 38, 37
		};
		public static readonly int[] System16 = new int[16]
		{
			48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
			65, 66, 67, 68, 69, 70
		};
		public int ModeCount => Modes.ModeCount;
		public AutomataKernel()
		{
			Modes = new HashTable<string>();
			Rules = null;
			InputCount = 0;
			Functions = new HashTable<string>();
			InitFunction = int.MaxValue;
			EndCheck = false;
		}
		public void Reset()
		{
			Modes.Clear();
			Rules = null;
			InputCount = 0;
			Functions.Clear();
			InitFunction = int.MaxValue;
			EndCheck = false;
		}
		public int RegisterMode(string modeName)
			=>Modes.Register(modeName, modeName.GetHashCode());
		public void LockModes()
			=>Rules = new int[ModeCount * InputCount << 1];
		public void SetInputCount(int max)
			=>InputCount = max;
		public int RegisterFunction(string function)
			=>Functions.Register(function, function.GetHashCode());
		public void RegisterRule(int mode, int input, int function, int next)
		{
			int num = mode * InputCount + input << 1;
			Rules[num] = function;
			Rules[num | 1] = next;
		}
		public void RegisterRule(string modeName, int input, string function, string nextMode)
			=>RegisterRule(RegisterMode(modeName), input, (function == "null") ? int.MaxValue : RegisterFunction(function), RegisterMode(nextMode));
		public void RegisterRule(string modeName, int[] inputs, string function, string nextMode)
		{
			foreach (int input in inputs)
				RegisterRule(RegisterMode(modeName), input, (function == null) ? int.MaxValue : RegisterFunction(function), RegisterMode(nextMode));
		}
		public void SetInitFunction(int function)
			=>InitFunction = function;
		public void SetInitFunction(string function)
			=>SetInitFunction(RegisterFunction(function));
		public void SetEndCheck(bool tf)=>EndCheck = tf;
		public AutomataInstance CreateInstance()
			=>new AutomataInstance(Modes.ToArray(), Rules, InputCount, Functions.GetHashArray(), InitFunction, EndCheck);
	}
}
