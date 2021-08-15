using System.Collections.Generic;
namespace Net.Html
{
    public interface IXPathMode
    {
        IEnumerable<object> Search(IEnumerable<object> nodes);
    }
}
