using Compiler.Tokenizor.Automata;

namespace Compiler.Tokenizor
{
	public class RE_Parser : IParser<Token, IRE_Block, Parsed_Result>
	{
		public static readonly int[,] VariableTable ={
		{-1,1,-1,-1,-1,-1,-1,-1},
		{-1,-1,5,6,7,8,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,9,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,5,6,7,8,15,-1},
		{-1,-1,-1,-1,-1,-1,-1,19},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,20,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,22},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,5,6,7,8,24,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,26},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,27},
		{-1,-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1,-1}
	};
		public override Parsed_Result Parse(ITokenizer<Token> tokenizer)
		{
			Init();
			CharBlocks = new();
			Token token = tokenizer.Get();
			int symbol = 0;
			bool mode = true;
			Token[] tokens;
			IRE_Block[] values;
			IRE_Block value;
			while (true)
			{
				if (mode)
				{
					switch (StateStack.Peek())
					{
						case 0:
							if (token.Type == "EOF")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "(")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "[")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else return Error(token);
							break;
						case 1:
							if (token.Type == "EOF")
							{
								values = PopValue(1);
								return new(values[0], CharBlocks.ToArray());
								ValueStack.Push(value);
								symbol = 0;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(2);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(3);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "[")
							{
								TokenStack.Push(token);
								StateStack.Push(4);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 2:
							if (token.Type == "EOF")
							{
								tokens = PopToken(1);
								value = new RE_Char(tokens[0].Value);
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new RE_Char(tokens[0].Value);
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new RE_Char(tokens[0].Value);
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(1);
								value = new RE_Char(tokens[0].Value);
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new RE_Char(tokens[0].Value);
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new RE_Char(tokens[0].Value);
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(1);
								value = new RE_Char(tokens[0].Value);
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(1);
								value = new RE_Char(tokens[0].Value);
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 3:
							if (token.Type == "Char")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "(")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == ")")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "|")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "[")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else return Error(token);
							break;
						case 4:
							if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(10);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 5:
							if (token.Type == "EOF")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "*")
							{
								TokenStack.Push(token);
								StateStack.Push(11);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "+")
							{
								TokenStack.Push(token);
								StateStack.Push(12);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "Char")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "|")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "[")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else return Error(token);
							break;
						case 6:
							if (token.Type == "EOF")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "|")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "[")
							{
								values = PopValue(2);
								value = values[0];
								(value as RE_Block).Values.Add(values[1]);
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else return Error(token);
							break;
						case 7:
							if (token.Type == "EOF")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "*")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "|")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "[")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 8:
							if (token.Type == "EOF")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "*")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "+")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "(")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == ")")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "|")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else if (token.Type == "[")
							{
								values = PopValue(1);
								value = values[0];
								ValueStack.Push(value);
								symbol = 2;
								mode = false;
							}
							else return Error(token);
							break;
						case 9:
							if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(2);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(3);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ")")
							{
								TokenStack.Push(token);
								StateStack.Push(13);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "|")
							{
								TokenStack.Push(token);
								StateStack.Push(14);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "[")
							{
								TokenStack.Push(token);
								StateStack.Push(4);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 10:
							if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(16);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "-")
							{
								TokenStack.Push(token);
								StateStack.Push(17);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "]")
							{
								TokenStack.Push(token);
								StateStack.Push(18);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 11:
							if (token.Type == "EOF")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_Closure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_Closure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_Closure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_Closure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_Closure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_Closure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else return Error(token);
							break;
						case 12:
							if (token.Type == "EOF")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_PositiveClosure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_PositiveClosure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_PositiveClosure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_PositiveClosure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_PositiveClosure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = new RE_PositiveClosure(values[0]);
								ValueStack.Push(value);
								symbol = 3;
								mode = false;
							}
							else return Error(token);
							break;
						case 13:
							if (token.Type == "EOF")
							{
								tokens = PopToken(1);
								value = new RE_Optional();
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new RE_Optional();
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new RE_Optional();
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(1);
								value = new RE_Optional();
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new RE_Optional();
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new RE_Optional();
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(1);
								value = new RE_Optional();
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(1);
								value = new RE_Optional();
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else return Error(token);
							break;
						case 14:
							if (token.Type == "Char")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "(")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == ")")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "|")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else if (token.Type == "[")
							{
								value = new RE_Block();
								ValueStack.Push(value);
								symbol = 1;
								mode = false;
							}
							else return Error(token);
							break;
						case 15:
							if (token.Type == "EOF")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 4;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 4;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 4;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 4;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 4;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 4;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 4;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 4;
								mode = false;
							}
							else return Error(token);
							break;
						case 16:
							if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(16);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "-")
							{
								TokenStack.Push(token);
								StateStack.Push(21);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "]")
							{
								TokenStack.Push(token);
								StateStack.Push(18);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 17:
							if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(23);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 18:
							if (token.Type == "EOF")
							{
								tokens = PopToken(1);
								value = new RE_CharSet();
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								value = new RE_CharSet();
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								value = new RE_CharSet();
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(1);
								value = new RE_CharSet();
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								value = new RE_CharSet();
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								value = new RE_CharSet();
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(1);
								value = new RE_CharSet();
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(1);
								value = new RE_CharSet();
								CharBlocks.Add(value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else return Error(token);
							break;
						case 19:
							if (token.Type == "EOF")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(2);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else return Error(token);
							break;
						case 20:
							if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(2);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "(")
							{
								TokenStack.Push(token);
								StateStack.Push(3);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == ")")
							{
								TokenStack.Push(token);
								StateStack.Push(13);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "|")
							{
								TokenStack.Push(token);
								StateStack.Push(14);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "[")
							{
								TokenStack.Push(token);
								StateStack.Push(4);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 21:
							if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(25);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 22:
							if (token.Type == "EOF")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(1);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else return Error(token);
							break;
						case 23:
							if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(16);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "]")
							{
								TokenStack.Push(token);
								StateStack.Push(18);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 24:
							if (token.Type == "EOF")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(1);
								values = PopValue(2);
								value = values[1];
								(value as RE_Optional).Values.Add(values[0]);
								ValueStack.Push(value);
								symbol = 6;
								mode = false;
							}
							else return Error(token);
							break;
						case 25:
							if (token.Type == "Char")
							{
								TokenStack.Push(token);
								StateStack.Push(16);
								mode = true;
								token = tokenizer.Get();
							}
							else if (token.Type == "]")
							{
								TokenStack.Push(token);
								StateStack.Push(18);
								mode = true;
								token = tokenizer.Get();
							}
							else return Error(token);
							break;
						case 26:
							if (token.Type == "EOF")
							{
								tokens = PopToken(4);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value, tokens[3].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(4);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value, tokens[3].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(4);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value, tokens[3].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(4);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value, tokens[3].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(4);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value, tokens[3].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(4);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value, tokens[3].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(4);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value, tokens[3].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(4);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[1].Value, tokens[3].Value);
								ValueStack.Push(value);
								symbol = 5;
								mode = false;
							}
							else return Error(token);
							break;
						case 27:
							if (token.Type == "EOF")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value, tokens[2].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "*")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value, tokens[2].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "+")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value, tokens[2].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "Char")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value, tokens[2].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "(")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value, tokens[2].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == ")")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value, tokens[2].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "|")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value, tokens[2].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else if (token.Type == "[")
							{
								tokens = PopToken(3);
								values = PopValue(1);
								value = values[0];
								(value as RE_CharSet).Insert(tokens[0].Value, tokens[2].Value);
								ValueStack.Push(value);
								symbol = 7;
								mode = false;
							}
							else return Error(token);
							break;
						default:
							return Error(token);
					}
				}
				else
				{
					int vt = VariableTable[StateStack.Peek(), symbol];
					if (vt < 0) return Error(token);
					StateStack.Push(vt);
					mode = true;
				}
			}
		}
		private List<IRE_Block> CharBlocks;
	}
	public class Parsed_Result
	{
		public bool Success;
		public IRE_Block Root;
		public IRE_Block[] CharBlocks;
		public string Error;
		public Parsed_Result(IRE_Block root, IRE_Block[] blocks)
		{
			Success = true;
			Root = root;
			CharBlocks = blocks;
			Error = null;
		}
		public Parsed_Result(string error)
        {
			Success = false;
			Root = null;
			CharBlocks = null;
			Error = error;
        }
		public override string ToString()
			=> Success ?
				$"Parsed Success:{Root}" :
				$"Parsed Error:{Error}";

    }
}
