using System.IO;
using Automata;
using Net.Http.Properties;
namespace Net.Http
{
	public sealed class HeaderReader
	{
		public static readonly AutomataInstance HeaderReader_Instance = CreateInstance();
		public static AutomataInstance CreateInstance()
		{
			using StreamReader streamReader = new(new MemoryStream(Resources.Header));
			return AKC.ReadFrom(streamReader.ReadToEnd());
		}
		public static Pre_Header ReadFrom(AutomataInstance instance, string header)
		{
			HeaderReaderHost headerReaderHost = new();
			StringArg stringArg = new(header);
			instance.Run(headerReaderHost, stringArg);
			headerReaderHost.Pre_Header.Data = stringArg;
			return headerReaderHost.Pre_Header;
		}
        public static Pre_Header ReadFrom(string header) => ReadFrom(HeaderReader_Instance, header);
    }
}
