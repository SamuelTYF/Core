using Net.Html;
using TestFramework;
namespace Net.Test
{
    public class XPath_Test : ITest
    {
        public XPath_Test()
            : base("XPath_Test",4)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            XPath.CreateFrom("/body");
            update(1);
            XPath.CreateFrom("/body/div");
            update(2);
            XPath.CreateFrom("/body//p[@class='test']");
            update(3);
            XPath.CreateFrom("/body/p/a[0]");
            update(4);
        }
    }
}
