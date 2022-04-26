using Collection;
using System;

namespace Language
{
    public class CFG<T>
    {
        public IAssemble<Variable<T>> Variables;
        public Alphabet Alphabet;
        public CFGTransitionCollection<T> Transitions;
        public Variable<T> Start;
        public CFG(Alphabet alphabet,IAssemble<Variable<T>> variables,int start)
        {
            Alphabet = alphabet;
            Variables = variables;
            Transitions = new(variables.Count);
            Start = Variables[start];
        }
        public void SetTransition(int variable,params int[] to)
        {
            Union<Variable<T>, Terminator>[] values = new Union<Variable<T>, Terminator>[to.Length];
            for (int i = 0; i < to.Length; i++)
                if (to[i] >= 0) values[i] = Variables[to[i]];
                else values[i] = Alphabet[-to[i] - 1];
            Transitions.SetTransition(Variables[variable], new(values));
        }
        public override string ToString()
        {
            List<string> list = new();
            for(int i=0;i<Variables.Count;i++)
                list.Add($"{Variables[i]}->{string.Join<ClosureEntity<Union<Variable<T>, Terminator>>>("|", Array.ConvertAll(Transitions.Variables[i].ToArray(), (t) => t.To))}");
            return string.Join("\n", list);
        }
        public bool[] FindEmptyVariable()
        {
            int count = 0;
            bool[] oldu=new bool[Variables.Count];
            bool[] newu = new bool[Variables.Count];
            Queue<CFGTransition<T>> oldqueue;
            Queue<CFGTransition<T>> newqueue = new();
            foreach (CFGTransition<T> transition in Transitions.List)
                if (transition.To.Count == 0)
                {
                    newu[transition.From.Index] = true;
                    count++;
                }
                else newqueue.Insert(transition);
            while(count!=0)
            {
                Array.Copy(newu,oldu,newu.Length);
                count = 0;
                oldqueue = newqueue;
                newqueue = new();
                while(oldqueue.Count!=0)
                {
                    CFGTransition<T> transition = oldqueue.Pop();
                    bool empty = true;
                    foreach (Union<Variable<T>, Terminator> t in transition.To.Values)
                        if (!(t.IsT1 && oldu[t.Value1.Index]))
                        {
                            empty = false;
                            break;
                        }
                    if(empty)
                    {
                        if(!newu[transition.From.Index])
                        {
                            newu[transition.From.Index] = true;
                            count++;
                        }
                    }newqueue.Insert(transition);
                }
            }
            return newu;
        }
    }
}
