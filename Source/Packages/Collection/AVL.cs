using Collection.Serialization;
using System;
namespace Collection
{
    public sealed class AVL<TKey, TValue> : IBST<TKey, TValue>, ISerializable where TKey : IComparable<TKey>
    {
        private sealed class Node
        {
            public Node Parent;
            public Node L;
            public Node R;
            public int Level;
            public TKey Key;
            public TValue Value;
            private bool IsL;
            public Node(Node parent, bool isl)
            {
                Parent = parent;
                Level = 0;
                IsL = isl;
            }
            public bool Find(TKey key, out Node node)
            {
                if (Level == 0)
                {
                    node = this;
                    return false;
                }
                return FindNode(key, out node);
            }
            public void Remove()
            {
                Node node;
                if (L.Level == 0)
                {
                    if (R.Level == 0)
                    {
                        node = this;
                        node.Delete();
                    }
                    else
                    {
                        if (IsL)
                        {
                            (Parent.L = R).IsL = true;
                            R.Parent = Parent;
                        }
                        else (Parent.R = R).Parent = Parent;
                        node = R;
                    }
                }
                else if (R.Level == 0)
                {
                    if (IsL)
                        (Parent.L = L).Parent = Parent;
                    else
                    {
                        (Parent.R = L).IsL = false;
                        L.Parent = Parent;
                    }
                    node = L;
                }
                else
                {
                    Swap(node = L.GetLastNode());
                    node.Remove();
                }
                node.Parent.Update();
            }
            public Node Leq(Node temp, TKey key) => Level == 0
                    ? temp
                    : Key.CompareTo(key) switch
                    {
                        -1 => R.Leq(this, key),
                        0 => this,
                        _ => L.Leq(temp, key),
                    };
            public Node Geq(Node temp, TKey key) 
                => Level == 0
                    ? temp
                    : Key.CompareTo(key) switch
                    {
                        1 => L.Geq(this, key),
                        0 => this,
                        _ => R.Geq(temp, key),
                    };
            public Node Predecessor(Node temp, TKey key) => Level == 0 ? temp : Key.CompareTo(key) == -1 ? R.Predecessor(this, key) : L.Predecessor(temp, key);
            public Node Successor(Node temp, TKey key)
                => Level == 0 ? temp : Key.CompareTo(key) == 1 ? L.Successor(this, key) : R.Successor(temp, key);
            public void Update()
            {
                if (Parent == null)
                    return;
                if (L.Level > R.Level + 1)
                {
                    if (L.R.Level > L.L.Level)
                        L.LeftRotate();
                    RightRotate();
                }
                else if (R.Level > L.Level + 1)
                {
                    if (R.L.Level > R.R.Level)
                        R.RightRotate();
                    LeftRotate();
                }
                int level = Level;
                UpdateLevel();
                if (level != Level)
                    Parent.Update();
            }
            public void DLR(Foreach<Node> func)
            {
                func(this);
                if (L.Level > 0)
                    L.DLR(func);
                if (R.Level > 0)
                    R.DLR(func);
            }
            public void LDR(Foreach<Node> func)
            {
                if (L.Level > 0)
                    L.LDR(func);
                func(this);
                if (R.Level > 0)
                    R.LDR(func);
            }
            public void LRD(Foreach<Node> func)
            {
                if (L.Level > 0)
                    L.LRD(func);
                if (R.Level > 0)
                    R.LRD(func);
                func(this);
            }
            public void RDL(Foreach<Node> func)
            {
                if (R.Level > 0)
                    R.LRD(func);
                func(this);
                if (L.Level > 0)
                    L.LRD(func);
            }
            public void DLR(Foreach<TKey, TValue> func)
            {
                func(Key, Value);
                if (L.Level > 0)
                    L.DLR(func);
                if (R.Level > 0)
                    R.DLR(func);
            }
            public void LDR(Foreach<TKey, TValue> func)
            {
                if (L.Level > 0)
                    L.LDR(func);
                func(Key, Value);
                if (R.Level > 0)
                    R.LDR(func);
            }
            public void LRD(Foreach<TKey, TValue> func)
            {
                if (L.Level > 0)
                    L.LRD(func);
                if (R.Level > 0)
                    R.LRD(func);
                func(Key, Value);
            }
            public void RDL(Foreach<TKey, TValue> func)
            {
                if (R.Level > 0)
                    R.LRD(func);
                func(Key, Value);
                if (L.Level > 0)
                    L.LRD(func);
            }
            private void UpdateLevel() => Level = ((L.Level > R.Level) ? L.Level : R.Level) + 1;
            private bool FindNode(TKey key, out Node node)
            {
                switch (key.CompareTo(Key))
                {
                    case 1:
                        if (R.Level > 0)
                            return R.FindNode(key, out node);
                        node = R;
                        return false;
                    case -1:
                        if (L.Level > 0)
                            return L.FindNode(key, out node);
                        node = L;
                        return false;
                    default:
                        node = this;
                        return true;
                }
            }
            private void LeftRotate()
            {
                R.Parent = Parent;
                R.IsL = IsL;
                Parent = IsL ? (Parent.L = R) : (Parent.R = R);
                (R = R.L).IsL = false;
                (Parent.L = R.Parent = this).IsL = true;
                UpdateLevel();
                Parent.UpdateLevel();
            }
            private void RightRotate()
            {
                L.Parent = Parent;
                L.IsL = IsL;
                Parent = IsL ? (Parent.L = L) : (Parent.R = L);
                (L = L.R).IsL = true;
                (Parent.R = L.Parent = this).IsL = false;
                UpdateLevel();
                Parent.UpdateLevel();
            }
            public Node GetFirstNode()
            {
                Node node = this;
                while (node.L.Level != 0)
                    node = node.L;
                return node;
            }
            public Node GetLastNode()
            {
                Node node = this;
                while (node.R.Level != 0)
                    node = node.R;
                return node;
            }
            private void Swap(Node node)
            {
                TKey key = Key;
                TValue value = Value;
                Key = node.Key;
                Value = node.Value;
                node.Key = key;
                node.Value = value;
            }
            private void Delete()
            {
                Level = 0;
                L = R = null;
            }
        }
        private readonly Node Hidden;
        private Node Top => Hidden.R;
        public int Length { get; private set; }
        public TValue this[TKey key]
        {
            get => Get(key);
            set => Set(key, value);
        }
        public AVL() => (Hidden = new Node(null, true)).R = new Node(Hidden, false);
        public bool ContainsKey(TKey key) => Top.Find(key, out _);
        public TValue Get(TKey key) => !Top.Find(key, out var node) ? throw new Exception() : node.Value;
        public void Set(TKey key, TValue value)
        {
            if (!Top.Find(key, out var node))
            {
                node.Key = key;
                node.L = new Node(node, isl: true);
                node.R = new Node(node, isl: false);
                node.Level = 1;
                node.Parent.Update();
                Length++;
            }
            node.Value = value;
        }
        public void Remove(TKey key)
        {
            if (!Top.Find(key, out var node))
                throw new Exception();
            node.Remove();
            Length--;
        }
        public override string ToString()
        {
            string s = "";
            Top.LDR((key, value) => s += $"Key:{key}\tValue:{value}\n");
            return s;
        }
        public bool Findleq(TKey key, ref TValue value)
        {
            Node node = Top.Leq(null, key);
            if (node != null)
            {
                value = node.Value;
                return true;
            }
            return false;
        }
        public bool Findgeq(TKey key, ref TValue value)
        {
            Node node = Top.Geq(null, key);
            if (node != null)
            {
                value = node.Value;
                return true;
            }
            return false;
        }
        public bool Predecessor(TKey key, ref TValue value)
        {
            Node node = Top.Predecessor(null, key);
            if (node != null)
            {
                value = node.Value;
                return true;
            }
            return false;
        }
        public bool Successor(TKey key, ref TValue value)
        {
            Node node = Top.Successor(null, key);
            if (node != null)
            {
                value = node.Value;
                return true;
            }
            return false;
        }
        public void DLR(Foreach<TKey, TValue> func)
        {
            if (Top.Level > 0)
                Top.DLR(func);
        }
        public void LDR(Foreach<TKey, TValue> func)
        {
            if (Top.Level > 0)
                Top.LDR(func);
        }
        public void LRD(Foreach<TKey, TValue> func)
        {
            if (Top.Level > 0)
                Top.LRD(func);
        }
        public void RDL(Foreach<TKey, TValue> func)
        {
            if (Top.Level > 0)
                Top.RDL(func);
        }
        public TKey GetFirst() => Top.GetFirstNode().Key;
        public TValue GetFirstValue() => Top.GetFirstNode().Value;
        public TKey GetLast() => Top.GetLastNode().Key;
        public AVL(Formatter formatter)
            : this()
        {
            TKey[] keys = formatter.Read() as TKey[];
            TValue[] values = formatter.Read() as TValue[];
            for (int i = 0; i < keys.Length; i++)
                Set(keys[i], values[i]);
        }
        public void Write(Formatter formatter)
        {
            TKey[] keys = new TKey[Length];
            TValue[] values = new TValue[Length];
            int p = 0;
            DLR((key, value) =>
            {
                keys[p] = key;
                values[p] = value;
                p++;
            });
            formatter.Write(keys);
            formatter.Write(values);
        }
    }
}
