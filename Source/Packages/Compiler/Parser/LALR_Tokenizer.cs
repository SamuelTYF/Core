using System.Text;

namespace Compiler.Parser
{
    public class LALR_Tokenizer:ITokenizer<Token>
	{
		public LALR_Tokenizer(Encoding encoding, int length = 1 << 10) : base(encoding, length) { }
		public LALR_Tokenizer(int length = 1 << 10) : base(length) { }
		public override Token Get()
		{
			Token token;
			while (true)
			{
				char symbol = Peek();
				switch (State)
				{
					case 0:
						if (symbol is '\0') return new Token("EOF");
						else if (symbol is '\'') { State = 1; Index++; }
						else if (symbol is '-') { State = 2; Index++; }
						else if (symbol is '<') { State = 3; Index++; }
						else return Error(symbol);
						break;
					case 1:
						if (symbol is >= ' ' and <= '&' or >= '(' and <= '\uffff') { Push(symbol); State = 1; Index++; }
						else if (symbol is '\'') { State = 4; Index++; }
						else return Error(symbol);
						break;
					case 2:
						if (symbol is '>') { State = 5; Index++; }
						else return Error(symbol);
						break;
					case 3:
						if (symbol is >= ' ' and <= '=' or >= '?' and <= '\uffff') { Push(symbol); State = 3; Index++; }
						else if (symbol is '>') { State = 6; Index++; }
						else return Error(symbol);
						break;
					case 4:
						token = Token.Terminal(Value.ToString()); return ReturnToken(token);
					case 5:
						token = new("Define"); return ReturnToken(token);
					case 6:
						token = Token.Variable(Value.ToString()); return ReturnToken(token);
					default:
						return Error(symbol);
				}
			}
		}
		public Delta ParseDelta(string text)
        {
			StartParse(new MemoryStream(Encoding.UTF8.GetBytes(text)));
			Token token = Get();
			if (token == null) return null;
			if (token.Type != "Symbol"||token.Value_Symbol==null)
            {
				_Error = $"LALR_Tokenizer Error Expect Symbol {token.Type}";
				return null;
            }
			Symbol start = token.Value_Symbol;
			if(!start.IsVariable)
			{
				_Error = $"LALR_Tokenizer Error Expect Variable {start.Name}";
				return null;
			}
			token = Get();
			if (token == null) return null;
			if(token.Type!= "Define")
            {
				_Error = $"LALR_Tokenizer Error Expect Define {token.Type}";
				return null;
			}
			List<Symbol> deltas = new();
			while(true)
            {
				token = Get();
				if (token == null) return null;
				if (token.Type == "EOF") break;
				if(token.Type!="Symbol"||token.Value_Symbol==null)
                {
					_Error = $"LALR_Tokenizer Error Expect Symbol {token.Type}";
					return null;
				}
				deltas.Add(token.Value_Symbol);
            }
			return new(start, deltas.ToArray());
		}
	}
}