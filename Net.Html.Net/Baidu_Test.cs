using TestFramework;
namespace Net.Html.Net
{
    public class Baidu_Test : ITest
    {
        public Baidu_Test() 
            : base("Baidu_Test") 
        {
        }
        public override void Run()
        {
            HtmlNode node = Manager.CreateFromUrl("https://www.baidu.com/");
            HtmlNode titlenode = XPath.CreateFrom("/head/title[0]").Search(node).Get(0) as HtmlNode;
            Ensure.Equal(titlenode.Text, "百度一下，你就知道");
            string enter = new Collection.List<object>(XPath.CreateFrom("/body//span[@class='bg s_btn_wr'][0]/GetValue()").Search(node)).Get(0) as string;
            Ensure.Equal("百度一下", enter);
        }
    }
}
