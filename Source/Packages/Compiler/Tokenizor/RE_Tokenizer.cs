using System.Text;

namespace Compiler.Tokenizor
{
	public class RE_Tokenizer:ITokenizer<Token>
	{
		public RE_Tokenizer(Encoding encoding, int length = 1 << 10) : base(encoding, length) { }
		public RE_Tokenizer(int length = 1 << 10) : base(length) { }
		public override Token Get()
		{
			char Temp_Char = '\0';
			int Temp_Int = 0;
			while (true)
			{
				char symbol = Peek();
				switch (State)
				{
					case 0:
						if (symbol is '\0') return new Token("EOF");
						else if(symbol is >= '(' and <= '+' or '-' or '[' or ']' or '|') { Temp_Char = symbol; State = 2; Index++; }
						else if (symbol is '\\') { State = 3; Index++; }
						else { Temp_Char = symbol; State = 1; Index++; }
						break;
					case 1:
						return ReturnToken(new Token(Temp_Char));
					case 2:
						return ReturnToken(new Token(Temp_Char.ToString()));
					case 3:
						if (symbol is 'a') { Temp_Char = '\a'; State = 1; Index++; }
						else if (symbol is 'b') { Temp_Char = '\b'; State = 1; Index++; }
						else if (symbol is 'd') {  State = 4; Index++; }
						else if (symbol is 'f') { Temp_Char = '\f'; State = 1; Index++; }
						else if (symbol is 'n') { Temp_Char = '\n'; State = 1; Index++; }
						else if (symbol is 'r') { Temp_Char = '\r'; State = 1; Index++; }
						else if (symbol is 's') { Temp_Char = ' '; State = 1; Index++; }
						else if (symbol is 't') { Temp_Char = '\t'; State = 1; Index++; }
						else if (symbol is 'v') { Temp_Char = '\v'; State = 1; Index++; }
						else if (symbol is 'x') { State = 5; Index++; }
						else if (symbol is '(' or ')' or '[' or '-' or ']' or '+' or '|' or '*' or '\\') { Temp_Char = symbol; State = 1; Index++; }
						else { return Error(symbol); }
						break;
					case 4:
						if (symbol is >= '0' and <= '9') { Temp_Int = Temp_Int * 10 + (symbol ^ 48); State = 6; Index++; }
						else { return Error(symbol); }
						break;
					case 5:
						if (symbol is >= '0' and <= '9') { Temp_Int = (Temp_Int << 4) | (symbol ^ 48); State = 7; Index++; }
						else if (symbol is >= 'a' and <= 'f') { Temp_Int = (Temp_Int << 4) | (10 + symbol -'a'); State = 7; Index++; }
						else if (symbol is >= 'A' and <= 'F') { Temp_Int = (Temp_Int << 4) | (10 + symbol -'A'); State = 7; Index++; }
						else { return Error(symbol); }
						break;
					case 6:
						if (symbol is >= '0' and <= '9') { Temp_Int = Temp_Int * 10 + (symbol ^ 48); State = 6; Index++; }
						else return ReturnToken(new Token((char)Temp_Int));
						break;
					case 7:
						if (symbol is >= '0' and <= '9') { Temp_Int = (Temp_Int << 4) | (symbol ^ 48); State = 7; Index++; }
						else if (symbol is >= 'a' and <= 'f') { Temp_Int = (Temp_Int << 4) | (10 + symbol - 'a'); State = 7; Index++; }
						else if (symbol is >= 'A' and <= 'F') { Temp_Int = (Temp_Int << 4) | (10 + symbol - 'A'); State = 7; Index++; }
						else return ReturnToken(new Token((char)Temp_Int));
						break;
					default:
						return Error(symbol);
				}
			}
		}
	}
}
