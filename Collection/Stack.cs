using System;
using System.Collections;
using System.Collections.Generic;
using Collection.Serialization;

namespace Collection
{
	public class Stack<TValue> : ISerializable, IEnumerable<TValue>, IEnumerable
	{
		private class StackNode
		{
			public TValue Value;
			public StackNode Last;
			public StackNode(TValue value, StackNode last = null)
			{
				Value = value;
				Last = last;
			}
		}
		private StackNode Top;
		public int Count { get; private set; }
		private Stack(StackNode top = null, int count = 0)
		{
			Top = top;
			Count = count;
		}
		public Stack()=>Count = 0;
		public void Insert(TValue value)
		{
			if (Top == null)
				Top = new StackNode(value);
			else
				Top = new StackNode(value, Top);
			Count++;
		}
		public TValue Pop()
		{
			if (Count == 0)
				throw new Exception();
			TValue value = Top.Value;
			if (Count-- == 1)
				Top = null;
			else
				Top = Top.Last;
			return value;
		}
		public TValue Get()
		{
			if (Count == 0)
				throw new Exception();
			return Top.Value;
		}
		public void Foreach(Foreach<TValue> function)
		{
			StackNode stackNode = Top;
			for (int i = 0; i < Count; i++)
			{
				function(stackNode.Value);
				stackNode = stackNode.Last;
			}
		}
		public Stack(Formatter formatter)
		{
			TValue[] values = formatter.Read() as TValue[];
			Count = 0;
			for (int i = 0; i < values.Length; i++)
				Insert(values[i]);
		}
		public void Write(Formatter formatter)
		{
			TValue[] values = new TValue[Count];
			StackNode stackNode = Top;
			for (int num = Count - 1; num >= 0; num--)
			{
				values[num] = stackNode.Value;
				stackNode = stackNode.Last;
			}
			formatter.Write(values);
		}
		public IEnumerator<TValue> GetEnumerator()
		{
			for (StackNode i = Top; i != null; i = i.Last)
				yield return i.Value;
		}
		IEnumerator IEnumerable.GetEnumerator()=>GetEnumerator();
		public override string ToString()
			=>"[" + string.Join(",", this) + "]";
		private static StackNode Reverse(StackNode node)
		{
			if (node.Last == null)
				return node;
			StackNode result = Reverse(node.Last);
			node.Last.Last = node;
			node.Last = null;
			return result;
		}
		public void Reverse()
		{
			if (Top != null)
				Top = Reverse(Top);
		}
	}
}
