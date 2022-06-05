using System.Text;
using Compiler;
namespace CSharpCompiler
{
	public class CSharp_Tokenizer : ITokenizer<Token>
	{
		public CSharp_Tokenizer(Encoding encoding, int length = 1 << 10) : base(encoding, length) { }
		public CSharp_Tokenizer(int length = 1 << 10) : base(length) { }
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
						else if (symbol is >= '\t' and <= '\n' or '\r' or ' ') { State = 1; Index++; }
						else if (symbol is '!' or '<' or '>') { Push(symbol); State = 2; Index++; }
						else if (symbol is '"') { Push(symbol); State = 3; Index++; }
						else if (symbol is '%' or >= '(' and <= '*' or ',' or '/' or >= ':' and <= ';' or '?' or '[' or >= ']' and <= '^' or '{' or '}') { Push(symbol); State = 4; Index++; }
						else if (symbol is '&') { Push(symbol); State = 5; Index++; }
						else if (symbol is '\'') { Push(symbol); State = 6; Index++; }
						else if (symbol is '+') { Push(symbol); State = 7; Index++; }
						else if (symbol is '-') { Push(symbol); State = 8; Index++; }
						else if (symbol is '.') { Push(symbol); State = 9; Index++; }
						else if (symbol is '0') { Push(symbol); State = 10; Index++; }
						else if (symbol is >= '1' and <= '9') { Push(symbol); State = 11; Index++; }
						else if (symbol is '=') { Push(symbol); State = 12; Index++; }
						else if (symbol is '@') { Push(symbol); State = 13; Index++; }
						else if (symbol is >= 'A' and <= 'Z' or >= 'a' and <= 'z') { Push(symbol); State = 14; Index++; }
						else if (symbol is '|') { Push(symbol); State = 15; Index++; }
						else return Error(symbol);
						break;
					case 1:
						if (symbol is >= '\t' and <= '\n' or '\r' or ' ') { State = 1; Index++; }
						else if (symbol is '!' or '<' or '>') { Push(symbol); State = 2; Index++; }
						else if (symbol is '%' or >= '(' and <= '*' or ',' or '/' or >= ':' and <= ';' or '?' or '[' or >= ']' and <= '^' or '{' or '}') { Push(symbol); State = 4; Index++; }
						else if (symbol is '&') { Push(symbol); State = 5; Index++; }
						else if (symbol is '+') { Push(symbol); State = 7; Index++; }
						else if (symbol is '-') { Push(symbol); State = 8; Index++; }
						else if (symbol is '=') { Push(symbol); State = 12; Index++; }
						else if (symbol is '|') { Push(symbol); State = 15; Index++; }
						else { token = new(" "); return ReturnToken(token); }
						break;
					case 2:
						if (symbol is '=') { Push(symbol); State = 4; Index++; }
						else { token = new(Value.ToString()); return ReturnToken(token); }
						break;
					case 3:
						StringBuilder sb = new();
						char? c;
						do
						{
							CharState = 0;
							c = GetChar();
							if (c.HasValue) sb.Append(c.Value);
							else return Error(symbol);
						} while (Peek() != '"');
						token = Token.String(sb.ToString());
						Index++; return ReturnToken(token);
						break;
					case 4:
						token = new(Value.ToString()); return ReturnToken(token);
						break;
					case 5:
						if (symbol is '&') { Push(symbol); State = 4; Index++; }
						else { token = new(Value.ToString()); return ReturnToken(token); }
						break;
					case 6:
						if (symbol is >= ' ' and <= '&' or >= '(' and <= '[' or >= ']' and <= '\uffff') { Push(symbol); State = 16; Index++; }
						else if (symbol is '\\') { Push(symbol); State = 17; Index++; }
						else return Error(symbol);
						break;
					case 7:
						if (symbol is '+' or '=') { Push(symbol); State = 4; Index++; }
						else { token = new(Value.ToString()); return ReturnToken(token); }
						break;
					case 8:
						if (symbol is '-' or '=') { Push(symbol); State = 4; Index++; }
						else if (symbol is >= '0' and <= '9') { Push(symbol); State = 11; Index++; }
						else { token = new(Value.ToString()); return ReturnToken(token); }
						break;
					case 9:
						if (symbol is '?') { Push(symbol); State = 4; Index++; }
						else { token = new(Value.ToString()); return ReturnToken(token); }
						break;
					case 10:
						if (symbol is >= '0' and <= '9') { Push(symbol); State = 11; Index++; }
						else if (symbol is '.') { Push(symbol); State = 18; Index++; }
						else if (symbol is 'b') { Push(symbol); State = 19; Index++; }
						else if (symbol is 'x') { Push(symbol); State = 20; Index++; }
						else { token = Token.Int32(int.Parse(Value.ToString())); return ReturnToken(token); }
						break;
					case 11:
						if (symbol is >= '0' and <= '9') { Push(symbol); State = 11; Index++; }
						else if (symbol is '.') { Push(symbol); State = 18; Index++; }
						else { token = Token.Int32(int.Parse(Value.ToString())); return ReturnToken(token); }
						break;
					case 12:
						if (symbol is >= '=' and <= '>') { Push(symbol); State = 4; Index++; }
						else { token = new(Value.ToString()); return ReturnToken(token); }
						break;
					case 13:
						if (symbol is '"') { Push(symbol); State = 21; Index++; }
						else if (symbol is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z') { Push(symbol); State = 22; Index++; }
						else { token = new(Value.ToString()); return ReturnToken(token); }
						break;
					case 14:
						if (symbol is >= '0' and <= '9' or >= 'A' and <= 'Z' or '_' or >= 'a' and <= 'z') { Push(symbol); State = 14; Index++; }
						else { token = Keys.Contains(Value.ToString()) ? (new(Value.ToString())) : Token.Symbol(Value.ToString()); return ReturnToken(token); }
						break;
					case 15:
						if (symbol is '|') { Push(symbol); State = 4; Index++; }
						else { token = new(Value.ToString()); return ReturnToken(token); }
						break;
					case 16:
						if (symbol is '\'') { Push(symbol); State = 23; Index++; }
						else return Error(symbol);
						break;
					case 17:
						if (symbol is '\'') { Push(symbol); State = 24; Index++; }
						else if (symbol is '0') { Push(symbol); State = 25; Index++; }
						else if (symbol is '\\') { Push(symbol); State = 26; Index++; }
						else if (symbol is 'a') { Push(symbol); State = 27; Index++; }
						else if (symbol is 'b') { Push(symbol); State = 28; Index++; }
						else if (symbol is 'f') { Push(symbol); State = 29; Index++; }
						else if (symbol is 'n') { Push(symbol); State = 30; Index++; }
						else if (symbol is 'r') { Push(symbol); State = 31; Index++; }
						else if (symbol is 't') { Push(symbol); State = 32; Index++; }
						else if (symbol is 'u') { Push(symbol); State = 33; Index++; }
						else if (symbol is 'v') { Push(symbol); State = 34; Index++; }
						else return Error(symbol);
						break;
					case 18:
						if (symbol is >= '0' and <= '9') { Push(symbol); State = 35; Index++; }
						else return Error(symbol);
						break;
					case 19:
						if (symbol is >= '0' and <= '1') { Push(symbol); State = 36; Index++; }
						else return Error(symbol);
						break;
					case 20:
						if (symbol is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f') { Push(symbol); State = 37; Index++; }
						else return Error(symbol);
						break;
					case 21:
						if (symbol is >= '\u0001' and <= '!' or >= '#' and <= '\uffff') { Push(symbol); State = 21; Index++; }
						else if (symbol is '"') { Push(symbol); State = 38; Index++; }
						else return Error(symbol);
						break;
					case 22:
						if (symbol is >= '0' and <= '9' or >= 'A' and <= 'Z' or >= 'a' and <= 'z') { Push(symbol); State = 22; Index++; }
						else { if (Keys.Contains(Value.ToString()[1..])) token = Token.Symbol(Value.ToString()[1..]); else return Error(symbol); return ReturnToken(token); }
						break;
					case 23:
						token = Token.Char(Value[1]); return ReturnToken(token);
						break;
					case 24:
						if (symbol is '\'') { Push(symbol); State = 39; Index++; }
						else return Error(symbol);
						break;
					case 25:
						if (symbol is '\'') { Push(symbol); State = 40; Index++; }
						else return Error(symbol);
						break;
					case 26:
						if (symbol is '\'') { Push(symbol); State = 41; Index++; }
						else return Error(symbol);
						break;
					case 27:
						if (symbol is '\'') { Push(symbol); State = 42; Index++; }
						else return Error(symbol);
						break;
					case 28:
						if (symbol is '\'') { Push(symbol); State = 43; Index++; }
						else return Error(symbol);
						break;
					case 29:
						if (symbol is '\'') { Push(symbol); State = 44; Index++; }
						else return Error(symbol);
						break;
					case 30:
						if (symbol is '\'') { Push(symbol); State = 45; Index++; }
						else return Error(symbol);
						break;
					case 31:
						if (symbol is '\'') { Push(symbol); State = 46; Index++; }
						else return Error(symbol);
						break;
					case 32:
						if (symbol is '\'') { Push(symbol); State = 47; Index++; }
						else return Error(symbol);
						break;
					case 33:
						if (symbol is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f') { Push(symbol); State = 48; Index++; }
						else return Error(symbol);
						break;
					case 34:
						if (symbol is '\'') { Push(symbol); State = 49; Index++; }
						else return Error(symbol);
						break;
					case 35:
						if (symbol is >= '0' and <= '9') { Push(symbol); State = 35; Index++; }
						else if (symbol is 'e') { Push(symbol); State = 50; Index++; }
						else { token = Token.Double(double.Parse(Value.ToString())); return ReturnToken(token); }
						break;
					case 36:
						if (symbol is >= '0' and <= '1') { Push(symbol); State = 36; Index++; }
						else { token = Token.UInt32(Convert.ToUInt32(Value.ToString()[2..], 2)); return ReturnToken(token); }
						break;
					case 37:
						if (symbol is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f') { Push(symbol); State = 37; Index++; }
						else { token = Token.UInt32(uint.Parse(Value.ToString()[2..], System.Globalization.NumberStyles.HexNumber)); return ReturnToken(token); }
						break;
					case 38:
						token = Token.String(Value.ToString()[2..^1]); return ReturnToken(token);
						break;
					case 39:
						token = Token.Char('\''); return ReturnToken(token);
						break;
					case 40:
						token = Token.Char('\0'); return ReturnToken(token);
						break;
					case 41:
						token = Token.Char('\\'); return ReturnToken(token);
						break;
					case 42:
						token = Token.Char('\a'); return ReturnToken(token);
						break;
					case 43:
						token = Token.Char('\b'); return ReturnToken(token);
						break;
					case 44:
						token = Token.Char('\f'); return ReturnToken(token);
						break;
					case 45:
						token = Token.Char('\n'); return ReturnToken(token);
						break;
					case 46:
						token = Token.Char('\r'); return ReturnToken(token);
						break;
					case 47:
						token = Token.Char('\t'); return ReturnToken(token);
						break;
					case 48:
						if (symbol is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f') { Push(symbol); State = 51; Index++; }
						else return Error(symbol);
						break;
					case 49:
						token = Token.Char('\v'); return ReturnToken(token);
						break;
					case 50:
						if (symbol is '-') { Push(symbol); State = 52; Index++; }
						else if (symbol is >= '0' and <= '9') { Push(symbol); State = 53; Index++; }
						else return Error(symbol);
						break;
					case 51:
						if (symbol is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f') { Push(symbol); State = 54; Index++; }
						else return Error(symbol);
						break;
					case 52:
						if (symbol is >= '0' and <= '9') { Push(symbol); State = 53; Index++; }
						else return Error(symbol);
						break;
					case 53:
						if (symbol is >= '0' and <= '9') { Push(symbol); State = 53; Index++; }
						else { token = Token.Double(double.Parse(Value.ToString())); return ReturnToken(token); }
						break;
					case 54:
						if (symbol is >= '0' and <= '9' or >= 'A' and <= 'F' or >= 'a' and <= 'f') { Push(symbol); State = 55; Index++; }
						else return Error(symbol);
						break;
					case 55:
						if (symbol is '\'') { Push(symbol); State = 56; Index++; }
						else return Error(symbol);
						break;
					case 56:
						token = Token.Char((char)int.Parse(Value.ToString()[3..6], System.Globalization.NumberStyles.HexNumber)); return ReturnToken(token);
						break;
					default:
						return Error(symbol);
				}
			}
		}
		private static readonly HashSet<string> Keys = new(new string[]{
			"public",
			"private",
			"class",
			"byte",
			"char",
			"int",
			"double",
			"string",
			"void",
			"using",
			"namespace",
			"static",
			"true",
			"false",
			"for",
			"if",
			"else",
			"while",
			"do"
		});
		private int CharState;
		public char? GetChar()
		{
			char token = '\0';
			int value = 0;
			while (true)
			{
				char symbol = Peek();
				switch (CharState)
				{
					case 0:
						if (symbol is '\0') return null;
						else if (symbol is >= ' ' and <= '!' or >= '#' and <= '&' or >= '(' and <= '[' or >= ']' and <= '\uffff') { token = symbol; CharState = 1; Index++; }
						else if (symbol is '\\') { CharState = 2; Index++; }
						else return null;
						break;
					case 1:
						return token;
						break;
					case 2:
						if (symbol is '"') { token = '"'; CharState = 1; Index++; }
						else if (symbol is '0') { token = '\0'; CharState = 1; Index++; }
						else if (symbol is '\\') { token = '\\'; CharState = 1; Index++; }
						else if (symbol is 'a') { token = '\a'; CharState = 1; Index++; }
						else if (symbol is 'b') { token = '\b'; CharState = 1; Index++; }
						else if (symbol is 'f') { token = '\f'; CharState = 1; Index++; }
						else if (symbol is 'n') { token = '\n'; CharState = 1; Index++; }
						else if (symbol is 'r') { token = '\r'; CharState = 1; Index++; }
						else if (symbol is 't') { token = '\t'; CharState = 1; Index++; }
						else if (symbol is 'u') { value = 0; CharState = 3; Index++; }
						else if (symbol is 'v') { token = '\v'; CharState = 1; Index++; }
						else return null;
						break;
					case 3:
						if (symbol is >= '0' and <= '9') { value = (value << 4) | (symbol ^ 48); CharState = 4; Index++; }
						else if (symbol is >= 'A' and <= 'F') { value = (value << 4) | (10 + symbol - 'A'); CharState = 4; Index++; }
						else if (symbol is >= 'a' and <= 'f') { value = (value << 4) | (10 + symbol - 'a'); CharState = 4; Index++; }
						else return null;
						break;
					case 4:
						if (symbol is >= '0' and <= '9') { value = (value << 4) | (symbol ^ 48); CharState = 5; Index++; }
						else if (symbol is >= 'A' and <= 'F') { value = (value << 4) | (10 + symbol - 'A'); CharState = 5; Index++; }
						else if (symbol is >= 'a' and <= 'f') { value = (value << 4) | (10 + symbol - 'a'); CharState = 5; Index++; }
						else return null;
						break;
					case 5:
						if (symbol is >= '0' and <= '9') { value = (value << 4) | (symbol ^ 48); CharState = 6; Index++; }
						else if (symbol is >= 'A' and <= 'F') { value = (value << 4) | (10 + symbol - 'A'); CharState = 6; Index++; }
						else if (symbol is >= 'a' and <= 'f') { value = (value << 4) | (10 + symbol - 'a'); CharState = 6; Index++; }
						else return null;
						break;
					case 6:
						if (symbol is >= '0' and <= '9') { value = (value << 4) | (symbol ^ 48); token = (char)value; CharState = 1; Index++; }
						else if (symbol is >= 'A' and <= 'F') { value = (value << 4) | (10 + symbol - 'A'); CharState = 1; Index++; }
						else if (symbol is >= 'a' and <= 'f') { value = (value << 4) | (10 + symbol - 'a'); CharState = 1; Index++; }
						else return null;
						break;
					default:
						return null;
				}
			}
		}
	}
}
