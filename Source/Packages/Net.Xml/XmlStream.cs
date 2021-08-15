using System.IO;
using System.Text;

namespace Net.Xml
{
    public class XmlStream
    {
        public Stream Source;
        public string Temp;
        public int P;
        public int L;
        public bool CanRead;
        public XmlStream(Stream source)
        {
            Source = source;
            byte[] b = new byte[1024];
            Source.Read(b, 0, 1024);
            Temp = Encoding.UTF8.GetString(b).Replace("\r\n", "");
            L = Temp.Length;
            P = 0;
            CanRead = L != 0;
        }
        public void Update()
        {
            byte[] b = new byte[1024];
            Source.Read(b, 0, 1024);
            if (L == 0) CanRead = false;
            Temp = Encoding.UTF8.GetString(b).Replace("\r\n","");
            L = Temp.Length;
            P = 0;
        }
        public char ReadNext()
        {
            if (P == L) Update();
            return Temp[P++];
        }
        public void Back(char a,char b)
        {
            if (P >= 2)P -= 2;
            else if (P == 0) Temp = a + b + Temp;
            else if (P == 1)
            {
                Temp = a + Temp;
                P = 0;
            }
        }
        public void Back(char a)
        {
            if (P == 0) Temp = a + Temp;
            else P -= 1;
        }
    }
}
