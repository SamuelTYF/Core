namespace Automata
{
    public abstract class AutomataHostPlus<TStringArg> : AutomataHost where TStringArg : IStringArg
    {
        public TStringArg Source;
        public int Mode;
        public int NextMode;
        public AutomataHostPlus()
        {
        }
    }
}
