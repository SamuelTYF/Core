using System;
using System.IO;
using System.Net;
using System.Text;
namespace Net.Html
{
    public static class Manager
    {
        public static HtmlNode CreateFromString(string s)
        {
            using NodeBuilder nodeBuilder = new(Code.Work_Bytes(Encoding.UTF8.GetBytes(s)));
            return nodeBuilder.Search();
        }
        public static HtmlNode CreateFromUrl(string Url, string Referer = null, string Host = null, string UserAgent = null, CookieContainer Cookie = null, Encoding Encoding = null)
        {
            using NodeBuilder nodeBuilder = new(Code.Work_Normal(Url, Referer, Host, UserAgent, Cookie, Encoding));
            return nodeBuilder.Search();
        }
        public static HtmlNode CreateFromUrl_StringCookie(string Url, string Referer = null, string Host = null, string UserAgent = null, string Cookie = null, Encoding Encoding = null)
        {
            CookieContainer cookieContainer = null;
            if (Cookie != null)
            {
                cookieContainer = new CookieContainer();
                cookieContainer.SetCookies(new Uri(Url), Cookie);
            }
            using NodeBuilder nodeBuilder = new(Code.Work_Normal(Url, Referer, Host, UserAgent, cookieContainer, Encoding));
            return nodeBuilder.Search();
        }
        public static HtmlNode CreateFromStream(Stream stream)
        {
            using NodeBuilder nodeBuilder = new(Code.Work_Stream(stream));
            return nodeBuilder.Search();
        }
        public static HtmlNode CreateFromRequest(HttpWebRequest request)
        {
            using NodeBuilder nodeBuilder = new(Code.Work_Stream(request.GetResponse().GetResponseStream()));
            return nodeBuilder.Search();
        }
    }
}
