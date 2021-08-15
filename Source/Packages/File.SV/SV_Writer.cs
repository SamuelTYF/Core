using System.IO;
using System.Text;

namespace File.SV
{
    public static class SV_Writer
    {
		public static void Write(this SVFile csv,Stream stream,char sign=',')
		{
			using StreamWriter streamWriter = new(stream, Encoding.UTF8);
			streamWriter.WriteLine(string.Join(sign,csv.Title.Values));
			for (int i = 0; i < csv.Values.Length; i++)
				streamWriter.WriteLine(string.Join(sign,csv[i].Values));
		}
		public static void Write(this SVFile csv,string fname, char sign = ',')
		{
			FileInfo fileInfo = new(fname);
			if (fileInfo.Exists)
				fileInfo.Delete();
			using FileStream stream = fileInfo.OpenWrite();
			csv.Write(stream,sign);
		}
	}
}
