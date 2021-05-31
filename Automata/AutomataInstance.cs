using System;

namespace Automata
{
	public sealed class AutomataInstance
	{
		public string[] Modes;
		public int[] Rules;
		public int ModeCount;
		public int InputCount;
		public int[] Functions;
		public int InitFunction;
		public bool EndCheck;
		public AutomataInstance(string[] modes, int[] rules, int inputCount, int[] functions, int initFunction, bool endCheck)
		{
			Modes = modes;
			Rules = rules;
			ModeCount = modes.Length;
			InputCount = inputCount;
			Functions = functions;
			InitFunction = initFunction;
			EndCheck = endCheck;
		}
		public Rule GetRule(int mode, int input)
		{
			int num = mode * InputCount + input << 1;
			return new Rule(mode, input, Rules[num], Rules[num | 1]);
		}
		public void RunFunction(AutomataHost host, int function)
		{
			if (function != int.MaxValue)
				host.Functions[function]();
		}
		public int Run(AutomataHost host, int offset)
		{
			RunFunction(host, Rules[offset]);
			return Rules[offset | 1] * InputCount << 1;
		}
		public void Run(AutomataHost host, string source, int start, int length)
		{
			if (!host.CheckFunctions(Functions))
				throw new Exception();
			RunFunction(host, InitFunction);
			int num = 0;
			int num2 = start;
			int num3 = start + length;
			while (num2 < num3)
			{
				host.Input = source[num2++];
				num = Run(host, num + (host.Input << 1));
				if (num == 0)break;
			}
			if (EndCheck && num != 0 && Run(host, num) != 0)
				throw new Exception();
		}
		public void Run<TStringArg>(AutomataHost host, TStringArg source) where TStringArg : IStringArg
		{
			if (!host.CheckFunctions(Functions))
				throw new Exception();
			RunFunction(host, InitFunction);
			int num = 0;
			while (source.NotOver)
			{
				host.Input = source.Top();
				num = Run(host, num + (host.Input << 1));
				if (num == 0)break;
				source.Pop();
			}
			if (EndCheck && num != 0 && Run(host, num) != 0)
				throw new Exception();
		}
		public void RunPlus<TStringArg>(AutomataHostPlus<TStringArg> host, TStringArg source) where TStringArg : IStringArg
		{
			if (!host.CheckFunctions(Functions))
				throw new Exception();
			host.Source = source;
			RunFunction(host, InitFunction);
			host.Mode = 0;
			host.NextMode = -1;
			while (source.NotOver)
			{
				host.Input = source.Top();
				host.Mode = Run(host, host.Mode + (host.Input << 1));
				if (host.NextMode >= 0)
				{
					host.Mode = host.NextMode;
					host.NextMode = -1;
				}
				if (host.Mode == 0)
					break;
				source.Pop();
			}
			if (EndCheck && host.Mode != 0)
			{
				host.Mode = Run(host, host.Mode);
				if (host.NextMode >= 0)
				{
					host.Mode = host.NextMode;
					host.NextMode = -1;
				}
				if (host.Mode != 0)
					throw new Exception();
			}
		}
	}
}
