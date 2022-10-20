using Compiler;

namespace Json.Grammar
{
	public class Parser:IParser<Token, JsonNode, JsonNode>
	{
	    public static readonly int[,] VariableTable={
	        {-1,4,5,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,6,7,8,9,-1,-1},
		{-1,-1,10,-1,-1,11,12},
		{-1,-1,13,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,25,5,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,27,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,28,5,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1},
		{-1,31,5,-1,-1,-1,-1},
		{-1,-1,-1,-1,-1,-1,-1}
	    };
	    public override JsonNode Parse(ITokenizer<Token> tokenizer)
	    {
	        Init();
	        
	        Token token = tokenizer.Get();
	        int symbol = 0;
	        bool mode = true;
	        Token[] tokens;
	        JsonNode[] values;
	        JsonNode value;
	        while (true)
	        {
	            if (mode)
	            {
	                switch (StateStack.Peek())
	                {
	                    case 0:
						if(token.Type is " ")
						{
							TokenStack.Push(token);
							StateStack.Push(1);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "[")
						{
							TokenStack.Push(token);
							StateStack.Push(2);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "{")
						{
							TokenStack.Push(token);
							StateStack.Push(3);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "String" or "Int" or "Double" or "null" or "true" or "false")
						{
							// <Space>->
							
							ValueStack.Push(null);
							symbol=2;
							mode = false;
						}
						else return Error(token);
					break;
					case 1:
						if(token.Type is "EOF" or "]" or "}" or "String" or "Int" or "Double" or "null" or "true" or "false")
						{
							// <Space>->' '
							tokens=PopToken(1);
							
							ValueStack.Push(null);
							symbol=2;
							mode = false;
						}
						else return Error(token);
					break;
					case 2:
						if(token.Type is " ")
						{
							TokenStack.Push(token);
							StateStack.Push(1);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "[")
						{
							TokenStack.Push(token);
							StateStack.Push(2);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "{")
						{
							TokenStack.Push(token);
							StateStack.Push(3);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "]" or "String" or "Int" or "Double" or "null" or "true" or "false")
						{
							// <Space>->
							
							ValueStack.Push(null);
							symbol=2;
							mode = false;
						}
						else return Error(token);
					break;
					case 3:
						if(token.Type is " ")
						{
							TokenStack.Push(token);
							StateStack.Push(1);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "}" or "String")
						{
							// <Space>->
							
							ValueStack.Push(null);
							symbol=2;
							mode = false;
						}
						else return Error(token);
					break;
					case 4:
						if(token.Type is " ")
						{
							TokenStack.Push(token);
							StateStack.Push(1);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "EOF")
						{
							// <Space>->
							
							ValueStack.Push(null);
							symbol=2;
							mode = false;
						}
						else return Error(token);
					break;
					case 5:
						if(token.Type is "String")
						{
							TokenStack.Push(token);
							StateStack.Push(14);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "Int")
						{
							TokenStack.Push(token);
							StateStack.Push(15);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "Double")
						{
							TokenStack.Push(token);
							StateStack.Push(16);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "null")
						{
							TokenStack.Push(token);
							StateStack.Push(17);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "true")
						{
							TokenStack.Push(token);
							StateStack.Push(18);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "false")
						{
							TokenStack.Push(token);
							StateStack.Push(19);
							mode = true;
							token = tokenizer.Get();
						}
						else return Error(token);
					break;
					case 6:
						if(token.Type is "]" or ",")
						{
							// <Array'>-><JsonNode>
							values=PopValue(1);
							ArrayNode an=new();
							an.Values.Add(values[0]);
							value=an;
							ValueStack.Push(value);
							symbol=4;
							mode = false;
						}
						else return Error(token);
					break;
					case 7:
						if(token.Type is "String")
						{
							TokenStack.Push(token);
							StateStack.Push(14);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "Int")
						{
							TokenStack.Push(token);
							StateStack.Push(15);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "Double")
						{
							TokenStack.Push(token);
							StateStack.Push(16);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "null")
						{
							TokenStack.Push(token);
							StateStack.Push(17);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "true")
						{
							TokenStack.Push(token);
							StateStack.Push(18);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "false")
						{
							TokenStack.Push(token);
							StateStack.Push(19);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "]")
						{
							// <Array>-><Space>
							values=PopValue(1);
							value=new ArrayNode();
							ValueStack.Push(value);
							symbol=3;
							mode = false;
						}
						else return Error(token);
					break;
					case 8:
						if(token.Type is "]")
						{
							TokenStack.Push(token);
							StateStack.Push(20);
							mode = true;
							token = tokenizer.Get();
						}
						else return Error(token);
					break;
					case 9:
						if(token.Type is ",")
						{
							TokenStack.Push(token);
							StateStack.Push(21);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "]")
						{
							// <Array>-><Array'>
							values=PopValue(1);
							value=values[0];
							ValueStack.Push(value);
							symbol=3;
							mode = false;
						}
						else return Error(token);
					break;
					case 10:
						if(token.Type is "String")
						{
							TokenStack.Push(token);
							StateStack.Push(22);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "}")
						{
							// <Object>-><Space>
							values=PopValue(1);
							value=new ObjectNode();
							ValueStack.Push(value);
							symbol=5;
							mode = false;
						}
						else return Error(token);
					break;
					case 11:
						if(token.Type is "}")
						{
							TokenStack.Push(token);
							StateStack.Push(23);
							mode = true;
							token = tokenizer.Get();
						}
						else return Error(token);
					break;
					case 12:
						if(token.Type is ",")
						{
							TokenStack.Push(token);
							StateStack.Push(24);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "}")
						{
							// <Object>-><Object'>
							values=PopValue(1);
							value=values[0];
							ValueStack.Push(value);
							symbol=5;
							mode = false;
						}
						else return Error(token);
					break;
					case 13:
						if(token.Type is "EOF")
						{
							// <File>-><JsonNode><Space>
							values=PopValue(2);
							return values[0] as JsonNode;
							ValueStack.Push(null);
							symbol=0;
							mode = false;
						}
						else return Error(token);
					break;
					case 14:
						if(token.Type is "EOF" or " " or "]" or "," or "}")
						{
							// <JsonNode>-><Space>'String'
							tokens=PopToken(1);
							values=PopValue(1);
							value=new StringNode(tokens[0].Value);
							ValueStack.Push(value);
							symbol=1;
							mode = false;
						}
						else return Error(token);
					break;
					case 15:
						if(token.Type is "EOF" or " " or "]" or "," or "}")
						{
							// <JsonNode>-><Space>'Int'
							tokens=PopToken(1);
							values=PopValue(1);
							value=new IntNode(int.Parse(tokens[0].Value));
							ValueStack.Push(value);
							symbol=1;
							mode = false;
						}
						else return Error(token);
					break;
					case 16:
						if(token.Type is "EOF" or " " or "]" or "," or "}")
						{
							// <JsonNode>-><Space>'Double'
							tokens=PopToken(1);
							values=PopValue(1);
							value=new DoubleNode(double.Parse(tokens[0].Value));
							ValueStack.Push(value);
							symbol=1;
							mode = false;
						}
						else return Error(token);
					break;
					case 17:
						if(token.Type is "EOF" or " " or "]" or "," or "}")
						{
							// <JsonNode>-><Space>'null'
							tokens=PopToken(1);
							values=PopValue(1);
							value=JsonNode.Null;
							ValueStack.Push(value);
							symbol=1;
							mode = false;
						}
						else return Error(token);
					break;
					case 18:
						if(token.Type is "EOF" or " " or "]" or "," or "}")
						{
							// <JsonNode>-><Space>'true'
							tokens=PopToken(1);
							values=PopValue(1);
							value=JsonNode.True;
							ValueStack.Push(value);
							symbol=1;
							mode = false;
						}
						else return Error(token);
					break;
					case 19:
						if(token.Type is "EOF" or " " or "]" or "," or "}")
						{
							// <JsonNode>-><Space>'false'
							tokens=PopToken(1);
							values=PopValue(1);
							value=JsonNode.False;
							ValueStack.Push(value);
							symbol=1;
							mode = false;
						}
						else return Error(token);
					break;
					case 20:
						if(token.Type is "EOF" or " " or "]" or "," or "}")
						{
							// <JsonNode>->'['<Array>']'
							tokens=PopToken(2);
							values=PopValue(1);
							value=values[0];
							ValueStack.Push(value);
							symbol=1;
							mode = false;
						}
						else return Error(token);
					break;
					case 21:
						if(token.Type is " ")
						{
							TokenStack.Push(token);
							StateStack.Push(1);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "[")
						{
							TokenStack.Push(token);
							StateStack.Push(2);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "{")
						{
							TokenStack.Push(token);
							StateStack.Push(3);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "String" or "Int" or "Double" or "null" or "true" or "false")
						{
							// <Space>->
							
							ValueStack.Push(null);
							symbol=2;
							mode = false;
						}
						else return Error(token);
					break;
					case 22:
						if(token.Type is ":")
						{
							TokenStack.Push(token);
							StateStack.Push(26);
							mode = true;
							token = tokenizer.Get();
						}
						else return Error(token);
					break;
					case 23:
						if(token.Type is "EOF" or " " or "]" or "," or "}")
						{
							// <JsonNode>->'{'<Object>'}'
							tokens=PopToken(2);
							values=PopValue(1);
							value=values[0];
							ValueStack.Push(value);
							symbol=1;
							mode = false;
						}
						else return Error(token);
					break;
					case 24:
						if(token.Type is " ")
						{
							TokenStack.Push(token);
							StateStack.Push(1);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "String")
						{
							// <Space>->
							
							ValueStack.Push(null);
							symbol=2;
							mode = false;
						}
						else return Error(token);
					break;
					case 25:
						if(token.Type is "]" or ",")
						{
							// <Array'>-><Array'>','<JsonNode>
							tokens=PopToken(1);
							values=PopValue(2);
							value=values[0];
							(values[0] as ArrayNode).Values.Add(values[1]);
							ValueStack.Push(value);
							symbol=4;
							mode = false;
						}
						else return Error(token);
					break;
					case 26:
						if(token.Type is " ")
						{
							TokenStack.Push(token);
							StateStack.Push(1);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "[")
						{
							TokenStack.Push(token);
							StateStack.Push(2);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "{")
						{
							TokenStack.Push(token);
							StateStack.Push(3);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "String" or "Int" or "Double" or "null" or "true" or "false")
						{
							// <Space>->
							
							ValueStack.Push(null);
							symbol=2;
							mode = false;
						}
						else return Error(token);
					break;
					case 27:
						if(token.Type is "String")
						{
							TokenStack.Push(token);
							StateStack.Push(29);
							mode = true;
							token = tokenizer.Get();
						}
						else return Error(token);
					break;
					case 28:
						if(token.Type is "," or "}")
						{
							// <Object'>-><Space>'String'':'<JsonNode>
							tokens=PopToken(2);
							values=PopValue(2);
							ObjectNode on=new();
							on.Values[tokens[0].Value]=values[1];
							value=on;
							ValueStack.Push(value);
							symbol=6;
							mode = false;
						}
						else return Error(token);
					break;
					case 29:
						if(token.Type is ":")
						{
							TokenStack.Push(token);
							StateStack.Push(30);
							mode = true;
							token = tokenizer.Get();
						}
						else return Error(token);
					break;
					case 30:
						if(token.Type is " ")
						{
							TokenStack.Push(token);
							StateStack.Push(1);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "[")
						{
							TokenStack.Push(token);
							StateStack.Push(2);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "{")
						{
							TokenStack.Push(token);
							StateStack.Push(3);
							mode = true;
							token = tokenizer.Get();
						}
						else if(token.Type is "String" or "Int" or "Double" or "null" or "true" or "false")
						{
							// <Space>->
							
							ValueStack.Push(null);
							symbol=2;
							mode = false;
						}
						else return Error(token);
					break;
					case 31:
						if(token.Type is "," or "}")
						{
							// <Object'>-><Object'>','<Space>'String'':'<JsonNode>
							tokens=PopToken(3);
							values=PopValue(3);
							value=values[0];
							(values[0] as ObjectNode).Values[tokens[1].Value]=values[2];
							ValueStack.Push(value);
							symbol=6;
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
	                int vt=VariableTable[StateStack.Peek(),symbol];
	                if(vt<0)return Error(token);
	                StateStack.Push(vt);
					mode = true;
	            }
	        }
	    }
	    
	}
}
