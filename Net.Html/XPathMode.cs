using System.Collections.Generic;

namespace Net.Html
{
	public interface XPathMode
	{
		IEnumerable<object> Search(IEnumerable<object> nodes);
	}
}
