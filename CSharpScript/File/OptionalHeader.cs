using System.IO;
namespace CSharpScript.File
{
	public struct OptionalHeader
	{
		public StandardFields StandardFields;
		public WindowsSpecificFields WindowsSpecificFields;
		public DataDirectories DataDirectories;
		public OptionalHeader(Stream stream)
		{
			StandardFields = new StandardFields(stream);
			WindowsSpecificFields = new WindowsSpecificFields(stream, StandardFields.Magic);
			DataDirectories = new DataDirectories(stream);
		}
		public override string ToString()
		{
			return $"{StandardFields}\n{WindowsSpecificFields}\n{DataDirectories}";
		}
	}
}
