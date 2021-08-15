using System.Collections.Generic;
using System.IO;
using System.Text;
namespace File.SV
{
	public class TSV_Reader
	{
        public static IEnumerable<SVLine> Read(StreamReader sr)
		{
			while (!sr.EndOfStream)
				yield return new(sr.ReadLine().Split('\t'));
		}
		public static SVFile Read(Stream stream)
		{
			using StreamReader streamReader = new(stream);
			SVLine title = new(streamReader.ReadLine().Split('\t'));
			return new SVFile(title, Read(streamReader));
		}
		public static SVFile Read(string fname)
		{
			using FileStream stream = new FileInfo(fname).OpenRead();
			return Read(stream);
		}
	}
}
