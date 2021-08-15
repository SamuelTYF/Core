using System.IO;
namespace Collection.Serialization
{
    public delegate void Write(object value, Stream stream);
    public delegate void Write<in T>(T value, Stream stream);
}
