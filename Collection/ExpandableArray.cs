using System;
using Collection.Serialization;

namespace Collection
{
	public class ExpandableArray<TValue> : ISerializable
	{
		public ExpandableArray<TValue> Last;
		public ExpandableArray<TValue> Next;
		public int Length;
		public int Offset;
		public int NextOffset;
		public TValue[] Values;
		public TValue this[int index]
		{
			get
			{
				ExpandableArray<TValue> node = GetNode(index);
				return node.Values[index - node.Offset];
			}
			set
			{
				ExpandableArray<TValue> node = GetNode(index);
				node.Values[index - node.Offset] = value;
			}
		}
		public ExpandableArray(TValue[] values)
		{
			if (values.Length == 0)
				values = new TValue[16];
			Last = null;
			Offset = 0;
			Length = values.Length;
			NextOffset = values.Length;
			Values = values;
		}
		public ExpandableArray(int length = 16)
		{
			Last = null;
			Offset = 0;
			Length = length;
			NextOffset = length;
			Values = new TValue[Length];
		}
		public ExpandableArray(ExpandableArray<TValue> last)
		{
			Last = last;
			Offset = last.NextOffset;
			Length = last.Length * 2;
			NextOffset = Offset + Length;
			Values = new TValue[Length];
		}
		public ExpandableArray<TValue> Create()
			=>Next = new ExpandableArray<TValue>(this);
		public ExpandableArray<TValue> GetNode(int index)
		{
			if (index < Offset)
				return Last.GetNode(index);
			if (index >= NextOffset)
				return (Next ?? Create()).GetNode(index);
			return this;
		}
		public void Update(ref ExpandableArray<TValue> array)
		{
			if (Next == null)
				array = this;
			else
				Next.Update(ref array);
		}
		public void CopyTo(int sourceIndex, TValue[] destinationArray, int destinationIndex, int length)
		{
			if (sourceIndex + length <= Offset)
			{
				Last.CopyTo(sourceIndex, destinationArray, destinationIndex, length);
				return;
			}
			if (sourceIndex >= Offset)
			{
				Array.Copy(Values, sourceIndex - Offset, destinationArray, destinationIndex, length);
				return;
			}
			Array.Copy(Values, 0, destinationArray, destinationIndex + Offset - sourceIndex, sourceIndex + length - Offset);
			Last.CopyTo(sourceIndex, destinationArray, destinationIndex, Offset - sourceIndex);
		}
		public void CopyForm_Down(TValue[] sourceArray, int sourceIndex, int destinationIndex, int length)
		{
			if (destinationIndex + length <= Offset)
			{
				Last.CopyForm_Down(sourceArray, sourceIndex, destinationIndex, length);
				return;
			}
			if (destinationIndex >= Offset)
			{
				Array.Copy(sourceArray, sourceIndex, Values, destinationIndex - Offset, length);
				return;
			}
			Array.Copy(sourceArray, sourceIndex + Offset - destinationIndex, Values, 0, destinationIndex + length - Offset);
			Last.CopyForm_Down(sourceArray, sourceIndex, destinationIndex, Offset - destinationIndex);
		}
		public void CopyForm_Up(TValue[] sourceArray, int sourceIndex, int destinationIndex, int length)
		{
			if (destinationIndex > NextOffset)
			{
				Create().CopyForm_Up(sourceArray, sourceIndex, destinationIndex, length);
				return;
			}
			if (destinationIndex + length <= NextOffset)
			{
				Array.Copy(sourceArray, sourceIndex, Values, destinationIndex - Offset, length);
				return;
			}
			Array.Copy(sourceArray, sourceIndex, Values, destinationIndex - Offset, NextOffset - destinationIndex);
			Create().CopyForm_Up(sourceArray, sourceIndex + NextOffset - destinationIndex, NextOffset, destinationIndex + length - NextOffset);
		}
		public void CopyForm(TValue[] sourceArray, int sourceIndex, int destinationIndex, int length)
		{
			if (Next != null)
				throw new Exception();
			if (destinationIndex + length > NextOffset)
				if (destinationIndex <= NextOffset)
				{
					CopyForm_Down(sourceArray, sourceIndex, destinationIndex, NextOffset - destinationIndex);
					CopyForm_Up(sourceArray, sourceIndex + NextOffset - destinationIndex, NextOffset, destinationIndex + length - NextOffset);
				}
				else CopyForm_Up(sourceArray, sourceIndex, destinationIndex, length);
			else CopyForm_Down(sourceArray, sourceIndex, destinationIndex, length);
		}
		public TValue[] ToArray()
		{
			TValue[] array = new TValue[Length];
			CopyTo(0, array, 0, Length);
			return array;
		}
		public ExpandableArray(Formatter formatter)
		{
			TValue[] array = (TValue[])formatter.Read();
			if (array.Length == 0)
				array = new TValue[16];
			Last = null;
			Offset = 0;
			Length = array.Length;
			NextOffset = array.Length;
			Values = array;
		}
		public void Write(Formatter formatter)=>formatter.Write(ToArray());
	}
}
