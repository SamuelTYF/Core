﻿using Collection.Serialization;
using System;
using System.IO;
using TestFramework;
namespace Collection.Test
{
    public class AVL_Test : ITest
    {
        public AVL_Test()
            : base("AVL_Test",6)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            int len = 10000;
            int[] keys = new int[len];
            double[] values = new double[len];
            int[] indexs = new int[len];
            Random _R = new(DateTime.Now.Millisecond);
            for (int i = 0; i < len; i++)
            {
                keys[i] = indexs[i] = i;
                values[i] = _R.NextDouble();
            }
            Array.Sort(indexs, (a, b) => values[a].CompareTo(values[b]));
            AVL<int, double> tree = new();
            for (int i = 0; i < len; i++)
            {
                tree[keys[indexs[i]]] = values[indexs[i]];
                Ensure.Equal(i + 1, tree.Length);
            }
            for (int i = 0; i < len; i++)
                Ensure.Equal(tree[keys[i]], values[i]);
            update(1);
            int[] sortkeys = tree.LDROrder();
            for (int i = 0; i < len; i++)
                Ensure.Equal(sortkeys[i], i);
            double[] sortvalues = tree.LDRSort();
            for (int i = 0; i < len; i++)
                Ensure.Equal(sortvalues[i], values[i]);
            update(2);
            MemoryStream ms = new();
            using (Formatter formatter = new())
                formatter.Serialize(ms, tree);
            update(3);
            ms.Position = 0;
            AVL<int, double> stree;
            using (Formatter formatter = new())
                stree = formatter.Deserialize(ms) as AVL<int, double>;
            update(4);
            sortkeys = stree.LDROrder();
            for (int i = 0; i < len; i++)
                Ensure.Equal(sortkeys[i], i);
            sortvalues = stree.LDRSort();
            for (int i = 0; i < len; i++)
                Ensure.Equal(sortvalues[i], values[i]);
            update(5);
            for (int i = 0; i < len; i++)
            {
                stree.Remove(i);
                Ensure.Equal(stree.Length, len - i - 1);
                Ensure.Equal(stree.ContainsKey(i), false);
            }
            update(6);
        }
    }
}
