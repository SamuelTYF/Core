namespace Compiler
{
    public abstract class IParser<TToken,TResult> where TResult : class where TToken:class
    {
        protected Stack<int> StateStack;
        protected Stack<TToken> TokenStack;
        public IParser()
        {
        }
        protected void Init()
        {
            StateStack = new();
            TokenStack = new();
            StateStack.Push(0);
            //Init
        }
        protected TResult Error(TToken token)
        {
            Console.Error.WriteLine(StateStack.Peek());
            Console.Error.WriteLine(token);
            return null;
        }
        protected TToken[] PopToken(int k)
        {
            TToken[] tokens = new TToken[k];
            for(int i=0;i<k;i++)
            {
                tokens[k-i-1]=TokenStack.Pop();
                StateStack.Pop();
            }
            return tokens;
        }
        public abstract TResult Parse(ITokenizer<TToken> tokenizer);
    }
}
