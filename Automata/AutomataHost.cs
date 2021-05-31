using System;

namespace Automata
{
	public abstract class AutomataHost
	{
		public int Input;

		public Function[] Functions { get; private set; }

		public int[] FunctionHash { get; private set; }

		public AutomataHost()
		{
			Input = 0;
			HashTable<Function> hashTable = MarkFunctions();
			Functions = hashTable.ToArray();
			FunctionHash = hashTable.GetHashArray();
		}

		public void Throw()
		{
			throw new Exception();
		}

		public abstract HashTable<Function> MarkFunctions();

		public bool CheckFunctions(int[] functions)
		{
			if (functions.Length > FunctionHash.Length)
			{
				return false;
			}
			for (int i = 0; i < functions.Length; i++)
			{
				if (FunctionHash[i] != functions[i])
				{
					return false;
				}
			}
			return true;
		}
	}
}
