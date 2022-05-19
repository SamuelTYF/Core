using System.Text;

namespace Compiler
{
    public abstract class ITokenizer<TToken>where TToken:class
    {
        private Encoding Encoding;
        protected StringBuilder Value;
        private char[] Buffer;
        private int BufferLength;
        private int Length;
        protected int Index;
        private StreamReader Source;
        protected int State;
        public string _Error;
        public ITokenizer(Encoding encoding, int length = 1 << 10)
        {
            Encoding = encoding;
            BufferLength = length;
        }
        public ITokenizer(int length = 1 << 10) : this(Encoding.UTF8, length) { }
        private void InitValue() => Value.Clear();
        protected char Peek() => Index == Length && (Length = Source.Read(Buffer, 0, BufferLength)) == 0 ? '\0' : Buffer[Index];
        protected void Push(char value) => Value.Append(value);
        protected TToken Error(char symbol)
        {
            _Error = $"Tokenizor Error at State:{State} Index:{Index} Char:{symbol.FromEscape()}";
            return null;
        }
        public void StartParse(Stream stream)
        {
            Source = new(stream, Encoding);
            Index = 0;
            Length = 0;
            State = 0;
            Value = new();
            Buffer = new char[BufferLength];
            InitValue();
        }
        protected TToken ReturnToken(TToken token)
        {
            State = 0;
            InitValue();
            return token;
        }
        public abstract TToken Get();
    }
}
