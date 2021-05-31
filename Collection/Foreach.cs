namespace Collection
{
    public delegate void Foreach<in T>(T Value);
    public delegate void Foreach<in TKey, in TValue>(TKey Key, TValue Value);
}
