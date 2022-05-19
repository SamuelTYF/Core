namespace Compiler.Tokenizor
{
    public class CharRange
    {
        public char Min;
        public char Max;
        public CharRange(char min, char max)
        {
            Min = min;
            Max = max;
        }
        public override string ToString() => $"{Min.FromEscape()}-{Max.FromEscape()}";
    }
}
