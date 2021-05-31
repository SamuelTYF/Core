using Collection.Serialization;
namespace Collection
{
    public interface IList<TValue> : ISerializable
    {
        int Length { get; }
        TValue this[int index] { get; set; }
        void Add(TValue value);
        void AddRange(params TValue[] value);
    }
}
