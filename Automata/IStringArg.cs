namespace Automata
{
	public interface IStringArg
	{
		bool NotOver { get; }
		char Top();
		void Pop();
		char Last();
	}
}
