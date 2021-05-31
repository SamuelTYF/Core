using TestFramework;
namespace Net.Html.Net
{
    public class XPath_Test : ITest
    {
        public XPath_Test()
            : base("XPath_Test")
        {
        }
        public override void Run()
        {
            XPath.CreateFrom("/body");
            XPath.CreateFrom("/body/div");
            XPath.CreateFrom("/body//p[@class='test']");
            XPath.CreateFrom("/body/p/a[0]");
        }
    }
}
