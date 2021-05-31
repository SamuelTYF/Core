namespace Automata
{
	public struct Rule
	{
		public int Mode;
		public int Input;
		public int Function;
		public int NextMode;
		public Rule(int mode, int input, int function, int nextmode)
		{
			Mode = mode;
			Input = input;
			Function = function;
			NextMode = nextmode;
		}
	}
}
