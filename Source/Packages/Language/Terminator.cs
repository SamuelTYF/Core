namespace Language
{
    public class Terminator
    {
        public char Value;
        public int Index;
        public Terminator(char value,int index)
        {
            Value = value;
            Index = index;
        }
        public override string ToString() => $"{Value}";
    }
}
