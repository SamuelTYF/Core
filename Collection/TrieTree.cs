using Collection.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace Collection
{
    public class TrieTree<TValue> : ISerializable, IEnumerable<TValue>, IEnumerable
    {
        public Encoding Encoding;
        public TrieTree<TValue>[] Nodes;
        public TValue Value;
        public TValue this[string key, int index = 0]
        {
            get => GetNode(Encoding.GetBytes(key), index).Value;
            set => GetNode(Encoding.GetBytes(key), index).Value = value;
        }
        public TValue this[byte[] key, int index = 0]
        {
            get => GetNode(key, index).Value;
            set => GetNode(key, index).Value = value;
        }
        public TValue this[int key]
        {
            get => GetNode(BitConverter.GetBytes(key)).Value;
            set => GetNode(BitConverter.GetBytes(key)).Value = value;
        }
        public TrieTree()
        {
            Nodes = new TrieTree<TValue>[256];
            Encoding = Encoding.UTF8;
        }
        public TrieTree<TValue> GetNode(byte[] keys, int index = 0)
        {
            if (index == keys.Length)
                return this;
            byte b = keys[index];
            return (Nodes[b] ?? (Nodes[b] = new TrieTree<TValue>())).GetNode(keys, index + 1);
        }
        public TrieTree<TValue> GetNode(byte[] keys, int index, int length)
        {
            if (index == length)
                return this;
            byte b = keys[index];
            return (Nodes[b] ?? (Nodes[b] = new TrieTree<TValue>())).GetNode(keys, index + 1, length);
        }
        public TrieTree<TValue> GetNode(string key, int index = 0)
            => GetNode(Encoding.UTF8.GetBytes(key), index);
        private int Update()
        {
            int r = (Value != null) ? 1 : 0;
            for (int i = 0; i < 256; i++)
                if (Nodes[i] != null)
                {
                    int count = Nodes[i].Update();
                    if (count == 0)
                        Nodes[i] = null;
                    r += count;
                }
            return r;
        }
        public void Replace(CheckFunc<TrieTree<TValue>> check, TValue value)
        {
            if (check(this))
                Value = value;
            for (int i = 0; i < 256; i++)
                if (Nodes[i] != null)
                    Nodes[i].Replace(check, value);
        }
        private void Write(List<byte> info, string temp, TValue[] values, ref int index)
        {
            if (Value != null)
            {
                info.Add(43);
                info.Add((byte)temp.Length);
                string text = temp;
                foreach (char c in text)
                    info.Add((byte)c);
                values[index++] = Value;
                temp = "";
            }
            for (int j = 0; j < 256; j++)
                if (Nodes[j] != null)
                    Nodes[j].Write(info, temp + (char)j, values, ref index);
            info.Add(45);
        }
        public TrieTree(Formatter formatter)
        {
            Nodes = new TrieTree<TValue>[256];
            byte[] keys = formatter.Read() as byte[];
            TValue[] values = formatter.Read() as TValue[];
            int num = 0;
            Stack<TrieTree<TValue>> stack = new();
            TrieTree<TValue> top = this;
            int level = -1;
            if (keys[^1] != 45)
                throw new Exception();
            for (int i = 0; i < keys.Length - 1; i++)
                if (level > 0)
                {
                    stack.Insert(top);
                    top = top.Nodes[keys[i]] = new TrieTree<TValue>();
                    level--;
                }
                else if (keys[i] == 43)
                    level = keys[++i];
                else
                {
                    if (keys[i] != 45)
                        throw new Exception();
                    top = stack.Pop();
                }
            if (level == 0)
                top.Value = values[num++];
            if (stack.Count != 0)
                throw new Exception();
        }
        public void Write(Formatter formatter)
        {
            int num = Update();
            TValue[] array = new TValue[num];
            int index = 0;
            List<byte> list = new();
            Write(list, "", array, ref index);
            if (index != num)
                throw new Exception();
            formatter.Write(list.ToArray());
            formatter.Write(array);
        }
        public void Foreach(Foreach<TrieTree<TValue>> function)
        {
            if (Value != null && !Value.Equals(null))
                function(this);
            TrieTree<TValue>[] nodes = Nodes;
            for (int i = 0; i < nodes.Length; i++)
                nodes[i]?.Foreach(function);
        }
        public void Foreach(Foreach<string, TrieTree<TValue>> function, string suf = "")
        {
            if (Value != null && !Value.Equals(null))
                function(suf, this);
            for (int i = 0; i < 256; i++)
                if (Nodes[i] != null)
                    Nodes[i].Foreach(function, suf + (char)i);
        }
        private string GetString(string suf, string separator = ",")
        {
            string text = "";
            if (Value != null && !Value.Equals(null))
                text += $"{suf}:{Value}";
            bool flag = false;
            for (int i = 0; i < 256; i++)
                if (Nodes[i] != null)
                {
                    if (flag)
                        text += separator;
                    text += Nodes[i].GetString(suf + (char)i, separator);
                    flag = true;
                }
            return text;
        }
        public override string ToString() => GetString("");
        public string ToString(string separator = ",") => GetString("", separator);
        public IEnumerator<TValue> GetEnumerator()
        {
            if (Value != null)
                yield return Value;
            TrieTree<TValue>[] nodes = Nodes;
            foreach (TrieTree<TValue> node in nodes)
            {
                if (node == null)
                    continue;
                foreach (TValue item in node)
                    yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
