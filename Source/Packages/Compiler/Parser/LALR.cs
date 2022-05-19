﻿using System.Text;
namespace Compiler.Parser
{
    public class LALR
    {
        public List<Symbol> Variables;
        public List<Symbol> Terminals;
        public Dictionary<string, Symbol> _Variables;
        public Dictionary<string, Symbol> _Terminals;
        public List<Delta> Deltas;
        public List<Closure> Closures;
        public List<List<Closure>[]> StateClosureMap;
        public List<List<Delta>> VariableDeltas;
        public List<string> Errors;
        public List<Entity[]> TerminalTable;
        public List<int?[]> VariableTable;
        public LALR()
        {
            Variables = new();
            Terminals = new();
            _Variables = new();
            _Terminals = new();
            Deltas = new();
            VariableDeltas = new();
            Errors = new();
            Closures = new();
            TerminalTable = new();
            VariableTable = new();
            StateClosureMap = new();
        }
        public void Register(string token)
        {
            LALR_Tokenizer tokenizer = new(Encoding.UTF8);
            token = token.Replace("\r", "");
            token = token.Replace("\n\n", "\n");
            string[] blocks = token.Split(">>>");
            foreach (string block in blocks)
            {
                string[] lines = block.Split("\n");
                if (lines.Length < 3) continue;
                Delta delta = tokenizer.ParseDelta(lines[1]);
                if (delta == null)
                {
                    Errors.Add(tokenizer._Error);
                    Errors.Add($"LALR Parse Error {lines[1]}");
                }
                else
                {
                    delta.Action = string.Join("\n\t\t", lines[2..].Where(line => line.Length > 1).ToArray());
                    Register(delta);
                }
            }
        }
        public Symbol RegisterTerminals(Symbol terminal)
        {
            if (!_Terminals.ContainsKey(terminal.Name))
            {
                terminal.Index = Terminals.Count;
                Terminals.Add(terminal);
                _Terminals[terminal.Name] = terminal;
            }
            return _Terminals[terminal.Name];
        }
        public Symbol RegisterVariable(Symbol variable)
        {
            if (!_Variables.ContainsKey(variable.Name))
            {
                variable.Index = Variables.Count;
                Variables.Add(variable);
                _Variables[variable.Name] = variable;
                VariableDeltas.Add(new());
            }
            return _Variables[variable.Name];
        }
        public void Register(Delta delta)
        {
            delta.Index = Deltas.Count;
            Deltas.Add(delta);
            delta.Start=RegisterVariable(delta.Start);
            for(int i=0;i<delta.Deltas.Length;i++)
                delta.Deltas[i] = delta.Deltas[i].IsVariable ? RegisterVariable(delta.Deltas[i]) : RegisterTerminals(delta.Deltas[i]);
            VariableDeltas[delta.Start.Index].Add(delta);
            List<Closure>[] closures = new List<Closure>[delta.Deltas.Length+1];
            for (int i = 0; i <= delta.Deltas.Length; i++)
                closures[i] = new();
            StateClosureMap.Add(closures);
        }
        public void ComputeFirst()
        {
            foreach (Symbol variable in Variables)
                variable.First = new();
            while(true)
            {
                bool update = false;
                for(int i=0;i< Deltas.Count;i++)
                {
                    Symbol start = Deltas[i].Start;
                    Symbol[] deltas = Deltas[i].Deltas;
                    First set = start.First;
                    bool epsilon = true;
                    for(int j=0;j<deltas.Length;j++)
                    {
                        if (deltas[j].IsVariable)
                        {
                            First t = deltas[j].First;
                            if (set.AddRange(t.Firsts)) update = true;
                            if (set.AddVariable(deltas[j])) update = true;
                            if(set.AddVariableRange(t.FirstVariables)) update = true;
                            if(!t.IsEpsilon)
                            {
                                epsilon = false;
                                break;
                            }
                        }
                        else
                        {
                            if (set.Add(deltas[j]))update = true;
                            epsilon = false;
                            break;
                        }
                    }
                    if(!set.IsEpsilon&&epsilon)
                    {
                        set.IsEpsilon = true;
                        update = true;
                    }
                }
                if (!update) break;
            }
        }
        public string PrintFirst()
        {
            List<string> results = new();
            foreach(Symbol variable in Variables)
            {
                First first = variable.First;
                List<string> firstname = new();
                firstname.AddRange(first.Firsts.Select(terminal => terminal.Name));
                if (first.IsEpsilon) firstname.Add("ϵ");
                results.Add($"{variable.Name}\t{string.Join(" ", firstname)}");
            }
            return string.Join("\n", results);
        }
        public void CreateClosures()
        {
            Delta firstDelta = new(Deltas[0].Start, Deltas[0].Deltas[..^1]);
            firstDelta.Action = Deltas[0].Action;
            State firstState = new(firstDelta, 0);
            firstState.Predicts.Add(Deltas[0].Deltas[^1]);
            Closure firstClosure = CreateClosure(firstState);
            Queue<State> queue = new();
            for (int i=0;i<Closures.Count;i++)
            {
                Closure closure = Closures[i];
                for(int terminal=0;terminal<Terminals.Count;terminal++)
                {
                    List<State> states = closure.NextTerminalStates[terminal];
                    if (states.Count == 0) continue;
                    HashSet<Closure> t = new(StateClosureMap[states[0].Delta.Index][states[0].Index+1]);
                    for (int j = 1; j < states.Count; j++)
                        t.IntersectWith(StateClosureMap[states[j].Delta.Index][states[j].Index+1]);
                    List<Closure> cs = t.Where(c => c.StateCount == states.Count).ToList();
                    if(cs.Count==1)
                    {
                        Closure nextclosure = cs[0];
                        foreach (State state in states)
                            state.NextLink.Add(nextclosure.States[state.Delta.Index][state.Index + 1]);
                        TerminalTable[i][terminal] = new Entity_Push(nextclosure.Index);
                    }
                    else if(cs.Count>1)
                    {
                        Errors.Add($"LALR Parser Error, Found Closures Conflict");
                        continue;
                    }
                    else
                    {
                        State[] nextstates = states.Select(state =>
                        {
                            State nextstate = new(state.Delta, state.Index + 1);
                            state.NextLink.Add(nextstate);
                            return nextstate;
                        }).ToArray();
                        Closure nextclosure = CreateClosure(nextstates);
                        TerminalTable[i][terminal] = new Entity_Push(nextclosure.Index);
                    }
                }
                for (int variable = 0; variable < Variables.Count; variable++)
                {
                    List<State> states = closure.NextVariableStates[variable];
                    if (states.Count == 0) continue;
                    HashSet<Closure> t = new(StateClosureMap[states[0].Delta.Index][states[0].Index + 1]);
                    for (int j = 1; j < states.Count; j++)
                        t.IntersectWith(StateClosureMap[states[j].Delta.Index][states[j].Index + 1]);
                    List<Closure> cs = t.Where(c => c.StateCount == states.Count).ToList();
                    if (cs.Count == 1)
                    {
                        Closure nextclosure = cs[0];
                        foreach (State state in states)
                            state.NextLink.Add(nextclosure.States[state.Delta.Index][state.Index + 1]);
                        VariableTable[i][variable] = nextclosure.Index;
                    }
                    else if (cs.Count > 1)
                    {
                        Errors.Add($"LALR Parser Error, Found Closures Conflict");
                        continue;
                    }
                    else
                    {
                        State[] nextstates = states.Select(state =>
                        {
                            State nextstate = new(state.Delta, state.Index + 1);
                            state.NextLink.Add(nextstate);
                            return nextstate;
                        }).ToArray();
                        Closure nextclosure = CreateClosure(nextstates);
                        VariableTable[i][variable] = nextclosure.Index;
                    }
                }
                foreach (State state in closure.TerminalStates)
                    queue.Enqueue(state);
                foreach (State state in closure.VariableStates)
                    queue.Enqueue(state);
            }
            while(queue.Count>0)
            {
                State state = queue.Dequeue();
                foreach(State nextstate in state.NextLink)
                {
                    int count = nextstate.Predicts.Count;
                    nextstate.Predicts.UnionWith(state.Predicts);
                    if (nextstate.Predicts.Count != count)
                        queue.Enqueue(nextstate);
                }
            }
            for(int i=0;i<Closures.Count;i++)
            {
                foreach (State state in Closures[i].SuccessStates)
                    foreach (Symbol terminal in state.Predicts)
                        if (TerminalTable[i][terminal.Index] == null)
                            TerminalTable[i][terminal.Index] = new Entity_Reduce(state.Delta);
                        else
                            Errors.Add($"Find Entity Conflict at Closure {i} When Read Terminal {terminal} : {TerminalTable[i][terminal.Index]} with Reduce {state.Delta}");
            }
        }
        public Closure CreateClosure(params State[] states)
        {
            TerminalTable.Add(new Entity[Terminals.Count]);
            VariableTable.Add(new int?[Variables.Count]);
            Closure closure = new(Closures.Count);
            Closures.Add(closure);
            State[][] ds = new State[Deltas.Count][];
            for (int i = 0; i < Deltas.Count; i++)
                ds[i] = new State[Deltas[i].Deltas.Length + 1];
            closure.States = ds;
            closure.NextTerminalStates = new List<State>[Terminals.Count];
            closure.NextVariableStates = new List<State>[Variables.Count];
            for (int i = 0; i < Terminals.Count; i++)
                closure.NextTerminalStates[i] = new();
            for (int i = 0; i < Variables.Count; i++)
                closure.NextVariableStates[i] = new();
            Queue<State> queue = new();
            foreach (State state in states)
            {
                ds[state.Delta.Index][state.Index] = state;
                StateClosureMap[state.Delta.Index][state.Index].Add(closure);
                if (state.End)
                    closure.SuccessStates.Add(state);
                else if (state.NextSymbol().IsVariable)
                {
                    closure.VariableStates.Add(state);
                    closure.NextVariableStates[state.NextSymbol().Index].Add(state);
                    queue.Enqueue(state);
                }
                else
                {
                    closure.TerminalStates.Add(state);
                    closure.NextTerminalStates[state.NextSymbol().Index].Add(state);
                }
                if (state.Index != 0) closure.StateCount++;
            }
            while(queue.Count>0)
            {
                State state = queue.Dequeue();
                List<Symbol> predicts = new();
                bool epsilon = true;
                for(int index=state.Index+1;index<state.Delta.Deltas.Length;index++)
                {
                    if (state.Delta.Deltas[index].IsVariable)
                    {
                        First first = state.Delta.Deltas[index].First;
                        predicts.AddRange(first.Firsts);
                        if(!first.IsEpsilon)
                        {
                            epsilon = false;
                            break;
                        }
                    }
                    else
                    {
                        predicts.Add(state.Delta.Deltas[index]);
                        epsilon = false;
                        break;
                    }
                }
                if (epsilon) predicts.AddRange(state.Predicts);
                foreach (Delta delta in VariableDeltas[state.NextSymbol().Index])
                {
                    if (ds[delta.Index][0] == null)
                    {
                        ds[delta.Index][0] = new(delta, 0);
                        StateClosureMap[delta.Index][0].Add(closure);
                        if (ds[delta.Index][0].End)
                            closure.SuccessStates.Add(ds[delta.Index][0]);
                        else if (ds[delta.Index][0].NextSymbol().IsVariable)
                        {
                            closure.VariableStates.Add(ds[delta.Index][0]);
                            queue.Enqueue(ds[delta.Index][0]);
                            closure.NextVariableStates[ds[delta.Index][0].NextSymbol().Index].Add(ds[delta.Index][0]);
                        }
                        else
                        {
                            closure.TerminalStates.Add(ds[delta.Index][0]);
                            closure.NextTerminalStates[ds[delta.Index][0].NextSymbol().Index].Add(ds[delta.Index][0]);
                        }
                    }
                    ds[delta.Index][0].Predicts.UnionWith(predicts);
                    if (epsilon)
                        state.NextLink.Add(ds[delta.Index][0]);
                }
            }
            return closure;
        }
        public string BuildParser(string name,string ttoken,string tvalue,string tresult,string method="",string init="")
        {
            string pattern = Properties.Resources.Parser;
            pattern = pattern.Replace("_Parser", name);
            pattern = pattern.Replace("TToken", ttoken);
            pattern = pattern.Replace("TValue", tvalue);
            pattern = pattern.Replace("TResult", tresult);
            pattern = pattern.Replace("//Init",init);
            List<string> variablerows = new();
            foreach (int?[] entities in VariableTable)
                variablerows.Add($"{{{string.Join(",", entities.Select(entity => entity == null ? -1 : entity))}}}");
            pattern = pattern.Replace("//VariableTable", string.Join(",\n\t\t", variablerows));
            List<string> cases = new();
            for (int i = 0; i < Closures.Count; i++)
                cases.Add(CreateTerminalCase(i));
            pattern = pattern.Replace("//ShiftCode", string.Join("\n\t\t\t\t\t", cases));
            pattern = pattern.Replace("//Method", string.Join("\n\t\t", method.Split("\n")));
            return pattern;
        }
        public string CreateTerminalCase(int index)
        {
            List<string> conditions = new();
            Entity[] entities = TerminalTable[index];
            for (int i = 0; i < entities.Length; i++)
            {
                List<string> cs = new();
                if (entities[i] is Entity_Push push)
                {
                    cs.Add("TokenStack.Push(token);");
                    cs.Add($"StateStack.Push({push.Index});");
                    cs.Add("mode = true;");
                    cs.Add("token = tokenizer.Get();");
                }
                else if (entities[i] is Entity_Reduce reduce)
                {
                    Delta delta = reduce.Delta;
                    Symbol[] deltas = delta.Deltas;
                    string action = delta.Action.Replace("\r","");
                    bool need = action.Contains("value=");
                    int tokens = 0;
                    int values = 0;
                    for (int j = 0; j < deltas.Length; j++)
                        if (deltas[j].IsVariable)
                            action = action.Replace($"Values[{j}]", $"values[{values++}]");
                        else
                            action = action.Replace($"Values[{j}]", $"tokens[{tokens++}]");
                    if(tokens>0)
                        cs.Add($"tokens=PopToken({tokens});");
                    if(values>0)
                        cs.Add($"values=PopValue({values});");
                    cs.AddRange(action.Split("\n"));
                    if(need) cs.Add($"ValueStack.Push(value);");
                    else cs.Add($"ValueStack.Push(null);");
                    cs.Add($"symbol={delta.Start.Index};");
                    cs.Add("mode = false;");
                }
                else continue;
                conditions.Add($"if(token.Type==\"{Terminals[i].Name}\")\n\t\t\t\t\t\t{{\n\t\t\t\t\t\t\t{string.Join("\n\t\t\t\t\t\t\t", cs)}\n\t\t\t\t\t\t}}");
            }
            conditions.Add("return Error(token);");
            return $"case {index}:\n\t\t\t\t\t\t{string.Join("\n\t\t\t\t\t\telse ", conditions)}\n\t\t\t\t\tbreak;";
        }
    }
}