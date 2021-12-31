namespace Language
{
    public class Variable<T>
    {
        public T Name;
        public int Index;
        public bool End;
        public Variable(T name,int index)
        {
            Name = name;
            Index = index;
        }
        public static Assemble<Variable<T>> Create(params T[] values)
        {
            Variable<T>[] variables = new Variable<T>[values.Length];
            for (int i = 0; i < values.Length; i++)
                variables[i] = new(values[i], i);
            return new(variables);
        }
        public override string ToString() => Name.ToString();
    }
}