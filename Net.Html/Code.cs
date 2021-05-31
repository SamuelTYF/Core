using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Net.Html
{
	public static class Code
	{
		public static string GetString(string URL, CookieContainer Cookie, string Referer = null, string Host = null, string UserAgent = null, Encoding Encoding = null)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
			httpWebRequest.Method = "Get";
			if(Referer!=null)
				httpWebRequest.Referer = Referer;
			if (Host != null)
				httpWebRequest.Host = Host;
			if (UserAgent != null)
				httpWebRequest.UserAgent = UserAgent;
			if (Cookie != null)
				httpWebRequest.CookieContainer = Cookie;
			using StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding ?? Encoding.Default);
			return streamReader.ReadToEnd();
		}
		public static string GetString(string URL, string Referer = null, string Host = null, string UserAgent = null, string Cookie = null, Encoding Encoding = null)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
			httpWebRequest.Method = "Get";
			if (Referer != null)
				httpWebRequest.Referer = Referer;
			if (Host != null)
				httpWebRequest.Host = Host;
			if (UserAgent != null)
				httpWebRequest.UserAgent = UserAgent;
			if (Cookie != null)
			{
				httpWebRequest.CookieContainer = new CookieContainer();
				httpWebRequest.CookieContainer.SetCookies(new Uri(URL), Cookie);
			}
			using StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding ?? Encoding.Default);
			return streamReader.ReadToEnd();
		}
		public static Stream GetCodeStream(string url, string Referer = null, string Host = null, string UserAgent = null, CookieContainer Cookie = null)
		{
			GC.Collect();
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Method = "Get";
			if (Referer != null)
				httpWebRequest.Referer = Referer;
			if (Host != null)
				httpWebRequest.Host = Host;
			if (UserAgent != null)
				httpWebRequest.UserAgent = UserAgent;
			httpWebRequest.CookieContainer = Cookie;
			using HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			MemoryStream memoryStream = new MemoryStream();
			httpWebResponse.GetResponseStream().CopyTo(memoryStream);
			httpWebRequest.Abort();
			memoryStream.Position = 0L;
			return memoryStream;
		}
		public static IEnumerable<string> Pretreatment_First(StreamReader Source)
		{
			string text = "";
			while (!Source.EndOfStream)
			{
				char c = (char)Source.Read();
				if (!(text == "") || (c != ' ' && c != '\t' && c != '\n' && c != 0 && c != '\r'))
				{
					if (text != "" && c == '<')
					{
						yield return text;
						text = c.ToString() ?? "";
					}
					else if (text != "" && c == '>')
					{
						yield return text + c;
						text = "";
					}
					else if (c != '\t' && c != '\n' && c != 0 && c != '\r')
					{
						text += c;
					}
				}
			}
		}
		public static IEnumerable<string> Pretreatment_Second(IEnumerable<string> Source, params string[] sign)
		{
			string[] Start = new string[sign.Length];
			string[] End = new string[sign.Length];
			for (int j = 0; j < sign.Length; j++)
			{
				Start[j] = "<" + sign[j];
				End[j] = "</" + sign[j] + ">";
			}
			string compare = "";
			string ts = "";
			foreach (string s in Source)
			{
				if (s.StartsWith("<!") || s.EndsWith("-->"))
					continue;
				if (compare == "")
				{
					int i = Find(Start, s);
					yield return s;
					if (i != -1 && !s.EndsWith("/>"))
						compare = End[i];
				}
				else if (s == compare)
				{
					yield return ts;
					yield return s;
					ts = "";
					compare = "";
				}
				else ts += s;
			}
		}
		public static int Find(string[] A, string s)
		{
			for (int i = 0; i < A.Length; i++)
				if (s.StartsWith(A[i]))
					return i;
			return -1;
		}
		public static Collection.List<string> Work_Normal(string url, string Referer = null, string Host = null, string UserAgent = null, CookieContainer Cookie = null, Encoding Encoding = null)
			=>new Collection.List<string>(Pretreatment_Second(Pretreatment_First(new StreamReader(GetCodeStream(url, Referer, Host, UserAgent, Cookie), Encoding ?? Encoding.Default)), "script", "style"));
		public static Collection.List<string> Work_String(string str)
			=>new Collection.List<string>(Pretreatment_Second(Pretreatment_First(new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(str)))), "script", "style"));
		public static Collection.List<string> Work_Bytes(byte[] b)
			=>new Collection.List<string>(Pretreatment_Second(Pretreatment_First(new StreamReader(new MemoryStream(b))), "script", "style"));
		public static Collection.List<string> Work_Stream(Stream s)
			=>new Collection.List<string>(Pretreatment_Second(Pretreatment_First(new StreamReader(s)), "script", "style"));
		public static Collection.List<string> Work_StreamReader(StreamReader sr)
			=>new Collection.List<string>(Pretreatment_Second(Pretreatment_First(sr), "script", "style"));
	}
}
