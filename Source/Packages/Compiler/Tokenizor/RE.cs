using Compiler.Tokenizor.Automata;
using System.Text;

namespace Compiler.Tokenizor
{
    public class RE
    {
		private RE_Tokenizer Tokenizer;
		private RE_Parser Parser;
		private List<Parsed_Result> Results;
		private List<string> RawRE;
		private List<string> RawActions;
		private Dictionary<string, int> ActionMap;
		private List<int> Actions;
		public List<string> Errors;
		private List<CharRange> CharRanges;
		private mDFA mDFA;
		public RE()
        {
			Tokenizer = new();
			Parser = new();
			Results = new();
			RawRE = new();
			RawActions = new();
			ActionMap = new();
			Actions = new();
			Errors = new();
		}
		public void Register(string token)
        {
			token = token.Replace("\r", "");
			token = token.Replace("\n\n", "\n");
			string[] blocks = token.Split(">>>");
			foreach(string block in blocks)
            {
				string[] lines = block.Split("\n");
				if (lines.Length < 3) continue;
				Register(lines[1], string.Join("\n", lines[2..].Where(line=>line.Length>1).ToArray()));
            }
        }
		public void Register(string re,string action)
        {
			Tokenizer.StartParse(new MemoryStream(Encoding.UTF8.GetBytes(re)));
			Parsed_Result result = Parser.Parse(Tokenizer);
			if (result.Success)
			{
				RawRE.Add(re);
				Results.Add(result);
				if(!ActionMap.ContainsKey(action))
                {
					ActionMap[action] = RawActions.Count;
					RawActions.Add(action);
                }
				Actions.Add(ActionMap[action]);
			}
            else
            {
				Errors.Add(result.Error);
				if (Tokenizer._Error != null)
					Errors.Add(Tokenizer._Error);
            }
		}
		public void Combine()
        {
			List<IRE_Block> blocks = new();
			foreach (Parsed_Result result in Results)
				blocks.AddRange(result.CharBlocks);
			MapCharSet(blocks);
			List<DFA> dfas = new();
			foreach (Parsed_Result result in Results)
				dfas.Add(CreateENFA(result.Root).ToNFA().ToDFA().Simplify());
			mNFA mnfa = Combine(dfas);
			mDFA mdfa = mnfa.TomDFA();
			Errors.AddRange(mnfa.Errors);
			mDFA smdfa = mdfa.Simplify();
			mDFA = smdfa;
		}
		private void MapCharSet(List<IRE_Block> charblocks)
		{
			List<int> ts = new();
			foreach (IRE_Block re in charblocks)
				if (re is RE_Char rec)
				{
					ts.Add(rec.Value);
					ts.Add(rec.Value + 1);
				}
				else if (re is RE_CharSet recs)
					foreach (CharRange cr in recs.Values)
					{
						ts.Add(cr.Min);
						ts.Add(cr.Max + 1);
					}
				else throw new Exception();
			ts.Sort();
			Dictionary<char, int> indexes = new();
			CharRanges = new();
			char last = (char)ts[0];
			for (int i = 1; i < ts.Count; i++)
				if (ts[i] != ts[i - 1])
				{
					indexes[last] = CharRanges.Count;
					CharRanges.Add(new(last, (char)(ts[i] - 1)));
					last = (char)ts[i];
				}
			foreach (IRE_Block re in charblocks)
				if (re is RE_Char rec)
					rec.Token = indexes[rec.Value];
				else if (re is RE_CharSet recs)
					foreach (CharRange cr in recs.Values)
					{
						int l = cr.Min;
						while (l <= cr.Max)
						{
							int index = indexes[(char)l];
							recs.Tokens.Add(index);
							l = CharRanges[index].Max + 1;
						}
					}
				else throw new Exception();
		}
		private ENFA CreateENFA(IRE_Block re)
		{
			ENFA enfa = new(CharRanges.Count);
			re.Register(enfa, enfa.Start, enfa.End);
			return enfa;
		}
		private mNFA Combine(List<DFA> dfas)
		{
			int terminals = CharRanges.Count;
			mNFA mnfa = new(terminals, RawActions.Count);
			int startvariable = mnfa.Start;
			for (int i = 0; i < dfas.Count; i++)
			{
				DFA dfa = dfas[i];
				int[] map = new int[dfa.VariableCount];
				for (int j = 0; j < dfa.VariableCount; j++)
					map[j] = mnfa.RegisterVariable(dfa.Ends[j] ? Actions[i] : null);
				if (dfa.Ends[0])
				{
					if (mnfa.Ends[0].HasValue && mnfa.Ends[0].Value != Actions[i])
						Errors.Add($"RE Combine Error: Start Varibale Can't Execute Different Actions");
					else mnfa.Ends[0] = Actions[i];
				}
				for (int start = 0; start < dfa.VariableCount; start++)
					for (int terminal = 0; terminal < terminals; terminal++)
						if (dfa.Deltas[start][terminal].HasValue)
						{
							if (start == 0)
								mnfa.InsertDelta(startvariable, terminal, map[dfa.Deltas[start][terminal].Value]);
							mnfa.InsertDelta(map[start], terminal, map[dfa.Deltas[start][terminal].Value]);
						}
			}
			return mnfa;
		}
		public string BuildTokenizer(string name,string ttoken,string method="", Language language = Language.CSharp)
        {
			switch(language)
			{
				case Language.CSharp:
					{
                        string pattern = Properties.Resources.TokenizerCSharp;
                        pattern = pattern.Replace("_Tokenizer", name);
                        pattern = pattern.Replace("TToken", ttoken);
                        List<string> cases = new();
                        for (int i = 0; i < mDFA.VariableCount; i++)
                        {
                            string temp = CreateVaribaleCodeCSharp(i);
                            cases.Add($"case {i}:\n{temp}\n\t\t\t\tbreak;");
                        }
                        pattern = pattern.Replace("//Method", string.Join("\n\t\t", method.Split("\n")));
                        pattern = pattern.Replace("//StateCode", string.Join("\n\t\t\t\t", cases));
                        return pattern;
                    }
				case Language.Python:
					{
                        string pattern = Properties.Resources.TokenizerPython;
                        pattern = pattern.Replace("_Tokenizer", name);
                        pattern = pattern.Replace("TToken", ttoken);
                        List<string> funcs = new();
                        List<string> cases = new();
                        for (int i = 0; i < mDFA.VariableCount; i++)
                        {
                            string temp = CreateVaribaleCodePython(i);
                            string f = $"Function{i}";
                            cases.Add($"def {f}(self,symbol:int)->Tuple[bool,{ttoken}]:\n{temp}");
							funcs.Add("self." + f);
                        }
                        pattern = pattern.Replace("//Map", string.Join(",", funcs));
                        pattern = pattern.Replace("//Methods", string.Join("\n\t", method.Split("\n")));
                        pattern = pattern.Replace("//Functions", string.Join("\n\t", cases));
						return pattern;
                    }
				default:throw new NotImplementedException();
            }
        }
		private string CreateVaribaleCodeCSharp(int index)
		{
			List<int>[] groups = new List<int>[mDFA.VariableCount];
			for (int i = 0; i < mDFA.VariableCount; i++)
				groups[i] = new();
			for (int terminal = 0; terminal < mDFA.TerminalCount; terminal++)
				if (mDFA.Deltas[index][terminal].HasValue)
					groups[mDFA.Deltas[index][terminal].Value].Add(terminal);
			List<string> codes = new();
			if (index == 0) codes.Add("if(symbol is '\\0')return new Token(\"EOF\");");
			for (int i = 0; i < mDFA.VariableCount; i++)
				if (groups[i].Count > 0)
				{
					List<int> group = groups[i];
					groups[i].Sort();
					List<string> conditions = new();
					char l = CharRanges[group[0]].Min;
					char r = CharRanges[group[0]].Max;
					for(int j=1;j<group.Count;j++)
                    {
                        if (r + 1 != CharRanges[group[j]].Min)
                        {
							if (l == r) conditions.Add($"'{l.FromEscape()}'");
							else conditions.Add($">= '{l.FromEscape()}' and <= '{r.FromEscape()}'");
							l = CharRanges[group[j]].Min;
						}
						r = CharRanges[group[j]].Max;
					}
					if (l == r) conditions.Add($"'{l.FromEscape()}'");
					else conditions.Add($">= '{l.FromEscape()}' and <= '{r.FromEscape()}'");
					codes.Add($"if(symbol is {string.Join(" or ", conditions)}){{Push(symbol);State={i};Index++;}}");
				}
			if (mDFA.Ends[index].HasValue)
            {
				if (codes.Count == 0)
					codes.Add($"{RawActions[mDFA.Ends[index].Value]}return ReturnToken(token);");
				else codes.Add($"{{{RawActions[mDFA.Ends[index].Value]}return ReturnToken(token);}}");
			}
			else codes.Add("return Error(symbol);");
			return $"\t\t\t\t\t{string.Join("\n\t\t\t\t\telse ", codes)}";
		}
        private string CreateVaribaleCodePython(int index)
        {
            List<int>[] groups = new List<int>[mDFA.VariableCount];
            for (int i = 0; i < mDFA.VariableCount; i++)
                groups[i] = new();
            for (int terminal = 0; terminal < mDFA.TerminalCount; terminal++)
                if (mDFA.Deltas[index][terminal].HasValue)
                    groups[mDFA.Deltas[index][terminal].Value].Add(terminal);
            List<string> codes = new()
			{
				"Value=self.Value"
			};
			bool el = false;
			if (index == 0)
			{
				codes.Add("if symbol == '\\0':\n\t\t\treturn True,Token(\"EOF\")");
                el=true;
            }
            for (int i = 0; i < mDFA.VariableCount; i++)
                if (groups[i].Count > 0)
                {
                    List<int> group = groups[i];
                    groups[i].Sort();
                    List<string> conditions = new();
                    char l = CharRanges[group[0]].Min;
                    char r = CharRanges[group[0]].Max;
                    for (int j = 1; j < group.Count; j++)
                    {
                        if (r + 1 != CharRanges[group[j]].Min)
                        {
                            if (l == r) conditions.Add($"symbol == '{l.FromEscape()}'");
                            else conditions.Add($"symbol >= '{l.FromEscape()}' and symbol <= '{r.FromEscape()}'");
                            l = CharRanges[group[j]].Min;
                        }
                        r = CharRanges[group[j]].Max;
                    }
                    if (l == r) conditions.Add($"symbol=='{l.FromEscape()}'");
                    else conditions.Add($"symbol >= '{l.FromEscape()}' and symbol <= '{r.FromEscape()}'");
                    codes.Add($"{(el?"el":"")}if {string.Join(" or ", conditions)}:\n\t\t\tself.Push(symbol)\n\t\t\tself.State={i}\n\t\t\treturn self.Pop()");
					el = true;
				}
            if (mDFA.Ends[index].HasValue)
            {
                if (codes.Count == 0)
                    codes.Add($"{(el ? "else:\n\t\t\t" : "")}{RawActions[mDFA.Ends[index].Value]}\n\t\t{(el?"\t":"")}return self.ReturnToken(token)");
                else codes.Add($"{(el ? "else:\n\t\t\t" : "")}{RawActions[mDFA.Ends[index].Value]}\n\t\t{(el ? "\t" : "")}return self.ReturnToken(token)");
            }
            else codes.Add($"{(el ? "else:\n\t\t\t" : "")}return True,Token(\"_Error\",symbol)");
            return $"\t\t{string.Join("\n\t\t", codes)}";
        }
    }
}
