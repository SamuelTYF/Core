using Compiler;
using System.Text;
namespace Json.Grammar
{
	public class Tokenizer:ITokenizer<Token>
	{
		public Tokenizer(Encoding encoding,int length=1<<10):base(encoding,length){}
		public Tokenizer(int length = 1 << 10) : base(length) { }
		public override Token Get()
		{
			Token token;
			while(true)
			{
				char symbol = Peek();
				switch (State)
				{
					case 0:
					if(symbol is '\0')return new Token("EOF");
					else if(symbol is >= '\t' and <= '\n' or '\r' or ' '){Push(symbol);State=1;Index++;}
					else if(symbol is '"'){Push(symbol);State=2;Index++;}
					else if(symbol is ',' or ':' or '[' or ']' or '{' or '}'){Push(symbol);State=3;Index++;}
					else if(symbol is '-'){Push(symbol);State=4;Index++;}
					else if(symbol is '0'){Push(symbol);State=5;Index++;}
					else if(symbol is >= '1' and <= '9'){Push(symbol);State=6;Index++;}
					else if(symbol is >= 'A' and <= 'Z' or >= 'a' and <= 'z'){Push(symbol);State=7;Index++;}
					else return Error(symbol);
				break;
				case 1:
					if(symbol is >= '\t' and <= '\n' or '\r' or ' '){Push(symbol);State=1;Index++;}
					else if(symbol is ',' or ':' or '[' or ']' or '{' or '}'){Push(symbol);State=3;Index++;}
					else if(symbol is '-'){Push(symbol);State=4;Index++;}
					else {token = new(" ");return ReturnToken(token);}
				break;
				case 2:
					if(symbol is >= ' ' and <= '!' or >= '#' and <= '[' or >= ']' and <= '\uffff'){Push(symbol);State=2;Index++;}
					else if(symbol is '"'){Push(symbol);State=8;Index++;}
					else if(symbol is '\\'){Push(symbol);State=9;Index++;}
					else return Error(symbol);
				break;
				case 3:
					token = new(Value.ToString().Trim());return ReturnToken(token);
				break;
				case 4:
					if(symbol is '0'){Push(symbol);State=5;Index++;}
					else if(symbol is >= '1' and <= '9'){Push(symbol);State=6;Index++;}
					else return Error(symbol);
				break;
				case 5:
					if(symbol is '.'){Push(symbol);State=10;Index++;}
					else {token = new("Int",Value.ToString());return ReturnToken(token);}
				break;
				case 6:
					if(symbol is >= '0' and <= '9'){Push(symbol);State=6;Index++;}
					else if(symbol is '.'){Push(symbol);State=10;Index++;}
					else {token = new("Int",Value.ToString());return ReturnToken(token);}
				break;
				case 7:
					if(symbol is >= 'A' and <= 'Z' or >= 'a' and <= 'z'){Push(symbol);State=7;Index++;}
					else {string key=Value.ToString();if(Keys.Contains(key))token = new(key);else return Error(symbol);return ReturnToken(token);}
				break;
				case 8:
					token = new("String",Value.ToString()[1..^1]);return ReturnToken(token);
				break;
				case 9:
					if(symbol is '"' or '\\'){Push(symbol);State=2;Index++;}
					else return Error(symbol);
				break;
				case 10:
					if(symbol is >= '0' and <= '9'){Push(symbol);State=11;Index++;}
					else return Error(symbol);
				break;
				case 11:
					if(symbol is >= '0' and <= '9'){Push(symbol);State=11;Index++;}
					else {token = new("Int",Value.ToString());return ReturnToken(token);}
				break;
					default:
						return Error(symbol);
				}
			}
		}
		private static readonly HashSet<string> Keys=new(new string[]{
			    "true",
			    "false",
			    "null"
			});
	}
}
