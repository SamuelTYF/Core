using System;
using System.Text;
using Collection;

namespace Automata
{
	public sealed class AKCHost : AutomataHost
	{
		public List<byte> TempBytes;
		public List<string> TempStrings;
		public AutomataKernel Kernel;
		public AutomataInstance Result;
		public void Init()
		{
			TempBytes = new List<byte>();
			TempStrings = new List<string>();
			Kernel = new AutomataKernel();
			Result = null;
		}
		public void Push()=>TempBytes.Add((byte)Input);
		public void Store()
		{
			TempStrings.Add(Encoding.UTF8.GetString(TempBytes.ToArray()));
			TempBytes.Clear();
		}
		public void Tackle()
		{
			Store();
			if (TempStrings.Length == 0)
				throw new Exception();
			switch (TempStrings[0])
			{
			case "SetInputCount":
				if (TempStrings.Length != 2 || TempStrings[1] == "")
					throw new Exception();
				Kernel.SetInputCount(int.Parse(TempStrings[1]));
				TempStrings.Clear();
				break;
			case "RegisterMode":
				if (TempStrings.Length != 2 || TempStrings[1] == "")
					throw new Exception();
				Kernel.RegisterMode(TempStrings[1]);
				TempStrings.Clear();
				break;
			case "LockModes":
				if (TempStrings.Length != 2 || TempStrings[1] != "")
					throw new Exception();
				TempStrings.Clear();
				Kernel.LockModes();
				break;
			case "RegisterFunction":
				if (TempStrings.Length != 2 || TempStrings[1] == "")
					throw new Exception();
				Kernel.RegisterFunction(TempStrings[1]);
				TempStrings.Clear();
				break;
			case "SetInitFunction":
				if (TempStrings.Length != 2 || TempStrings[1] == "")
					throw new Exception();
				Kernel.SetInitFunction(TempStrings[1]);
				TempStrings.Clear();
				break;
			case "RegisterRule":
				if (TempStrings.Length == 5)
				{
					if (TempStrings[1] == "" || TempStrings[2] == "" || TempStrings[3] == "" || TempStrings[4] == "")
						throw new Exception();
					string modeName = TempStrings[1];
					string text = TempStrings[2];
					string function = TempStrings[3];
					string nextMode = TempStrings[4];
					TempStrings.Clear();
					if (int.TryParse(text, out var result))
					{
						Kernel.RegisterRule(modeName, result, function, nextMode);
						break;
					}
					switch (text)
					{
					case "Number":
						Kernel.RegisterRule(modeName, AutomataKernel.Number, function, nextMode);
						break;
					case "English":
						Kernel.RegisterRule(modeName, AutomataKernel.English, function, nextMode);
						break;
					case "Sign":
						Kernel.RegisterRule(modeName, AutomataKernel.Sign, function, nextMode);
						break;
					default:
						throw new Exception();
					}
				}
				else
				{
					if (TempStrings.Length != 6)
						throw new Exception();
					if (TempStrings[1] == "" || TempStrings[2] == "" || TempStrings[3] == "" || TempStrings[4] == "" || TempStrings[5] == "")
						throw new Exception();
					string modeName = TempStrings[1];
					int num = int.Parse(TempStrings[2]);
					int num2 = int.Parse(TempStrings[3]);
					string function = TempStrings[4];
					string nextMode = TempStrings[5];
					TempStrings.Clear();
					for (int i = num; i <= num2; i++)
						Kernel.RegisterRule(modeName, i, function, nextMode);
				}
				break;
			case "SetEndCheck":
				if (TempStrings.Length != 2 || TempStrings[1] == "")
					throw new Exception();
				Kernel.SetEndCheck(bool.Parse(TempStrings[1]));
				TempStrings.Clear();
				break;
			default:throw new Exception();
			}
		}
		public void Build()=>Result = Kernel.CreateInstance();
		public override HashTable<Function> MarkFunctions()
		{
			HashTable<Function> hashTable = new HashTable<Function>();
			hashTable.Register(Throw, "Throw".GetHashCode());
			hashTable.Register(Init, "Init".GetHashCode());
			hashTable.Register(Push, "Push".GetHashCode());
			hashTable.Register(Store, "Store".GetHashCode());
			hashTable.Register(Tackle, "Tackle".GetHashCode());
			hashTable.Register(Build, "Build".GetHashCode());
			return hashTable;
		}
	}
}
