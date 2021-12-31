using Net.Html;
using TestFramework;
using Collection;
namespace Net.Test
{
    public class Baidu_Test : ITest
    {
        public Baidu_Test()
            : base("Baidu_Test",3)
        {
        }
        public override void Run(UpdateTaskProgress update)
        {
            HtmlNode node = Manager.CreateFromUrl("https://www.baidu.com/");
            update(1);
            HtmlNode titlenode = XPath.CreateFrom("/head/title[0]").Search(node).Get(0) as HtmlNode;
            Ensure.Equal(titlenode.Text, "百度一下，你就知道");
            update(2);
            string enter = new List<object>(XPath.CreateFrom("/body//span[@class='bg s_btn_wr'][0]/GetValue()").Search(node)).Get(0) as string;
            Ensure.Equal("百度一下", enter);
            update(3);
        }
    }
}
