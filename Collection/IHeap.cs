using Collection.Serialization;
using System;
namespace Collection
{
    public interface IHeap<T> : ISerializable where T : IComparable<T>
    {
        int Length { get; }
        void Insert(T value);
        T Pop();
    }
}
